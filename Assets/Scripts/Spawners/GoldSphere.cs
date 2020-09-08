
using UnityEngine;
using System.Collections;

public class GoldSphere : MonoBehaviour
{

    void Awake() {
        Debug.Log($"[{gameObject.name}] Awake");
    } 

    void OnEnable() {
        Debug.Log($"[{gameObject.name}] Enabled");
    }

    void OnDisable() {
        Debug.Log($"[{gameObject.name}] Disabled");
        ObjectSpawner.Instance.DispawnItem(gameObject);
    }
}
