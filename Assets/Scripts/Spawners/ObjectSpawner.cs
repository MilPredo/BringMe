using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ObjectSpawner : NetworkBehaviour {
    [SerializeField]
    private int maxSpawnedItem = 25;  // maximum number of items that can be spawned

    [SerializeField]
    private float minSpawnRadius = 10;

    [SerializeField]
    private float maxSpawnRadius = 10;



    public static ObjectSpawner Instance { get; private set; }

    private void Awake() {
        Instance = this;  // create a variable containing an instance of this object
        //itemManager = new ItemManager();  // create an instance of the `ItemManager()`
    }

    private void Start() {
        // spawn specified(maxSpawnedItems) at the start of the game
        while (maxSpawnedItem > 0) {
            SpawnItem();
            // reduced maxSpawnedItem for item spawned
            maxSpawnedItem--;
        }
    }

    Vector3 GetNewPosition() {
        // randomize location of the spawned item
        Vector2 random = Random.insideUnitCircle * (maxSpawnRadius - minSpawnRadius);
        random = random + random.normalized * minSpawnRadius;

        Vector3 spawnPosition = new Vector3(random.x, 1f, random.y);
        return spawnPosition;
    }

    void SpawnItem() {
        // using item manager to spawn items
        GameObject.Find("GameManager")
            .GetComponent<ItemManager>()
            .Spawn(Random.Range(0, 4), GetNewPosition(), Quaternion.identity).GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}
