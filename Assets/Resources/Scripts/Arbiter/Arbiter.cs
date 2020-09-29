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
                SelectRandomItems(3);
                StartRoundTime();
                //check dropzone items 
                CheckDropZone();
                if (targetItems.Count == 0) {
                    oneTime = false;
                    roundManager.StopRoundTime();
                }
            }
        }

        private void CheckDropZone() {
            if (itemsInDropZone.Count == 0) return;
            foreach (Item targetItem in targetItems) {
                foreach (Item itemInDropZone in itemsInDropZone) {
                    if (itemInDropZone.itemName == targetItem.itemName) {
                        targetItems.Remove(targetItem);
                        itemsInDropZone.Remove(itemInDropZone);
                        Debug.Log("Player: " + itemInDropZone.lastTouch.playerName + " Successfully dropped " + itemInDropZone.itemName + " into dropzone!");
                        itemInDropZone.ApplyDamage(100);
                        return;
                    }
                }
            }
        }

        private bool oneTime = false;
        private void SelectRandomItems(int count) {
            if (oneTime) return;
            //copy list
            List<Item> temp = new List<Item>(itemManager.items);
            for (int i = 0; i < count; i++) {
                int randIndex = Random.Range(0, temp.Count);
                targetItems.Add(temp[randIndex]);
                temp.RemoveAt(randIndex);
            }
            oneTime = true;
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

