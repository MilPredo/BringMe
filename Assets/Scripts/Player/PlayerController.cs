using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerController : NetworkBehaviour {
    float speed = 20.0f;
    float gravity = 9.81f;
    float directionY;
    [SerializeField] GameObject prefab;
    Vector3 lookAt;
    Vector3 direction;
    CharacterController player;
    Transform pickedItem = null;
    Rigidbody rb = null;
    RaycastHit hit;
    void Start() {
        player = GetComponent<CharacterController>();
    }

    void Update() {
        CmdSphereCast();
        if (!isLocalPlayer) return;
        Move();
        LookAtMouse();
        PickupItem();
        Jump();
        //Debug.Log(PrimitiveType.Plane);
    }

    void FixedUpdate() {
        if (!isLocalPlayer) return;
        player.Move(direction * Time.deltaTime * speed);
    }

    void Move() {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction = Vector3.ClampMagnitude(direction, 1f);
        //transform.Translate(direction * Time.deltaTime * speed, Space.World);
        Camera.main.transform.position = transform.position + new Vector3(0, 10, -10);
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded) {
            //player.velocity += Vector3.up * 10f;
            directionY = 2f;
        }
        directionY -= gravity * Time.deltaTime;
        direction.y = directionY;
    }

    void LookAtMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, -transform.position.y);
        float distance = 0;
        if (hPlane.Raycast(ray, out distance)) {
            lookAt = ray.GetPoint(distance);
            lookAt.y = transform.position.y;
        }
        //player.MoveRotation(Quaternion.LookRotation(lookAt, Vector3.up));
        transform.LookAt(lookAt);
    }



    [Command] //call from client, run in server
    void CmdSpawnPrefab() {
        Debug.Log(transform.position);
        GameObject spawn = Instantiate(prefab, transform.position, transform.rotation);
        NetworkServer.Spawn(spawn);
    }

    [Command]
    void CmdMoveItem() {
        rb.MovePosition(transform.forward * 1.25f + transform.position);
    }

    [Command]
    void CmdGetRigidBody() {
        try {
            pickedItem = hit.collider.gameObject.transform; //save
            rb = pickedItem.GetComponent<Rigidbody>();
        } catch (System.Exception) {
        }
    }

    [Server]
    void CmdSphereCast() {
        if (Physics.SphereCast(transform.position, 0.5f, transform.TransformDirection(Vector3.forward), out hit, 4f)) {
            //isHit = true;
        }
    }

    void PickupItem() {
        if (Input.GetMouseButtonDown(1)) {
            CmdSpawnPrefab();
        }
        if (Input.GetMouseButtonDown(0)) {
            CmdGetRigidBody();
        }
        if (Input.GetMouseButton(0) && (pickedItem != null) && (rb != null)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            CmdMoveItem();
        }
        if (Input.GetMouseButtonUp(0)) {
            pickedItem = null;
            rb = null;
        }
    }

}
