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
        GameObject newBullet = Instantiate(this.BulletObject, this.transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        vector  = vector.normalized;

        if (Random.Range(0,2) == 0){
            vector.x = vector.x + Random.Range(0f, 0.5f);
        }else{
            vector.y = vector.y + Random.Range(0f, 0.5f);
        }

        rb.AddForce(vector * 5000);
    }

    void ShootBulletLeft(){
        ShootBullet(Vector2.left);
    }


}
