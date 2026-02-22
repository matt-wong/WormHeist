using System;
using NUnit.Framework;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public int spawnID;
    private gameManager gm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = gameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gm != null)
        {
            gm.CurrentSpawnId = spawnID;

            // Animate color to white over 0.5 seconds
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white;
            }
        }
    }
}
