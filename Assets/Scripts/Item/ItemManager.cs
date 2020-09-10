using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemProperties { //contains unique properties of each item.
    public string name = "none"; //name of the item
    public string model; //model of the item
    string icon; //icon of the item
    int weight; //weight of the item
    public ItemProperties(string model) {
        this.model = model;
    }

    public ItemProperties(string name, string model, string icon, int weight) {
        this.name = name;
        this.model = model;
        this.icon = icon;
        this.weight = weight;
    }
}

class ItemObject { //the item object itself
    GameObject itemObject;
    ItemProperties properties;
    public ItemObject(GameObject itemObject, ItemProperties properties) {
        this.itemObject = itemObject;
        this.properties = properties;
        itemObject = GameObject.CreatePrimitive(Primitives(properties.model));
        //Add Components
        itemObject.AddComponent<Rigidbody>();
        itemObject.AddComponent<MeshFilter>();
        itemObject.AddComponent<BoxCollider>();
        itemObject.AddComponent<MeshRenderer>();
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


public class ItemManager : MonoBehaviour {
    //    List<ItemProperties> itemType = new List<ItemProperties>();
    //    List<ItemObject> itemObjects = new List<ItemObject>();
    // public ItemManager() {
    //     itemType.Add(new ItemProperties("cube"));
    //     itemType.Add(new ItemProperties("capsule"));
    // }
    public void Spawn(string model, Vector3 position) {
        // ItemProperties
        // objToSpawn = new GameObject("Cool GameObject made from Code");
        // //Add Components
        // objToSpawn.AddComponent<Rigidbody>();
        // objToSpawn.AddComponent<MeshFilter>();
        // objToSpawn.AddComponent<BoxCollider>();
        // objToSpawn.AddComponent<MeshRenderer>();
    }


}