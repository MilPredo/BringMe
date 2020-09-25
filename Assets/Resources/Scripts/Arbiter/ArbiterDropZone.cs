using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class ArbiterDropZone : NetworkBehaviour {
        public Arbiter arbiter;

        void Start() {
        }

        void Update() {
        }

        void OnTriggerEnter(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a == null) return;
            Debug.Log(a.itemName + "Entered a Drop Zone");
            arbiter.itemsInDropZone.Add(a);
        }

        void OnTriggerExit(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a == null) return;
            Debug.Log(a.itemName + "Exited a Drop Zone");
            arbiter.itemsInDropZone.Remove(a);
        }
    }
}

