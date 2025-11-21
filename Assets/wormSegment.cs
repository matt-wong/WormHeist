using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wormSegment : MonoBehaviour
{
    public bool IsVital = false;
    public int health = 3;

    private bool isInvulnerable = false;
    public float invulDuration = 1f;
    public float blinkInterval = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            if (IsVital)
            {
                if (isInvulnerable) return;

                this.health -= 1;
                Debug.Log($"VITAL WORM SEGMENT HIT. REMAINING HEALTH: {this.health}");
                if (this.health <= 0)
                {
                    Debug.Log("VITAL WORM SEGMENT DESTROYED.");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    StartCoroutine(TemporaryInvulnerability());
                }
            }
            else
            {
                Destroy(gameObject);
                // TODO: Spawn another worm
            }
        }
    }

    private IEnumerator TemporaryInvulnerability()
    {
        isInvulnerable = true;
        Collider2D col = GetComponent<Collider2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (col != null) col.enabled = false;

        if (sr != null)
        {
            float elapsed = 0f;
            bool visible = true;
            while (elapsed < invulDuration)
            {
                visible = !visible;
                sr.enabled = visible;
                yield return new WaitForSeconds(blinkInterval);
                elapsed += blinkInterval;
            }
            sr.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invulDuration);
        }

        if (col != null) col.enabled = true;
        isInvulnerable = false;
    }
}

