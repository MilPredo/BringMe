using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class RaycastTest : NetworkBehaviour {
        public Punch powerup;
        public ItemManager itemManager;
        void HandleInputs() {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue)) {
                    IDamageable<float> target = hit.transform.GetComponent<IDamageable<float>>();
                    if (target != null) {
                        powerup.target = target;
                        powerup.Trigger();
                    }
                }
            }
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue)) {
                    if (hit.transform != null) {
                        SpawnPrefab(hit.transform.position);
                    }
                }
            }
        }

        [SerializeField] private List<GameObject> hacc = new List<GameObject>();
        void SpawnPrefab(Vector3 pos) {
            GameObject hax = itemManager.SpawnItem(hacc, pos, transform.rotation);
            if (hax == null) return;
            NetworkServer.Spawn(hax, transform.gameObject);
        }

        void Update() {
            HandleInputs();
        }
    }
}