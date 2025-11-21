using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paywall : MonoBehaviour
{

    private Collider2D wallCollider;
    private SpriteRenderer spriteRenderer;
    public int RequiredCash;

    // Start is called before the first frame update
    void Start()
    {
        wallCollider = this.gameObject.GetComponentInChildren<Collider2D>();
        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    internal void SetActive(bool v)
    {
        Debug.Log("OPEN");
        Destroy(wallCollider);
        this.spriteRenderer.color = new Color(1f,1f,1f,0.25f);
    }
}
