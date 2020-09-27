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
                targetItems = SelectRandomItems(3);
            } else {
                playerManager.UnFreezePlayers();
                StartRoundTime();
                //check dropzone items 
            }
        }

        private void CheckDropZone() {
            if (itemsInDropZone.Count == 0) return;
            foreach (Item item in itemsInDropZone) {
                if (targetItems.Contains(item)) {

                }
            }
        }

        private bool oneTime = false;
        private List<Item> SelectRandomItems(int count) {
            List<Item> items = new List<Item>();
            for (int i = 0; i < count; i++) {
                items.Add(SelectRandomItem());
            }
            oneTime = true;
            return items;
        }

        private Item SelectRandomItem() {
            int random = Random.Range(0, itemManager.items.Count);
            return itemManager.items[random];
        }

        public void OnGUI() { //originally from RoundManager script. may not look the same but the logic is based from RoundManager.cs
            uiManager.SetRoundUI(roundManager.CurrentRound);
            uiManager.SetRoundTimeUI(Mathf.CeilToInt(roundManager.CurrentRoundTime));
            uiManager.SetFreezeTimeUI(Mathf.CeilToInt(roundManager.CurrentFreezeTime));
            uiManager.SetFreezeRoundUI(Mathf.CeilToInt(roundManager.CurrentRound));
            uiManager.SetTargetCountUI(targetItems.Count);
            string targetItemsString = "";
            foreach (Item item in targetItems) {
                targetItemsString += item.itemName + "\n";
            }
            uiManager.SetTargetItemUI(
                "Target Items:\n" +
                targetItemsString
                );
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

