using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    [RequireComponent(typeof(NetworkIdentity), typeof(NetworkTransform), typeof(Rigidbody))]
    public class Item : NetworkBehaviour, ITeleportable, IDamageable<float> {
        [SerializeField] public string itemName;
        private const float maxHealth = 100f;
        [SerializeField] [SyncVar] private float health;
        private Color color;
        [SyncVar]
        private float colorR, colorG, colorB;
        public Player lastTouch { get; private set; }

        void Start() {
            colorR = Random.value;
            colorG = Random.value;
            colorB = Random.value;
        }

        void Update() {
            color = new Color(colorR, colorG, colorB);
            GetComponent<Renderer>().material.color = color;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }


        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation) {
            transform.rotation = rotation;
        }

        public void ApplyDamage(float damage) {
            health -= damage;
            Debug.Log(itemName + " received " + damage + " damage\nRemaining Health: " + health);
        }

        public float GetHealth() {
            return health;
        }
    }
}