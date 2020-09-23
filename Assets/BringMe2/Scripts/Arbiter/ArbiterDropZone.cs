using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RummageBattle {
    public class ArbiterDropZone : Arbiter {
        void Start() {
        }

        void Update() {
        }

        void OnTriggerEnter(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a == null) return;
            Debug.Log(a.itemName + "Entered a Drop Zone");
            itemsInDropZone.Add(a);
        }

        void OnTriggerExit(Collider other) {
            Item a = other.gameObject.GetComponent<Item>();
            if (a == null) return;
            Debug.Log(a.itemName + "Exited a Drop Zone");
            itemsInDropZone.Remove(a);
        }
    }
}

