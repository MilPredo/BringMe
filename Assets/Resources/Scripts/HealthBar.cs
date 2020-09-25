using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
namespace RummageBattle {
    public class HealthBar : MonoBehaviour {
        private GameObject canvas;
        private Slider healthBar;
        // Start is called before the first frame update
        void Start() {
            canvas = GameObject.FindGameObjectWithTag("MainCanvas");
            healthBar = GetComponentInChildren<Slider>();
            if ((canvas == null) || (healthBar == null)) {
                Debug.LogError("No canvas in the scene!");
                Destroy(gameObject);
                return;
            }
            transform.SetParent(canvas.transform);
        }

        public void SetPosition(Vector3 position) {
            transform.position = Camera.main.WorldToScreenPoint(position);
        }

        public void SetHealth(float health) {
            healthBar.value = health;
            if (health <= 0) Destroy(gameObject);
        }
    }
}