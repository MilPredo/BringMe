using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class ItemSpawner : NetworkBehaviour {
        [SerializeField] private ItemManager itemManager;
        [SerializeField] private int maxLocalItems;
        [SerializeField] private List<GameObject> spawnableItems = new List<GameObject>();
        [Header("Spawn Mode: 0-3")]
        [SerializeField] private int spawnMode = 0;
        [Header("Mode 0")]
        [SerializeField] private float x = 5;
        [SerializeField] private float z = 5;
        [Header("Mode 1")]
        [SerializeField] private float length = 5;
        [Header("Mode 2")]
        [SerializeField] private float min = 5;
        [SerializeField] private float max = 5;
        [Header("Mode 2")]
        [SerializeField] private float r = 5;

        public static ItemSpawner Instance { get; private set; }

        private void Awake() {
            Instance = this;  // create a variable containing an instance of this object
                              //itemManager = new ItemManager();  // create an instance of the `ItemManager()`
        }

        public override void OnStartServer() {
            switch (spawnMode) {
                case 0:
                    SpawnItemsRectArea(x, z);
                    break;
                case 1:
                    SpawnItemsRectArea(length);
                    break;
                case 2:
                    SpawnItemsRadius(min, max);
                    break;
                case 3:
                    SpawnItemsRadius(r);
                    break;
                default:
                    SpawnItemsRectArea(x, z);
                    break;
            }
        }


        public void SpawnItemsRectArea(float a, float b) {
            Vector3 RandomPosition() {
                float x = Random.Range(-(a / 2f), a / 2f);
                float z = Random.Range(-(b / 2f), b / 2f);
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                return spawnPosition + transform.position;
            }

            for (int i = 0; i < maxLocalItems; i++) {
                itemManager.SpawnItem(spawnableItems, RandomPosition(), Random.rotation);
            }
        }

        public void SpawnItemsRectArea(float length) {
            Vector3 RandomPosition() {
                float x = Random.Range(-length / 2f, length / 2f);
                float z = Random.Range(-length / 2f, length / 2f);
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                return spawnPosition + transform.position;
            }

            for (int i = 0; i < maxLocalItems; i++) {
                itemManager.SpawnItem(spawnableItems, RandomPosition(), Random.rotation);
            }
        }

        public void SpawnItemsRadius(float min, float max) {
            Vector3 RandomPosition() {
                Vector2 random = Random.insideUnitCircle * (max - min);
                random = random + random.normalized * min;
                Vector3 spawnPosition = new Vector3(random.x, 0f, random.y);
                return spawnPosition + transform.position;
            }

            for (int i = 0; i < maxLocalItems; i++) {
                itemManager.SpawnItem(spawnableItems, RandomPosition(), Random.rotation);
            }
        }

        public void SpawnItemsRadius(float r) {
            Vector3 RandomPosition() {
                Vector2 random = Random.insideUnitCircle * r;
                Vector3 spawnPosition = new Vector3(random.x, 0f, random.y);
                return spawnPosition + transform.position;
            }

            for (int i = 0; i < maxLocalItems; i++) {
                itemManager.SpawnItem(spawnableItems, RandomPosition(), Random.rotation);
            }
        }

    }
}