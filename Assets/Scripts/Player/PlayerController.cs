using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float speed = 500.0f;
    Vector3 lookAt;
    Vector3 direction;
    Rigidbody player;
    void Start() {
        player = GetComponent<Rigidbody>();
    }

    void LateUpdate() {
        Move();
        LookAtMouse();
        PickupItem();
        //Debug.Log(PrimitiveType.Plane);
    }

    void FixedUpdate() {
        player.velocity = direction * Time.deltaTime * speed;
    }

    void Move() {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction = Vector3.ClampMagnitude(direction, 1f);
        //transform.Translate(direction * Time.deltaTime * speed, Space.World);
        Camera.main.transform.position = transform.position + new Vector3(0, 10, -10);
    }

    void LookAtMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, -transform.position.y);
        float distance = 0;
        if (hPlane.Raycast(ray, out distance)) {
            lookAt = ray.GetPoint(distance);
            lookAt.y = transform.position.y;
        }
        transform.LookAt(lookAt);
    }

    Transform item = null;
    void PickupItem() {
        //spherecast nearest object
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, transform.TransformDirection(Vector3.forward), out hit, 1f)) {
            if (Input.GetMouseButtonDown(0)) {
                item = hit.collider.gameObject.transform; //save
            }
            if (Input.GetMouseButton(0)) {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                item.GetComponent<Rigidbody>().MovePosition(transform.forward * 1.25f + transform.position);
            }
        }
    }

}
