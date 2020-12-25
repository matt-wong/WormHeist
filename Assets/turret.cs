﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    public enum eTurretType{
        LeftRight_UpDown
    }

    public float ShootDelay = 0.5f; 
    public eTurretType TurretType;

    public GameObject BulletObject;
    public GameObject LazerObject;
    public GameObject LazerTrackObject;

    private List<GameObject> lazersObjects;
    public List<List<eSensorDirection>> SensorSequence;
    private int sensorPhaseIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        //Default to Cross Alternating
        SensorSequence = new List<List<eSensorDirection>>();
        List<eSensorDirection> phase1 = new List<eSensorDirection>()  {eSensorDirection.E, eSensorDirection.W};
        List<eSensorDirection> phase2 = new List<eSensorDirection>()  {eSensorDirection.N, eSensorDirection.S};
        SensorSequence.Add(phase1);
        SensorSequence.Add(phase2);

        CreateLazersFromInstructions();

        InvokeRepeating("NextLazerState",2,2);
    }

    private void CreateLazersFromInstructions()
    {
        lazersObjects = new List<GameObject>();
        
        CreateLazerOfDirection(0, false);
        CreateLazerOfDirection(90, true);
        CreateLazerOfDirection(180, false);
        CreateLazerOfDirection(270, true);
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
        Debug.Log(lazerScript.direction);
        lazerScript.IsTriggered += this.ShootBulletDirection;;

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
        Debug.Log(dtv);
        rb.AddForce(dtv * 5000);
    }

    void ShootBulletDirection(eSensorDirection direction){
        Debug.Log(direction);
        StartCoroutine(Foo(VectorHelper.DirectionEnumToVector(direction), ShootDelay));
    }



    IEnumerator Foo(Vector2 vect, float delay)
    {
        yield return new WaitForSeconds(delay);

        ShootBullet(vect);
    }
}
