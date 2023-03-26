using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyroscopeTracker : MonoBehaviour
{
    private Text myText;


    private void Start()
    {
        myText = GetComponent<Text>();

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.supportsGyroscope) {
            GyroToUnity(Input.gyro.attitude);
        } else {
            this.myText.text = $"'HELLO!?' = {Random.Range(-10.0f, 10.0f)}";
        }
    }

    private Quaternion GyroToUnity (Quaternion q)
    { 

        this.myText.text = $"X = {string.Format("{0:0.##}", q.x)} \n Y = {q.y} \n -Z = {-q.z} \n -W = {-q.w}";
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}