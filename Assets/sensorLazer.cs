using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorLazer : MonoBehaviour
{

    public System.Action IsTriggered;
    private bool IsReady = true;

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
            IsTriggered.Invoke();
            IsReady = false;
        }

    }
}
