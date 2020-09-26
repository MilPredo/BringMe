using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace RummageBattle {
    public class RoundManager : NetworkBehaviour {
        //private RoundTimer roundTimer;
        //private FreezeTimer freezeTimer;

        private int maxRound = 3;
        private float maxRoundTime = 5.0f;
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

        private void StartFreezeTimer() { //originally from FreezeTimer script. may not look the same but the logic is based from FreezeTimer.cs
            if (currentRound <= maxRound) { //check remaining rounds
                freezeTimeFinished = false;
                currentFreezeTime -= Time.deltaTime;
                //Debug.Log($"Freeze Timer: {currentFreezeTime}");
                if (currentFreezeTime <= 0) { //check if timer reached zero
                    currentFreezeTime = maxFreezeTime; //reset timer
                    isFreezeTimerActive = false; //stop countdown
                    freezeTimeFinished = true;
                }
            }
        }

        private void StartRoundTimer() { //originally from RoundTimer script. may not look the same but the logic is based from RoundTimer.cs
            if (currentRound <= maxRound) { //check remaining rounds
                roundTimeFinished = false;
                currentRoundTime -= Time.deltaTime;
                //Debug.Log($"Round Timer: {currentRoundTime}");
                if (currentRoundTime <= 0) { //check if timer reached zero
                    currentRoundTime = maxRoundTime; //reset timer
                    isRoundTimerActive = false; //stop countdown
                    roundTimeFinished = true;
                    freezeTimeFinished = false;
                    currentRound++;
                }
            }
        }

        // public void IncreaseRound() {
        //     currentRound += 1;
        // }

        // public void DecreaseCurrentRoundTime() {
        //     currentRoundTime -= 1;
        // }

        // public void DecreaseFreezeTime() {
        //     currentFreezeTime -= 1;
        // }

        // public void OnGUI() {
        //     uiManager.SetRoundUI(currentRound);
        //     uiManager.SetRoundTimeUI(Mathf.RoundToInt(currentRoundTime));
        //     uiManager.SetFreezeTimeUI(Mathf.RoundToInt(currentFreezeTime));
        //     uiManager.SetFreezeRoundUI(currentRound);
        // }

        // public override void OnStartServer() {
        //     //roundTimer = gameObject.GetComponent<RoundTimer>();
        //     // freezeTimer = gameObject.GetComponent<FreezeTimer>();

        //     // put inside OnStartServer() of Arbiter()
        //     // roundManager.StartGame();
        //     //StartGame(); // remove this if Arbiter() is used to start game.
        // }
    }
}


