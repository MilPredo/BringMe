using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace RummageBattle {
    
    public class RoundManager : NetworkBehaviour {
        private int maxRound = 3;
        private int maxRoundTime = 120;
        private int maxFreezeTime = 15;
        private int currentRound = 1;
        private int currentRoundTime = 120;
        private int currentFreezeTime = 15;

        public void StartRound() {
            
        }
    }
}


