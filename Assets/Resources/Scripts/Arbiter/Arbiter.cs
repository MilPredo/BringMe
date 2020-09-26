using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class Arbiter : NetworkBehaviour {
        public List<Item> targetItems = new List<Item>();
        public List<Player> winners = new List<Player>();
        public List<Item> itemsInDropZone = new List<Item>();
        public RoundManager roundManager;
        public UIManager uiManager;
        public PlayerManager playerManager;
        public ItemManager itemManager;

        void Update() {
            //check if players is ready

            //start rounds
            if (!roundManager.freezeTimeFinished) {
                playerManager.FreezePlayers();
                StartFreezeTime();
            } else {
                playerManager.UnFreezePlayers();
                StartRoundTime();
            }
        }

        private Item SelectRandomItem() {
            return itemManager.items[Random.Range(0, itemManager.items.Count)];
        }

        public void OnGUI() { //originally from RoundManager script. may not look the same but the logic is based from RoundManager.cs
            uiManager.SetRoundUI(roundManager.CurrentRound);
            uiManager.SetRoundTimeUI(Mathf.RoundToInt(roundManager.CurrentRoundTime));
            uiManager.SetFreezeTimeUI(Mathf.RoundToInt(roundManager.CurrentFreezeTime));
            uiManager.SetFreezeRoundUI(Mathf.RoundToInt(roundManager.CurrentRound));
        }

        private bool StartFreezeTime() { //originally from FreezeTimer script. may not look the same but the logic is based from FreezeTimer.cs
            uiManager.HideRoundLayout();
            uiManager.ShowFreezeTimeLayout();
            return roundManager.StartFreezeTime();
        }

        private bool StartRoundTime() { //originally from RoundTimer script. may not look the same but the logic is based from RoundTimer.cs
            uiManager.ShowRoundLayout();
            uiManager.HideFreezeTimeLayout();
            return roundManager.StartRoundTime();
        }

        public override void OnStartServer() {
            //wait for players
            // if (playerManager.IsPlayersReady()) {
            //     roundManager.StartFreezeTime(); // start game
            // }
        }
    }
}

