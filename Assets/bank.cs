using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bank : MonoBehaviour
{
    private int myCash = 0;
    public int Cash { get {return myCash;}}
    public int RequiredCash = 3;

    private Text myText; 


    void Start()
    {
         myText = this.gameObject.GetComponentInChildren<Text>();
         myText.text = $"${myCash} / {RequiredCash}";

        gameManager.Instance.RequiredCash = RequiredCash;

    }

    public void Deposit(int addValue){
        myCash += addValue;
        
        myText.text = $"${myCash} / {RequiredCash}";
        gameManager.Instance.RequiredCash = RequiredCash;
        gameManager.Instance.CurrentCash = myCash;

    } 
}
