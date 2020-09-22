using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    [RequireComponent(typeof(NetworkIdentity), typeof(NetworkTransform), typeof(Rigidbody))]
    public class Item : MonoBehaviour, IMoveable {
        [SerializeField] private string itemName;
        [SerializeField] private int health;
        private Color color;
        public Player lastTouch { get; private set; }

        void Start() {
            color = new Color(Random.value, Random.value, Random.value);
            GetComponent<Renderer>().material.color = color;
        }

        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation) {
            transform.rotation = rotation;
        }
    }
}