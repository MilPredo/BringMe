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

        public override void OnStartServer() {
            this.roundManager.StartFreezeTime(); // start game
        }
    }
}

