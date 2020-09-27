using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class ArbiterDropZone : NetworkBehaviour {
        public Arbiter arbiter;

        void Awake() {
            arbiter = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Arbiter>();
        }

        void OnTriggerEnter(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a == null) return;
            if (a.lastTouch == null) return;
            Debug.Log(a.itemName + "Entered a Drop Zone");
            arbiter.itemsInDropZone.Add(a);
            Debug.Log(a.itemName + "Items in Drop Zone: " + arbiter.itemsInDropZone.Count);
        }

        void OnTriggerExit(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a == null) return;
            if (a.lastTouch == null) return;
            Debug.Log(a.itemName + "Exited a Drop Zone");
            arbiter.itemsInDropZone.Remove(a);
            Debug.Log(a.itemName + "Items in Drop Zone: " + arbiter.itemsInDropZone.Count);
        }
    }
}

