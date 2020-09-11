using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public Item[] a;
    public void Spawn(int index, Vector3 position, Quaternion rotation) {
        Instantiate(a[index].prefab, position, rotation);
    }
}
