using System;
using UnityEngine;

public class hideAwayCover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIDEAWAY COLLISION");
        Debug.Log(collision.gameObject.tag);
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (collision.gameObject.tag == "Player" && sr != null)
        {
            Debug.Log("HIDEAWAY COLLISION WITH PLAYER");
            sr.color = new Color(1f, 1f, 1f, 0.25f);
        }
    }
}
