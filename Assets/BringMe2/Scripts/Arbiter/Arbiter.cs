using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace RummageBattle {
    public class Arbiter : NetworkBehaviour {
        private static List<Item> targetItems = new List<Item>();
        private static List<Player> winners = new List<Player>();
        public static List<Item> itemsInDropZone = new List<Item>();
        //RoundManager roundManager;
        //UIManager uiManager;
    }
}

