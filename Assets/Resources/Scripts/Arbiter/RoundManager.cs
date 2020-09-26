using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace RummageBattle {
    public class RoundManager : NetworkBehaviour {
        private RoundTimer roundTimer;
        private FreezeTimer freezeTimer;

        private int maxRound = 3;
        private int maxRoundTime = 5;
        private int maxFreezeTime = 5;
        
        [SyncVar] private int currentRound = 1;
        [SyncVar] private int currentRoundTime = 5;
        [SyncVar] private int currentFreezeTime = 5;
        [SyncVar] private bool isActive = false; //is the RoundManager doing something?

        [SerializeField] UIManager uiManager;

        public int MaxRound {
            get { return this.maxRound; }
        }

        public int CurrentRound {
            get { return currentRound; }
        }

        public int CurrentRoundTime {
            get { return currentRoundTime; }
        }

        public int CurrentFreezeTime {
            get { return currentFreezeTime; }
        }

        public bool IsActive {
            get { return isActive; }
        }

        public void StartGame() {
            this.StartFreezeTime();
            this.isActive = true;
        }

        public void StartFreezeTime() {
            this.currentFreezeTime = this.maxFreezeTime;

            this.uiManager.HideRoundLayout();
            this.uiManager.ShowFreezeTimeLayout();
            this.freezeTimer.StartFreezeTimer();
        }

        public void StartRoundTime() {
            this.currentRoundTime = this.maxRoundTime;

            this.uiManager.ShowRoundLayout();
            this.uiManager.HideFreezeTimeLayout();
            this.roundTimer.StartRoundTimer();
        }

        public void IncreaseRound() {
            this.currentRound += 1;
        }

        public void DecreaseCurrentRoundTime() {
            this.currentRoundTime -= 1;
        }

        public void DecreaseFreezeTime() {
            this.currentFreezeTime -= 1;
        }

        public void OnGUI() {
            this.uiManager.SetRoundUI(this.currentRound);
            this.uiManager.SetRoundTimeUI(this.currentRoundTime);
            this.uiManager.SetFreezeTimeUI(this.currentFreezeTime);
            this.uiManager.SetFreezeRoundUI(this.currentRound);
        }

        public override void OnStartServer() {
            this.roundTimer = gameObject.GetComponent<RoundTimer>();
            this.freezeTimer = gameObject.GetComponent<FreezeTimer>();

            // put inside OnStartServer() of Arbiter()
            // this.roundManager.StartGame();
            this.StartGame(); // remove this if Arbiter() is used to start game.
        }
    }
}


