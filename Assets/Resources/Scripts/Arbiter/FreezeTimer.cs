using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RummageBattle {

    public class FreezeTimer : MonoBehaviour {

        private bool isRunning = false;
        private float lastTimerUpdate = 0f;

        [SerializeField] RoundManager roundManager;

        private void Awake() {
            this.roundManager = gameObject.GetComponent<RoundManager>();
        }

        private void Update() {
            this.lastTimerUpdate += Time.deltaTime;
            if (this.isRunning && this.roundManager.CurrentFreezeTime > 0) {
                if (this.lastTimerUpdate > 1f) {
                    
                    this.roundManager.DecreaseFreezeTime();
                    this.lastTimerUpdate = 0f;

                    if (this.roundManager.CurrentFreezeTime == 0) {
                        this.StopFreezeTimer();
                        if (this.roundManager.CurrentRound <= this.roundManager.MaxRound)
                            this.roundManager.StartRoundTime();
                    }
                }
            }
        }

        public void StartFreezeTimer() {
            this.isRunning = true;
        }

        public void StopFreezeTimer() {
            this.isRunning = false;
        }
    }
}

