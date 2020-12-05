using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(worldPosition.x - transform.position.x, worldPosition.y - transform.position.y);
        
        // float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //  transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        rb.AddForce(direction.normalized * 8000);
    }

    
}
