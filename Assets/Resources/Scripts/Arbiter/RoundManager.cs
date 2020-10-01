using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace RummageBattle {
    public class RoundManager : NetworkBehaviour {
        //private RoundTimer roundTimer;
        //private FreezeTimer freezeTimer;

        private int maxRound = 3;
        private float maxRoundTime = 60.0f;
        private float maxFreezeTime = 5.0f;

        [SyncVar] private int currentRound = 1;
        [SyncVar] private float currentRoundTime;
        [SyncVar] private float currentFreezeTime;
        [SyncVar] private bool isActive = false; //is the RoundManager doing something?
        [SyncVar] private bool isFreezeTimerActive = false;
        [SyncVar] private bool isRoundTimerActive = false;
        //[SerializeField] UIManager uiManager;

        public int MaxRound {
            get { return maxRound; }
        }

        public int CurrentRound {
            get { return currentRound; }
        }

        public float CurrentRoundTime {
            get { return currentRoundTime; }
        }

        public float CurrentFreezeTime {
            get { return currentFreezeTime; }
        }

        public bool IsActive {
            get { return isActive; }
        }

        public bool IsFreezeTimerActive {
            get { return isFreezeTimerActive; }
        }

        public bool IsRoundTimerActive {
            get { return isRoundTimerActive; }
        }

        void Start() {
            currentRoundTime = maxRoundTime;
            currentFreezeTime = maxFreezeTime;
        }

        void Update() {
            //moved timers in one Update() function and use if statements to control which timer to use.
            isActive = isFreezeTimerActive || isRoundTimerActive;
            if (isFreezeTimerActive && isRoundTimerActive) {
                Debug.LogError("Error! Freeze Timer and Round Timer cannot be active at the same time!");
                return;
            }

            if (isFreezeTimerActive) {
                StartFreezeTimer();
            }

            if (isRoundTimerActive) {
                StartRoundTimer();
            }

            if (currentRound == maxRound) {
                //Display scoreboard
            }
        }

        [HideInInspector] public bool freezeTimeFinished = false;
        public bool StartFreezeTime() {
            isFreezeTimerActive = true;
            return freezeTimeFinished;
        }

        [HideInInspector] public bool roundTimeFinished = false;
        public bool StartRoundTime() {
            isRoundTimerActive = true;
            return roundTimeFinished;
        }

        public void StopRoundTime() {
            currentRoundTime = maxRoundTime;
            isRoundTimerActive = false;
            roundTimeFinished = true;
            freezeTimeFinished = false;
            currentRound++;
        }

        private void StartFreezeTimer() {
            if (currentRound <= maxRound) {
                freezeTimeFinished = false;
                currentFreezeTime -= Time.unscaledDeltaTime;
                if (currentFreezeTime <= 0) {
                    currentFreezeTime = maxFreezeTime;
                    isFreezeTimerActive = false;
                    freezeTimeFinished = true;
                }
            }
        }

        private void StartRoundTimer() {
            if (currentRound <= maxRound) {
                roundTimeFinished = false;
                currentRoundTime -= Time.unscaledDeltaTime;
                if (currentRoundTime <= 0) {
                    StopRoundTime();
                }
            }
        }
    }
}


