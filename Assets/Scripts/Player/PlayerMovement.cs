using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update
    float speed = 5.0f;

    // Update is called once per frame
    void Update() {
        Move();
    }

    void Move() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
