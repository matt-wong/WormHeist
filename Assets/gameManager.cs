using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }

    public int CurrentCash = 0;

    // example state you want to keep across reloads
    private HashSet<int> deactivatedPaywalls = new HashSet<int>();

    public int CurrentSpawnId = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // move player to the spawn point (GameObject with tag "PlayerSpawn")
        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");

        GameObject selectedSpawn = spawns.FirstOrDefault(
            s => s.GetComponent<spawnPoint>()?.spawnID == 0
        ); // default to first spawn point

        if (player != null && selectedSpawn != null)
        {
            player.transform.position = selectedSpawn.transform.position;
            // optional: also reset velocity if Rigidbody2D exists
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }

        // Optionally, notify scene objects about saved state:
        // e.g. find all paywalls and let them query GameManager.Instance.IsPaywallDeactivated(id)
    }

    // Example API for stored state
    public void DeactivatePaywall(int id) => deactivatedPaywalls.Add(id);
    public bool IsPaywallDeactivated(int id) => deactivatedPaywalls.Contains(id);
}