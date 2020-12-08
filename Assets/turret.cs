using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    public enum eTurretType{
        LeftRight_UpDown
    }

    public eTurretType TurretType;

    public GameObject BulletObject;
    public GameObject LazerObject;

    private List<GameObject> lazersObjects;

    // Start is called before the first frame update
    void Start()
    {
        //Default to Cross Alternating
        lazersObjects = new List<GameObject>();
        
        CreateLazerOfDirection(0, false);
        CreateLazerOfDirection(90, true);
        CreateLazerOfDirection(180, false);
        CreateLazerOfDirection(270, true);

        InvokeRepeating("NextLazerState",3,3);
    }

    GameObject CreateLazerOfDirection(float angle, bool startingOn){
        GameObject newLazer = Instantiate(this.LazerObject, this.transform.position, Quaternion.identity);
        newLazer.transform.Rotate(0, 0, angle);
        newLazer.transform.localScale = new Vector3(10, 1, 0);

        newLazer.SetActive(startingOn);
        sensorLazer lazerScript2 = newLazer.GetComponentInChildren<sensorLazer>();
        lazerScript2.IsTriggered += () => { Invoke("ShootBulletLeft", 1); };

        lazersObjects.Add(newLazer);
        return newLazer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextLazerState()
    {
        foreach (GameObject element in lazersObjects)
        {
            element.SetActive(!element.activeSelf);
            if (element.activeSelf){
                sensorLazer lazer = element.GetComponentInChildren<sensorLazer>();
                lazer.IsTriggered += () => { Invoke("ShootBulletLeft", 1); };
                lazer.Reset();
            }
        }
    }

    void ShootBullet(Vector2 vector) {
        GameObject newBullet = Instantiate(this.BulletObject, this.transform.position + new Vector3(vector.normalized.x, vector.normalized.y, 0), Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        float angle = VectorHelper.Angle(vector);
        angle += Random.Range(-2, 2);

        Vector2 dtv = VectorHelper.DegreeToVector2(angle).normalized;
        Debug.Log(dtv);
        rb.AddForce(dtv * 5000);
    }

    void ShootBulletLeft(){
        ShootBullet(Vector2.left);
        ShootBullet(Vector2.up);
        ShootBullet(Vector2.down);
        ShootBullet(Vector2.right);
    }
}
