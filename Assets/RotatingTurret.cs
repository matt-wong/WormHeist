using UnityEngine;

public class RotatingTurret : TurretBase
{
    [Tooltip("Degrees per second clockwise (negative = counter-clockwise)")]
    public float rotateSpeedDegPerSec = 90f;

    public float lazerLength = 5f;

    private GameObject lazerObject;
    private sensorLazer lazerSensor;
    private float currentAngleDeg;

    protected override void InitializeTurret()
    {
        lazerObject = CreateLazerOfDirection(0f, true, lazerLength);
        lazerSensor = lazerObject.GetComponentInChildren<sensorLazer>();
        currentAngleDeg = 0f;
    }

    void Update()
    {
        if (lazerObject == null || lazerSensor == null) return;

        currentAngleDeg += rotateSpeedDegPerSec * Time.deltaTime;
        if (currentAngleDeg >= 360f) currentAngleDeg -= 360f;
        if (currentAngleDeg < 0f) currentAngleDeg += 360f;

        lazerObject.transform.position = transform.position;
        lazerObject.transform.rotation = Quaternion.Euler(0f, 0f, currentAngleDeg);
        lazerSensor.direction = VectorHelper.AngleToDirectionEnum(currentAngleDeg);
    }

    protected override void ShootBulletDirection(eSensorDirection direction)
    {
        // Match Unity's rotation: 0° = right (1,0), 90° = up (0,1). VectorHelper.DegreeToVector2 uses 0° = up, so use (cos, sin) instead.
        // The lazer is rotated 90° from the turret, so we need to add 90° to the current angle.
        Vector2 shootVector = VectorHelper.DegreeToVector2(currentAngleDeg + 90f);
        Debug.Log("Shoot vector: " + shootVector);
        StartCoroutine(ShootAfterDelay(shootVector, ShootDelay));

        // After shooting wait 1 second and build another lazer
        Invoke(nameof(RecreateLazer), 1);
    }

    private void RecreateLazer()
    {
        // Destroy the current lazer
        Destroy(lazerObject);
        Destroy(lazerSensor);
        // Create a new lazer
        this.lazerLength += 2;
        lazerObject = CreateLazerOfDirection(0f, true, lazerLength);
        lazerSensor = lazerObject.GetComponentInChildren<sensorLazer>();
    }
}
