using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class ArbiterDropZone : NetworkBehaviour {
        public Arbiter arbiter;

        void Start() {
            arbiter = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Arbiter>();
        }

        void Update() {
        }

        void OnTriggerEnter(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a.lastTouch == null) return;
            if (a == null) return;
            Debug.Log(a.itemName + "Entered a Drop Zone");
            arbiter.itemsInDropZone.Add(a);
            Debug.Log(a.itemName + "Items in Drop Zone: " + arbiter.itemsInDropZone.Count);
        }

        void OnTriggerExit(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a.lastTouch == null) return;
            if (a == null) return;
            Debug.Log(a.itemName + "Exited a Drop Zone");
            arbiter.itemsInDropZone.Remove(a);
            Debug.Log(a.itemName + "Items in Drop Zone: " + arbiter.itemsInDropZone.Count);
        }
    }
}

