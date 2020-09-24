using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace RummageBattle {
    public class RoundManager : NetworkBehaviour {
        private int maxRound = 3;
        private int maxRoundTime = 120;
        private int maxFreezeTime = 15;
        [SyncVar] private int currentRound = 1;
        [SyncVar] private int currentRoundTime = 120;
        [SyncVar] private int currentFreezeTime = 15;
        [SyncVar] private bool isActive = false; //is the RoundManager doing something?

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

        public void StartFreezeTime() {
            //Do something

        }

        public void StartRoundTime() {
            //Do something

        }
    }
}


