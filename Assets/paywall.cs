using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paywall : MonoBehaviour
{
    public int paywallId;

    private Collider2D wallCollider;
    private SpriteRenderer spriteRenderer;
    public int RequiredCash;

    // Start is called before the first frame update
    void Start()
    {
        wallCollider = this.gameObject.GetComponentInChildren<Collider2D>();
        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (gameManager.Instance != null && gameManager.Instance.IsPaywallDeactivated(paywallId))
            SetActive(true);
    }

    internal void SetActive(bool v)
    {
        Debug.Log("OPEN");
        Destroy(wallCollider);
        this.spriteRenderer.color = new Color(1f,1f,1f,0.25f);
// hide the bank associated with this paywall
        GameObject bank = GameObject.FindAnyObjectByType<bank>().gameObject;
        if (bank != null && bank.GetComponent<bank>().payWall.paywallId == paywallId)
        {
            bank.SetActive(false);
        }

    }
}
