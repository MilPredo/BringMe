using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        ItemManager a = new ItemManager();
        a.Spawn("cube", new Vector3(1f, 1f, 1f));
    }

}
