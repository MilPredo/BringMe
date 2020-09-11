using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] private List<GameObject> spawnedItems = new List<GameObject>();  // keeps track of all spawned items

    [SerializeField] private int maxSpawnedItem = 25;  // maximum number of items that can be spawned

    [SerializeField] private float minXSpawnPosition = -225f;  // min x spawn boundary

    [SerializeField] private float maxXSpawnPosition = 225f;  // max x spawn boundary

    [SerializeField] private float minZSpawnPosition = -225f;  // min z spawn boundary

    [SerializeField] private float maxZSpawnPosition = 225f;  // max z spawn boundary

    //private ItemManager itemManager;  // instance of `ItemManager`

    // list of all possible items that can be spawned
    private string[] itemModels = new string[] {
        "cube", "capsule", "cylinder", "plane", "quad", "sphere"
    };

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

        // check if position is already occupied
        foreach (GameObject item in spawnedItems) {
            Vector3 position = item.transform.position;
            // position is occupied
            if (spawnPosition == position) {
                // only return a location that is not occupied
                Debug.Log("Spawn position occupied");
                spawnPosition = GetNewPosition();
            }
        }
        return spawnPosition;
    }

    void SpawnItem() {
        // GameObject instance = ItemPool.Instance.GetFromPool();
        // if (instance != null) {
        //     Vector3 position = GetNewPosition();
        //     spawnedItems.Add(instance); // add item to spawnedlist
        //     instance.transform.position = position;
        // }

        // using item manager to spawn items
        string model = itemModels[Random.Range(0, itemModels.Length)];
        Vector3 position = GetNewPosition();
        //itemManager.Spawn(model, position);
    }

    public void DispawnItem(GameObject instance) {
        // dispawned items gets removed from the list of spawnedItems
        spawnedItems.Remove(instance);
        // destroy object
        Destroy(instance);
        maxSpawnedItem += 1;
        // ItemPool.Instance.AddToPool(instance);
    }
}
