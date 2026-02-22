using UnityEngine;

/// <summary>Angle and vector helpers using Unity's convention: 0° = right (+X), 90° = up (+Y), angles increase counter-clockwise.</summary>
public static class VectorHelper
{
    /// <summary>Returns angle in degrees [0, 360). 0° = right (1,0), 90° = up (0,1).</summary>
    public static float Angle(Vector2 p_vector2)
    {
        float deg = Mathf.Atan2(p_vector2.y, p_vector2.x) * Mathf.Rad2Deg;
        return deg < 0f ? deg + 360f : deg;
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    /// <summary>0° → right (1,0), 90° → up (0,1). Matches Unity's Transform rotation (Euler Z).</summary>
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    /// <summary>Maps Unity angle to direction enum: 0°=E, 90°=N, 180°=W, 270°=S.</summary>
    public static eSensorDirection AngleToDirectionEnum(float angle)
    {
        if (angle == 0f) return eSensorDirection.E;
        if (angle == 45f) return eSensorDirection.NE;
        if (angle == 90f) return eSensorDirection.N;
        if (angle == 135f) return eSensorDirection.NW;
        if (angle == 180f) return eSensorDirection.W;
        if (angle == 225f) return eSensorDirection.SW;
        if (angle == 270f) return eSensorDirection.S;
        if (angle == 315f) return eSensorDirection.SE;
        return eSensorDirection.E;
    }

    public static Vector2 DirectionEnumToVector(eSensorDirection direction)
    {
        if (direction == eSensorDirection.N)
        {
            return new Vector2(0, 1);
        }
        else if (direction == eSensorDirection.W)
        {
            return new Vector2(-1, 0);
        }
        else if (direction == eSensorDirection.S)
        {
            return new Vector2(0, -1);
        }
        else if (direction == eSensorDirection.E)
        {
            return new Vector2(1, 0);
        }
        else if (direction == eSensorDirection.NW)
        {
            return new Vector2(-1, 1).normalized;
        }
        else if (direction == eSensorDirection.NE)
        {
            return new Vector2(1, 1).normalized;
        }
        else if (direction == eSensorDirection.SW)
        {
            return new Vector2(-1, -1).normalized;
        }
        else if (direction == eSensorDirection.SE)
        {
            return new Vector2(1, -1).normalized;
        }
        return new Vector2(0, 0);
    }
}
