using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!player){
            playerMovement playerGObj = GameObject.FindObjectOfType<playerMovement>();
            this.player = playerGObj.gameObject.transform;
            
        }
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -3); // Camera follows the player with specified offset position
    }
}
