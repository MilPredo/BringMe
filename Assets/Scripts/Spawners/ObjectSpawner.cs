using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    [SerializeField] private int maxSpawnedItem = 25;  // maximum number of items that can be spawned

    [SerializeField] private float minXSpawnPosition = -50;  // min x spawn boundary

    [SerializeField] private float maxXSpawnPosition = 50;  // max x spawn boundary

    [SerializeField] private float minZSpawnPosition = -50;  // min z spawn boundary

    [SerializeField] private float maxZSpawnPosition = 50;  // max z spawn boundary

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
        float x = Random.Range(minXSpawnPosition, maxXSpawnPosition);
        float y = Random.Range(1, 2);
        float z = Random.Range(minZSpawnPosition, maxZSpawnPosition);
        Vector3 spawnPosition = new Vector3(x, y, z);
        return spawnPosition;
    }

    void SpawnItem() {
        // using item manager to spawn items
        GameObject.Find("GameManager")
            .GetComponent<ItemManager>()
            .Spawn(Random.Range(0, 4), GetNewPosition(), Quaternion.identity);
    }
}
