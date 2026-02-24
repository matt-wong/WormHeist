using UnityEngine;

public class oneItemStore : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;

    void Start()
    {
    }

    void Update()
    {
    }

    // If in contact with a coin, destroy it and spawn an item
    void OnTriggerEnter2D(Collider2D col)
    {
        coin asCoin = col.gameObject.GetComponent<coin>();
        if (asCoin == null) return;

        asCoin.DisposeFromCollection();
        if (itemToSpawn != null)
            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
    }
}
