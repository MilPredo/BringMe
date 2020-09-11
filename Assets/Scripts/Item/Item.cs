using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/////////////
//OOP STUFF//
/////////////
/// <summary>
/// abstract class for Item.
/// </summary>

[CreateAssetMenu]
public class Item : ScriptableObject { //Base class for all items in the game (Bring me items, powerups, etc.)
    public string itemName;
    public GameObject prefab;
    public Sprite icon;
    public int weight;
    public int cooldown;
    //public abstract void Initialize(Vector3 position, Quaternion rotation);
}

// public class BringItem : Item { //additional properties unique to Bring me items.
//     // public override void Initialize(Vector3 position, Quaternion rotation) {
//     //     Instantiate(this.prefab, position, rotation);
//     // }
// }

// public abstract class Powerup : Item { //additional properties unique to powerup
// }





/////////////
//OOP STUFF//
/////////////
