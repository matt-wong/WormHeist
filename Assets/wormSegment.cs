using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wormSegment : MonoBehaviour
{
    public bool IsVital = false;

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
        if (col.tag == "bullet")
        {
            if (IsVital)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }else{
                Destroy(gameObject);
            }
        }
    }
}

