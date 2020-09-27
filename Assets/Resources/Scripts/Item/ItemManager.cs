using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class ItemManager : NetworkBehaviour {
        public int maxWorldItems = 500;
        public List<Item> items = new List<Item>(); //items in world

        void Update() {
            Debug.Log(items.Count + "/" + maxWorldItems);
        }

        public GameObject SpawnItem(List<GameObject> spawnableItems, Vector3 position, Quaternion rotation) {
            if (spawnableItems.Count == 0) return null;
            if (items.Count >= maxWorldItems) return null;
            GameObject toSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            GameObject spawned = Instantiate(toSpawn, position, rotation);
            NetworkServer.Spawn(spawned);
            items.Add(spawned.GetComponent<Item>());
            return spawned;
        }
    }
}