using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depositArea : MonoBehaviour
{

    private bank myBank;

    // Start is called before the first frame update
    void Start()
    {
        myBank = GetComponent<bank>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "coin")
        {
            coin asCoin = col.gameObject.GetComponent<coin>();
            if (asCoin){
                myBank.Deposit(asCoin.Value);
            }

            asCoin.DisposeFromCollection();
            Debug.Log(myBank.Cash);
        }
    }
}
