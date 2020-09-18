using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PickupManager : MonoBehaviour {

    private ArbiterManager arbiterManager;
    private RoundManager roundManager;
    private GameObject targetBearer;
    private bool isItemAcquired = false;

    private void Start() {
        arbiterManager = gameObject.GetComponentInParent<ArbiterManager>();
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
    }

    private void OnTriggerEnter(Collider other) {
        Plog("detected object", other.gameObject.name);

        if ( this.isItemAcquired && other.tag.ToLower() == "player" )
            arbiterManager.PickUpItem(other.gameObject);
            this.isItemAcquired = false;

        string targetItemName = arbiterManager.TargetItemName;
        if (other.gameObject.name == targetItemName) {
            Plog("object acquired", other.gameObject.name);
            this.isItemAcquired = true;
            Destroy(other.gameObject);
        } else {
            Plog("invalid object", other.gameObject.name);
            if (other.gameObject.name == "Player") {
                Plog("player detected", other.gameObject.name);
                targetBearer = other.gameObject;
            }
        }
    }

    private void Plog(string titles, string msg) {
        var new_title = new StringBuilder();
        foreach (string t in titles.Split(',')) {
            new_title.Append($"[{ t }]");
        }
        Debug.Log($"{ new_title.ToString().ToUpper() } { msg.ToUpper() }");
    }
}
