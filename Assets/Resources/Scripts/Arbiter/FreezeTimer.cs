using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RummageBattle {

    public class FreezeTimer : MonoBehaviour {

        private bool isRunning = false;
        private float lastTimerUpdate = 0f;

        [SerializeField] RoundManager roundManager;

        private void Awake() {
            roundManager = gameObject.GetComponent<RoundManager>();
        }

        //Transferred to RoundManager, script no longer needed

        // private void Update() {
        //     if (!isRunning) return;
        //     lastTimerUpdate += Time.deltaTime;
        //     if (roundManager.CurrentFreezeTime > 0) {
        //         if (lastTimerUpdate > 1f) {
        //             roundManager.DecreaseFreezeTime();
        //             lastTimerUpdate = 0f;
        //             if (roundManager.CurrentFreezeTime == 0) {
        //                 StopFreezeTimer();
        //                 if (roundManager.CurrentRound <= roundManager.MaxRound)
        //                     roundManager.StartRoundTime();
        //             }
        //         }
        //     }
        // }

        public void StartFreezeTimer() {
            isRunning = true;
        }

        public void StopFreezeTimer() {
            isRunning = false;
        }
    }
}

