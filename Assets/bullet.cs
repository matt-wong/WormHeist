using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    const float MIN_SPEED = 1f;
    private Rigidbody2D myRBody;
    private bool myWasFired = false;

    // Start is called before the first frame update
    void Start()
    {
        myRBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myWasFired)
        {
            if (myRBody.linearVelocity.magnitude > 1)
            {
                myWasFired = true;
            }
        }
        else if (myRBody.linearVelocity.magnitude < MIN_SPEED)
        {
            Debug.Log("DEAD");
            Destroy(gameObject);
        }
    }
}
