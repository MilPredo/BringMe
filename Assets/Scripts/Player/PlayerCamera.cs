using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public GameObject cube;
    void Start() {
        cube = GameObject.Find("Cube");
    }
    void Update() {
        transform.position = cube.transform.position + new Vector3(0, 10, -10);
    }


}