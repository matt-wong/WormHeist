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
    public double TiltX = 0;
    public double TiltY = 0;

    private readonly float TILT_SCALE = 1.5f;

    private void Start()
    {
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
        } else {
            this.SetDirectionFromMouse();
        }
    }

    private void SetTilt(Quaternion q)
    {
        // Get accelerometer data from the device.
        Vector3 accelerometerData = Input.acceleration;

        // Create a 2D vector based on the accelerometer data.
        Vector2 tiltVector = new Vector2(accelerometerData.x, accelerometerData.y + 0.75f);
        this.setMovementDirection(tiltVector);
    }

    private void setMovementDirection(Vector2 vect){
        double angle = (float)getAngle(vect);
        this.TiltX = 0;
        this.TiltY = 0;

        if (vect.magnitude > 0.1f)
        {
            // Normalize the vector to make it a unit vector.
            vect.Normalize();

            // Apply sensitivity to the tilt vector.
            vect *= TILT_SCALE;
            // These are read by movement script
            this.TiltX = vect.x;
            this.TiltY = vect.y;

            RectTransform rect_transform = this.dot.gameObject.GetComponentInChildren<RectTransform>();
            RawImage rawImage = this.dot.gameObject.GetComponentInChildren<RawImage>();
            rawImage.enabled = true;
            rect_transform.rotation = Quaternion.Euler(0, 0, (float)angle);
        }else {
            RawImage rawImage = this.dot.gameObject.GetComponentInChildren<RawImage>();
            rawImage.enabled = false;
        }
    }

    private void SetDirectionFromMouse(){
    // Find the GameObject with the playerMovement component
    GameObject playerObject = GameObject.Find("wormPlayer");
    if (playerObject != null) {
        playerMovement playerMov = playerObject.GetComponentInChildren<playerMovement>();
        if (playerMov != null) {
            Transform transform1 = playerMov.transform;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldPosition.x - transform1.position.x, worldPosition.y - transform1.position.y);
            this.setMovementDirection(direction);
        }
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