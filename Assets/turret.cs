using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject BulletObject;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootBullet", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootBullet() {
        GameObject newBullet = Instantiate(this.BulletObject, this.transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-10,0) * 500);
        
    }


}
