using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RummageBattle {
    public class Punch : Powerup {
        [SerializeField] private float damage = 10;
        [SerializeField] private float duration = 1f;
        private float currentDuration = 0;
        //public IDamageable<float> target;
        private bool isTriggered = false;

        void Update() {
            if (target == null) return;
            if (!isTriggered) return;
            currentDuration += Time.deltaTime;
            if (currentDuration >= duration) {
                Debug.Log("Duration: " + currentDuration);
                isTriggered = false;
                currentDuration = 0;
            }
        }

        public override bool Trigger() {
            Debug.Log("Triggered");
            if (!isTriggered) {
                target.ApplyDamage(damage);
                if (target.GetHealth() <= 0) {
                    target = null;
                }
            }
            isTriggered = true;
            return true;
        }
    }
}

