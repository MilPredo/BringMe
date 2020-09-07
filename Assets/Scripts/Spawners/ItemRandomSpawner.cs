using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawner : MonoBehaviour {

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
    [SerializeField]

    void Start() {
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
        return spawnPosition;
    }

    void SpawnItem() {
        GameObject instance = ItemPool.Instance.GetFromPool();
        if (instance != null) {
            instance.transform.position = GetNewPosition();
        }
    }
}
