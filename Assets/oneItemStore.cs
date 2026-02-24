using UnityEngine;

public class oneItemStore : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;
    public int quantityRemaining = 1;

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

        if (itemToSpawn != null && this.quantityRemaining > 0){
            asCoin.DisposeFromCollection();
            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
            quantityRemaining--;
        }
    }
}
