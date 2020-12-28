using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    public int Value = 3; 

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
        if (col.tag == "Player")
        {
            //Stick to player
            transform.SetParent(col.transform);
        }
    }

    public void DisposeFromCollection()
    {
        Destroy(this.gameObject);
    }
}
