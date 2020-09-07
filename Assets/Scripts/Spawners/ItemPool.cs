using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour {
    [SerializeField]
    GameObject[] itemPrefabs;
    Queue<GameObject> availableObjects = new Queue<GameObject>();
    int maxInstancesPerGrowth = 10;

    public static ItemPool Instance { get; private set; }

    void Awake () {
        Instance = this;
        GrowPool ();
    }

    void GrowPool () {
        for (int i = 0; i < maxInstancesPerGrowth; i++) {
            Debug.Log ("[AVAILABLE OBJECTS] " + availableObjects.Count.ToString ());
            var instance = Instantiate (itemPrefabs[Random.Range (0, itemPrefabs.Length)]);
            AddToPool (instance);
        }
    }

    public void AddToPool (GameObject instance) {
        instance.gameObject.SetActive (false);
        availableObjects.Enqueue (instance);
    }

    public GameObject GetFromPool () {
        if (availableObjects.Count == 0) {
            GrowPool ();
        }

        GameObject instance = availableObjects.Dequeue ();
        instance.gameObject.SetActive (true);
        return instance;
    }
}