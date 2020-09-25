using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class Powerup : NetworkBehaviour, ITeleportable, IInteractable {
        private string powerupName;
        private float cooldown = 10;
        public Player lastTouch;
        private static List<GameObject> powerups = new List<GameObject>(); //all powerups present in the current scene.
        public IDamageable<float> target;
        public bool isActive { get; private set; } = false;

        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation) {
            transform.rotation = rotation;
        }

        public void Interact() {

        }



        public virtual bool Trigger() {
            return false;
        }
    }
}