using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField]
    List<GameObject> spawnedItems = new List<GameObject>();
    [SerializeField]
    int maxSpawnedItem = 25;
    [SerializeField]
    float minXSpawnPosition = -225f;
    [SerializeField]
    float maxXSpawnPosition = 225f;
    [SerializeField]
    float minZSpawnPosition = -225f;
    [SerializeField]
    float maxZSpawnPosition = 225f;

    ItemManager itemManager;

    string[] itemModels = new string[] {
        "cube", "capsule", "cylinder", "plane", "quad", "sphere"
    };

    public static ObjectSpawner Instance { get; private set; }

    void Awake() {
        Instance = this;
        itemManager = new ItemManager();
    }

    void Start() {
        // spawn specified(maxSpawnedItems) at the start of the game
        while (maxSpawnedItem > 0) {
            SpawnItem();
            maxSpawnedItem--;
        }
    }

    Vector3 GetNewPosition() {
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
        itemManager.Spawn(model, position);
    }

    public void DispawnItem(GameObject instance) {
        spawnedItems.Remove(instance);
        ItemPool.Instance.AddToPool(instance);
    }

    public void UpdateItemPosition(GameObject instance) {
        int index = 0;
        foreach (GameObject item in spawnedItems) {
            if (instance == item) {
                Debug.Log(item.gameObject.transform.position);
                break;
            }
            index++;
        }
    }
}
