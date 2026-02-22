using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    public float ShootDelay = 0.25f;
    public GameObject BulletObject;
    public GameObject LazerObject;
    public GameObject LazerTrackObject;

    protected GameObject CreateLazerOfDirection(float angle, bool startingOn, float length = 10f)
    {
        GameObject newLazer = Instantiate(LazerObject, transform.position, Quaternion.identity);
        newLazer.transform.Rotate(0, 0, angle);
        newLazer.transform.localScale = new Vector3(1, length, 0);

        GameObject newLazerTrack = Instantiate(LazerTrackObject, transform.position, Quaternion.identity);
        newLazerTrack.transform.Rotate(0, 0, angle);
        newLazerTrack.transform.localScale = new Vector3(1, length, 0);

        newLazer.SetActive(startingOn);
        sensorLazer lazerScript = newLazer.GetComponentInChildren<sensorLazer>();
        lazerScript.direction = VectorHelper.AngleToDirectionEnum(angle);
        lazerScript.IsTriggered += ShootBulletDirection;

        return newLazer;
    }

    protected void ShootBullet(Vector2 vector)
    {
        GameObject newBullet = Instantiate(BulletObject, transform.position + new Vector3(vector.normalized.x, vector.normalized.y, 0), Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        float angle = VectorHelper.Angle(vector);
        angle += Random.Range(-2, 2);

        Vector2 dtv = VectorHelper.DegreeToVector2(angle).normalized;
        rb.AddForce(dtv * 5000);
    }

    protected virtual void ShootBulletDirection(eSensorDirection direction)
    {
        StartCoroutine(ShootAfterDelay(VectorHelper.DirectionEnumToVector(direction), ShootDelay));
    }

    protected IEnumerator ShootAfterDelay(Vector2 vect, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Shooting bullet at " + vect);
        ShootBullet(vect);
    }

    protected abstract void InitializeTurret();

    void Start()
    {
        InitializeTurret();
    }
}

public class turret : TurretBase
{
    public enum eTurretType
    {
        LeftRight_UpDown,
        LeftRight_Down,
        LeftRight_None,
        Cross_Turn
    }

    public eTurretType TurretType;
    public int SequenceOffset = 0;
    public float phaseTime = 3;

    private List<GameObject> lazersObjects;
    private List<List<eSensorDirection>> SensorSequence;
    private int sensorPhaseIndex = -1;

    protected override void InitializeTurret()
    {
        PopulateSequence();
        sensorPhaseIndex = SequenceOffset;
        CreateLazersFromInstructions();
        InvokeRepeating(nameof(NextLazerState), 0, phaseTime);
    }

    private void PopulateSequence()
    {
        SensorSequence = new List<List<eSensorDirection>>();
        switch (TurretType)
        {
            case eTurretType.LeftRight_UpDown:
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W });
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.N, eSensorDirection.S });
                break;

            case eTurretType.LeftRight_Down:
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W });
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.S });
                break;

            case eTurretType.LeftRight_None:
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W });
                SensorSequence.Add(new List<eSensorDirection>() { });
                break;

            case eTurretType.Cross_Turn:
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W, eSensorDirection.N, eSensorDirection.S });
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.SE, eSensorDirection.SW, eSensorDirection.NW, eSensorDirection.NE });
                break;

            default:
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W });
                SensorSequence.Add(new List<eSensorDirection>() { eSensorDirection.N, eSensorDirection.S });
                break;
        }
    }

    private void CreateLazersFromInstructions()
    {
        lazersObjects = new List<GameObject>();
        HashSet<eSensorDirection> allDirections = new HashSet<eSensorDirection>();

        foreach (List<eSensorDirection> inst in SensorSequence)
        {
            foreach (eSensorDirection dir in inst)
                allDirections.Add(dir);
        }

        foreach (eSensorDirection dir in allDirections)
            lazersObjects.Add(CreateLazerOfDirection(VectorHelper.Angle(VectorHelper.DirectionEnumToVector(dir)), true));
    }

    private void NextLazerState()
    {
        sensorPhaseIndex = (sensorPhaseIndex + 1) % SensorSequence.Count;

        foreach (GameObject element in lazersObjects)
            element.SetActive(false);

        foreach (eSensorDirection dir in SensorSequence[sensorPhaseIndex])
        {
            GameObject lazerObjectForDir = FindLazer(dir);
            if (lazerObjectForDir != null)
            {
                lazerObjectForDir.SetActive(true);
                lazerObjectForDir.GetComponentInChildren<sensorLazer>().Reset();
            }
        }
    }

    private GameObject FindLazer(eSensorDirection dir)
    {
        return lazersObjects.Find(element =>
        {
            sensorLazer maybeObj = element.GetComponentInChildren<sensorLazer>();
            return maybeObj != null && maybeObj.direction == dir;
        });
    }
}
