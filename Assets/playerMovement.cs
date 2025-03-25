using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    gyroscopeTracker gyro; 

    // Start is called before the first frame update
    void Start()
    {
        gyro = GameObject.FindFirstObjectByType<gyroscopeTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            this.MoveTowardMouse();
            InvokeRepeating("MoveTowardMouse", 0.25f, 0.25f);
        }else if (Input.GetKeyUp(KeyCode.Mouse0)){
            CancelInvoke();
        }
    }

    private void MoveTowardMouse(){        
        Rigidbody2D rb = GetComponentInChildren<Rigidbody2D>();
        // if (gyro.isEnabled){
        Vector2 tiltDirection = new Vector2((float)gyro.tiltX, (float)gyro.tiltY);
        
        rb.AddForce(tiltDirection.normalized * 8000);
    }

    
}
