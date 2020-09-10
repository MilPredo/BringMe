using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/////////////
//OOP STUFF//
/////////////
/// <summary>
/// abstract class for Item.
/// </summary>

abstract class Item : ScriptableObject { //Base class for all items in the game (Bring me items, powerups, etc.)
    public string itemName = "";
    public string model = "";
    public Sprite icon = null;
    public abstract void Initialize(GameObject obj); //override then call this function to spawn
}

abstract class BringItem : Item { //additional properties unique to Bring me items.
    public int weight = 0;
}

abstract class Powerup : Item { //additional properties unique to powerup
    public int cooldown = 0;
    public abstract void Trigger();
}

//Item objects
[CreateAssetMenu]
class BringItemObject : BringItem { //class used to implement BringItem objects
    public override void Initialize(GameObject obj) {
        // ItemProperties
        obj = new GameObject(this.itemName);
        // //Add Components
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<BoxCollider>();
        obj.AddComponent<MeshRenderer>();
    }
    PrimitiveType Primitives(string model) {
        switch (model) {
            case "capsule":
                return PrimitiveType.Capsule;
            case "cube":
                return PrimitiveType.Cube;
            case "cylinder":
                return PrimitiveType.Cylinder;
            case "plane":
                return PrimitiveType.Plane;
            case "quad":
                return PrimitiveType.Quad;
            case "sphere":
                return PrimitiveType.Sphere;
            default:
                return PrimitiveType.Cube;
        }
    }
}
[CreateAssetMenu]
class PowerupObject : Powerup { //class used to implement Powerup objects
    public override void Initialize(GameObject obj) {

    }
    public override void Trigger() { //triggers the powerup

    }
}


/////////////
//OOP STUFF//
/////////////
