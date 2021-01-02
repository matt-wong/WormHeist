using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bank : MonoBehaviour
{
    private int myCash = 0;
    public int Cash { get {return myCash;}}
    public int RequiredCash = 3;

    public void Deposit(int addValue){
        myCash += addValue;
        if (myCash >= RequiredCash){
            gameManager.Instance.IsLevelCompleted = true;
        }
    } 
}
