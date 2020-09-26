using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RummageBattle {

    public class RoundTimer : MonoBehaviour {

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
        //     if (roundManager.CurrentRoundTime > 0) {
        //         if (lastTimerUpdate > 1f) {
        //             roundManager.DecreaseCurrentRoundTime();
        //             lastTimerUpdate = 0f;
        //             if (roundManager.CurrentRoundTime == 0) {
        //                 StopRoundTimer();
        //                 if (roundManager.CurrentRound < roundManager.MaxRound) {
        //                     roundManager.IncreaseRound();
        //                     roundManager.StartFreezeTime();
        //                 }
        //             }
        //         }
        //         Debug.Log($"Round Timer: isRunning({isRunning})");
        //     }
        // }

        public void StartRoundTimer() {
            isRunning = true;
        }

        public void StopRoundTimer() {
            isRunning = false;
        }
    }
}
