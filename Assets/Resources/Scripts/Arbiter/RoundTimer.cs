using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RummageBattle {
    
    public class RoundTimer : MonoBehaviour {

        private bool isRunning = false;
        private float lastTimerUpdate = 0f;

        [SerializeField] RoundManager roundManager;

        private void Awake() {
            this.roundManager = gameObject.GetComponent<RoundManager>();
        }

        private void Update() {
            this.lastTimerUpdate += Time.deltaTime;
            if (this.isRunning == true && this.roundManager.CurrentRoundTime > 0) {
                if (this.lastTimerUpdate > 1f) {
                    this.roundManager.DecreaseCurrentRoundTime();
                    this.lastTimerUpdate = 0f;

                    if (this.roundManager.CurrentRoundTime == 0) {
                        this.StopRoundTimer();
                        
                        if (this.roundManager.CurrentRound < this.roundManager.MaxRound)  {
                            this.roundManager.IncreaseRound();
                            this.roundManager.StartFreezeTime();
                        }
                    }

                }
                Debug.Log($"Round Timer: isRunning({this.isRunning})");
            }
        }

        public void StartRoundTimer() {
            this.isRunning = true;
        }

        public void StopRoundTimer() {
            this.isRunning = false;
        }
    }
}
