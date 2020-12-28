using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bank : MonoBehaviour
{
    private int myCash = 0;
    public int Cash { get {return myCash;}}

    public void Deposit(int addValue){
        myCash += addValue;
    } 
}
