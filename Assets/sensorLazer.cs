using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSensorDirection{
    N,
    NE,
    E,
    SE,
    S,
    SW,
    W,
    NW
}


public class sensorLazer : MonoBehaviour
{

    public System.Action<eSensorDirection> IsTriggered;
    private bool IsReady = true;
    public eSensorDirection direction = eSensorDirection.N;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (IsReady)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            // TODO: Animation for Alarm
            sr.color = Color.green;
            IsTriggered.Invoke(this.direction);
            IsReady = false;
        }

    }

    internal void Reset()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        IsReady = true;
    }
}
