using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject BulletObject;
    public GameObject LazerObject;
    private List<sensorLazer> lazers;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newLazer = Instantiate(this.LazerObject, this.transform.position, Quaternion.identity);
        newLazer.transform.localScale = new Vector3(10,1,0);
        sensorLazer lazerScript = newLazer.GetComponentInChildren<sensorLazer>();

        lazerScript.IsTriggered += () => {Invoke("ShootBulletLeft", 1);};
        //lazers.Add(lazerScript);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootBullet(Vector2 vector) {
        GameObject newBullet = Instantiate(this.BulletObject, this.transform.position + new Vector3(vector.normalized.x, vector.normalized.y, 0), Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        float angle = turret.Angle(vector);
        angle += Random.Range(-2, 2);

        Vector2 dtv = DegreeToVector2(angle).normalized;
        Debug.Log(dtv);
        rb.AddForce(dtv * 5000);
    }

    void ShootBulletLeft(){
        ShootBullet(Vector2.left);
        ShootBullet(Vector2.up);
        ShootBullet(Vector2.down);
        ShootBullet(Vector2.right);
    }

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
}
