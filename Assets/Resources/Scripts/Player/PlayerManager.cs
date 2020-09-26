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
            //Look for connections that are not in the player list
            foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned) {
                Player newPlayer = kvp.Value.GetComponent<Player>();

                //Add if new
                if (newPlayer != null && !players.Contains(newPlayer)) {
                    players.Add(newPlayer);
                }
            }

            players.ForEach(Debug.Log);
            // foreach (Player player in players) {
            //     Debug.Log("ID: " + player.GetComponent<NetworkIdentity>().netId);
            // }
        }

        public void FreezePlayers() {
            foreach (Player player in players) {
                player.Freeze();
            }
        }

        public void UnFreezePlayers() {
            foreach (Player player in players) {
                player.UnFreeze();
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