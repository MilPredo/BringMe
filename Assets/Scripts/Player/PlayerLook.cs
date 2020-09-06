using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    Vector3 direction;
    Vector3 lastDirection;
    Vector3 lookAt;
    // Update is called once per frame
    void Update() {
        LookAtMouse();
    }
    void Look() {
        //check latest axis
        direction.x = Input.GetAxis("Mouse X");
        direction.z = Input.GetAxis("Mouse Y");
        //check magnitude
        float mag = direction.magnitude;
        if (mag > 0f) {
            lastDirection = direction;
            Debug.Log(lastDirection);
        }
        transform.rotation = Quaternion.LookRotation(lastDirection);
    }

    void LookAtMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // create a plane at 0,0,0 whose normal points to +Y:
        Plane hPlane = new Plane(Vector3.up, -transform.position.y);
        // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
        float distance = 0;
        // if the ray hits the plane...
        if (hPlane.Raycast(ray, out distance)) {
            // get the hit point:
            lookAt = ray.GetPoint(distance);
            lookAt.y = transform.position.y;
        }
        transform.LookAt(lookAt);
    }
}
