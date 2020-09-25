using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class PlayerManager : NetworkBehaviour {
        private NetworkManager networkManager;
        [SerializeField] private int maxPlayers = 2;
        public List<Player> players = new List<Player>();

        void Start() { //constructors not recommended

        }

        void Update() {
            // foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned)
            foreach (Player player in players) {
                Debug.Log("ID: " + player.GetComponent<NetworkIdentity>().netId);
            }
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