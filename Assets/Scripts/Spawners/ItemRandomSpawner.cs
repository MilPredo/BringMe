using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawner : MonoBehaviour {

    [SerializeField]
    List<GameObject> spawnedItems = new List<GameObject>();
    [SerializeField]
    float minXSpawnPosition = -10f;
    [SerializeField]
    float maxXSpawnPosition = 10f;
    [SerializeField]
    float minZSpawnPosition = -10f;
    [SerializeField]
    float maxZSpawnPosition = 10f;
    [SerializeField]
    float spawnDelay = 2f;
    float lastSpawnTime;
    void Update() {
        
        if(Time.time - lastSpawnTime > spawnDelay) {
            SpawnItem();
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
        lastSpawnTime = Time.time;
        GameObject instance = ItemPool.Instance.GetFromPool();
        if (instance != null) {
            instance.transform.position = GetNewPosition();
        }
    }
}
