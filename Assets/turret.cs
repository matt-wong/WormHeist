using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    public enum eTurretType{
        LeftRight_UpDown,
        LeftRight_Down,
        LeftRight_None,
        Cross_Turn,
        Clockwise_Rotate
    }

    public float ShootDelay = 0.25f; 
    public eTurretType TurretType;

    public GameObject BulletObject;
    public GameObject LazerObject;
    public GameObject LazerTrackObject;
    public float phaseTime = 3;

    private List<GameObject> lazersObjects;
    public List<List<eSensorDirection>> SensorSequence;
    private int sensorPhaseIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        PopulateSequence();

        CreateLazersFromInstructions();

        InvokeRepeating("NextLazerState",0,phaseTime);
    }

    private void PopulateSequence()
    {
        SensorSequence = new List<List<eSensorDirection>>();
        switch (this.TurretType)
        {
            case eTurretType.Clockwise_Rotate:
                List<eSensorDirection> phase1 = new List<eSensorDirection>() { eSensorDirection.N};
                List<eSensorDirection> phase2 = new List<eSensorDirection>() { eSensorDirection.NE};
                List<eSensorDirection> phase3 = new List<eSensorDirection>() { eSensorDirection.E};
                List<eSensorDirection> phase4 = new List<eSensorDirection>() { eSensorDirection.SE};
                List<eSensorDirection> phase5 = new List<eSensorDirection>() { eSensorDirection.S};
                List<eSensorDirection> phase6 = new List<eSensorDirection>() { eSensorDirection.SW};
                List<eSensorDirection> phase7 = new List<eSensorDirection>() { eSensorDirection.W};
                List<eSensorDirection> phase8 = new List<eSensorDirection>() { eSensorDirection.NW};
                SensorSequence.Add(phase1);
                SensorSequence.Add(phase2);
                SensorSequence.Add(phase3);
                SensorSequence.Add(phase4);
                SensorSequence.Add(phase5);
                SensorSequence.Add(phase6);
                SensorSequence.Add(phase7);
                SensorSequence.Add(phase8);
                break;

            case eTurretType.LeftRight_UpDown:
                phase1 = new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W };
                phase2 = new List<eSensorDirection>() { eSensorDirection.N, eSensorDirection.S };
                SensorSequence.Add(phase1);
                SensorSequence.Add(phase2);
                break;

            case eTurretType.LeftRight_Down:
                phase1 = new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W };
                phase2 = new List<eSensorDirection>() { eSensorDirection.S };
                SensorSequence.Add(phase1);
                SensorSequence.Add(phase2);
                break;

            case eTurretType.LeftRight_None:
                phase1 = new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W };
                phase2 = new List<eSensorDirection>() { };
                SensorSequence.Add(phase1);
                SensorSequence.Add(phase2);
                break;

            case eTurretType.Cross_Turn:
                phase1 = new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W,  eSensorDirection.N, eSensorDirection.S };
                phase2 = new List<eSensorDirection>() { eSensorDirection.SE, eSensorDirection.SW,  eSensorDirection.NW, eSensorDirection.NE};
                SensorSequence.Add(phase1);
                SensorSequence.Add(phase2);
                break;
            default:
                phase1 = new List<eSensorDirection>() { eSensorDirection.E, eSensorDirection.W };
                phase2 = new List<eSensorDirection>() { eSensorDirection.N, eSensorDirection.S };
                SensorSequence.Add(phase1);
                SensorSequence.Add(phase2);
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
            {
                allDirections.Add(dir);
            }
        }

        foreach (eSensorDirection dir in allDirections)
        {
            CreateLazerOfDirection(VectorHelper.Angle(VectorHelper.DirectionEnumToVector(dir)), true);
        }

    }

    GameObject CreateLazerOfDirection(float angle, bool startingOn){
        GameObject newLazer = Instantiate(this.LazerObject, this.transform.position, Quaternion.identity);
        newLazer.transform.Rotate(0, 0, angle);
        newLazer.transform.localScale = new Vector3(1, 10, 0);

        GameObject newLazerTrack = Instantiate(this.LazerTrackObject, this.transform.position, Quaternion.identity);
        newLazerTrack.transform.Rotate(0, 0, angle);
        newLazerTrack.transform.localScale = new Vector3(1, 10, 0);

        newLazer.SetActive(startingOn);
        sensorLazer lazerScript = newLazer.GetComponentInChildren<sensorLazer>();
        lazerScript.direction = VectorHelper.AngleToDirectionEnum(angle);
        lazerScript.IsTriggered += this.ShootBulletDirection;

        lazersObjects.Add(newLazer);
        return newLazer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextLazerState()
    {

        this.sensorPhaseIndex += 1;
        this.sensorPhaseIndex = this.sensorPhaseIndex % this.SensorSequence.Count;
        //Clear all lazers first
        foreach (GameObject element in lazersObjects)
        {
            element.SetActive(false);
            
        }

        foreach (eSensorDirection dir in SensorSequence[this.sensorPhaseIndex]){
            GameObject lazerObjectForDir = this.FindLazer(dir);
            if (lazerObjectForDir)
            {
                lazerObjectForDir.SetActive(true);
                sensorLazer lazer = lazerObjectForDir.GetComponentInChildren<sensorLazer>();
                lazer.Reset();
            }
        }
    }

    GameObject FindLazer(eSensorDirection dir){
        return this.lazersObjects.Find((GameObject element ) => {
            sensorLazer maybeObj = element.GetComponentInChildren<sensorLazer>();
            if (maybeObj && maybeObj.direction == dir){
                return true;
            }else{
                return false;
            }
        });
    }

    void ShootBullet(Vector2 vector) {
        GameObject newBullet = Instantiate(this.BulletObject, this.transform.position + new Vector3(vector.normalized.x, vector.normalized.y, 0), Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        float angle = VectorHelper.Angle(vector);
        angle += Random.Range(-2, 2);

        Vector2 dtv = VectorHelper.DegreeToVector2(angle).normalized;
        rb.AddForce(dtv * 5000);
    }

    void ShootBulletDirection(eSensorDirection direction){
        StartCoroutine(Foo(VectorHelper.DirectionEnumToVector(direction), ShootDelay));
    }



    IEnumerator Foo(Vector2 vect, float delay)
    {
        yield return new WaitForSeconds(delay);

        ShootBullet(vect);
    }
}
