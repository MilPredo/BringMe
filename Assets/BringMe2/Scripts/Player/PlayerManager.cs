using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class PlayerManager : NetworkBehaviour {
        private NetworkManager networkManager;
        private int maxPlayers = 16;
        public static List<Player> players = new List<Player>();
        //private Arbiter arbiter;

        void Start() { //constructors not recommended

        }

        public void StartGame() {
            //Arbiter do something!
        }

        public bool IsPlayersReady() {
            foreach (Player player in players) {
                if (!player.isReady)
                    return false;
            }
            return true;
        }

        public bool IsServerFull() {
            return players.Count >= maxPlayers;
        }
    }
}