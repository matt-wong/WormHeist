using UnityEngine;

public static class VectorHelper{
    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }

    public static eSensorDirection AngleToDirectionEnum(float angle){
        if (angle == 0){
            return eSensorDirection.N;
        }else if (angle == 90){
            return eSensorDirection.W;
        }else if (angle == 180){
            return eSensorDirection.S;
        }else if (angle == 270){
            return eSensorDirection.E;
        }
        return eSensorDirection.N;
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
        return new Vector2(0, 0);
    }
}
