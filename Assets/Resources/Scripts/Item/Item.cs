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
        [SerializeField] private GameObject healthBarGameObject;
        [SerializeField] private HealthBar healthBar;

        void Start() {
            colorR = Random.value;
            colorG = Random.value;
            colorB = Random.value;
            healthBarGameObject = Instantiate((GameObject)Resources.Load("Prefabs/HealthBar"));
            healthBar = healthBarGameObject.GetComponent<HealthBar>();
        }

        void Update() {
            color = new Color(colorR, colorG, colorB);
            if (healthBar != null) {
                healthBar.SetPosition(transform.position);
                healthBar.SetHealth(health);
            }
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
            healthBar.GetComponent<HealthBar>().SetHealth(health);
            Debug.Log(itemName + " received " + damage + " damage\nRemaining Health: " + health);
        }

        public float GetHealth() {
            return health;
        }
    }
}