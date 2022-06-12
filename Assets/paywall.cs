using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paywall : MonoBehaviour
{

    private Collider2D wallCollider;
    public int RequiredCash;

    // Start is called before the first frame update
    void Start()
    {
        wallCollider = this.gameObject.GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.CurrentCash > RequiredCash){
            Debug.Log("OPEN");
            Destroy(wallCollider);
        };
    }
}
