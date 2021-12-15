using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager Instance;
    private bool myIsLevelCompleted = false;
    public Action<bool> LevelCompletedEvent;

    public bool IsLevelCompleted
    {
        get { return myIsLevelCompleted; }
        set
        {

            myIsLevelCompleted = value;
            Debug.Log("Level Done!");
            LevelCompletedEvent.Invoke(myIsLevelCompleted);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null){
            Destroy(gameObject);
        }else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
