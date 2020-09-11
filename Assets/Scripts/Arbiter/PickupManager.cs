using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {

    private ArbiterManager arbiterManager;

    private void Start() {
        arbiterManager = gameObject.GetComponentInParent<ArbiterManager>();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"Object in Pickup Area: { other.gameObject.name }");

        string targetItemName = arbiterManager.TargetItemName;
        if ( other.gameObject.name == targetItemName ) {
            Debug.Log("Target Acquired.");
            arbiterManager.ChangeTargetItem();
            Destroy(other.gameObject);
        } else {
            Debug.Log("Invalid Target");
        }
    }
}
