using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class gyroscopeTracker : MonoBehaviour
{
    public Text myText;
    public GameObject dot;

    public bool isEnabled = false;

    public float CURSOR_MOVEMENT_SCALE = 600;
    public double tiltAngle = 0;
    public double tiltX = 0;
    public double tiltY = 0;

    private readonly float TILT_SCALE = 1.5f;

    private void Start()
    {
        // myText = GetComponent<Text>();
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            this.isEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isEnabled){
            this.SetTilt(Input.gyro.attitude);
        }
    }

    private void SetTilt(Quaternion q)
    {

        // Get accelerometer data from the device.
        Vector3 accelerometerData = Input.acceleration;

        // Create a 2D vector based on the accelerometer data.
        Vector2 tiltVector = new Vector2(accelerometerData.x, accelerometerData.y + 0.75f);

        // Normalize the vector to make it a unit vector.
        tiltVector.Normalize();

        // Apply sensitivity to the tilt vector.
        tiltVector *= TILT_SCALE;

        double angle = (float)getAngle(tiltVector);
        this.tiltX = tiltVector.x;
        this.tiltY = tiltVector.y;

        this.tiltAngle = angle;

        if (tiltVector.magnitude > 0.5f)
        {
            RectTransform rect_transform = this.dot.gameObject.GetComponentInChildren<RectTransform>();
            RawImage rawImage = this.dot.gameObject.GetComponentInChildren<RawImage>();
            rawImage.enabled = true;
            rect_transform.rotation = Quaternion.Euler(0, 0, (float)angle);
        }else {
            RawImage rawImage = this.dot.gameObject.GetComponentInChildren<RawImage>();
            rawImage.enabled = false;
        }
    }

    public double getAngle(Vector2 vector2)
    {

        if (vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(-vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(-vector2.x, vector2.y) * Mathf.Rad2Deg;
        }
    }

}