using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PickupManager : MonoBehaviour {

    private ArbiterManager arbiterManager;
    private RoundManager roundManager;
    private bool isTargetAcquired = false;
    private GameObject targetBearer;

    private void Start() {
        arbiterManager = gameObject.GetComponentInParent<ArbiterManager>();
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
    }

    private void OnTriggerEnter(Collider other) {
        Plog("detected object", other.gameObject.name);

        string targetItemName = arbiterManager.TargetItemName;
        if ( other.gameObject.name == targetItemName ) {
            Plog("object acquired", other.gameObject.name);
            isTargetAcquired = true;
            arbiterManager.ChangeTargetItem();
            Destroy(other.gameObject);
        } else {
            Plog("invalid object", other.gameObject.name);
            if ( other.gameObject.name == "Player" ) {
                Plog("player detected", other.gameObject.name);
                targetBearer = other.gameObject;
            }
        }

        if ( isTargetAcquired && targetBearer != null ) {
            Plog("round winner", targetBearer.gameObject.name);
            roundManager.StopRound();
        } else {
            targetBearer = null;
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
