using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RummageBattle {
    public class ItemManager : MonoBehaviour {
        public static int maxWorldItems { get; private set; }
        public static List<Item> items = new List<Item>();
        public static GameObject SpawnItem(List<GameObject> spawnableItems, Vector3 position, Quaternion rotation) {
            if (spawnableItems.Count == 0) return null;
            GameObject toSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            GameObject spawned = Instantiate(toSpawn, position, rotation);
            items.Add(spawned.GetComponent<Item>());
            return spawned;
        }
    }
}