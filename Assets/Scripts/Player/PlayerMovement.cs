using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update
    float speed = 5.0f;

    // Update is called once per frame
    void LateUpdate() {
        Move();
    }

    void Move() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction = Vector3.ClampMagnitude(direction, 1f);
        transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }
}
