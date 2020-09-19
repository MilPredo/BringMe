using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerController : NetworkBehaviour {
    float speed = 20.0f;
    float gravity = 9.81f;
    float directionY;
    public string playerName = "";
    [SerializeField] GameObject prefab;
    Vector3 lookAt;
    Vector3 direction;
    CharacterController player;
    GameObject pickedItem = null;
    Rigidbody pickedItemRB = null;
    RaycastHit hit = new RaycastHit();
    Color textCol;
    Vector3 camVel = Vector3.zero;

    [SerializeField] public TextMesh nameText;
    void Start() {
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        nameText.text = playerName.Length > 0 ? playerName : "netID: " + GetComponent<NetworkIdentity>().netId;
        player = GetComponent<CharacterController>();
    }

    void Update() {
        nameText.transform.rotation = Camera.main.transform.rotation;
        if (!isLocalPlayer) return;
        Color textCol = new Color(Random.value, Random.value, Random.value);
        nameText.color = textCol;
        Move();
        LookAtMouse();
        PickupItem();
        Jump();
        //Debug.Log(PrimitiveType.Plane);
    }

    void FixedUpdate() {
        if (!isLocalPlayer) return;
        player.Move(direction * Time.deltaTime * speed);
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, transform.position + new Vector3(0, 10, -10), ref camVel, 0.1f);
    }


    void Move() {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        direction = Vector3.ClampMagnitude(direction, 1f);
        //transform.Translate(direction * Time.deltaTime * speed, Space.World);
        //Camera.main.transform.position = transform.position + new Vector3(0, 10, -10);
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded) {
            //player.velocity += Vector3.up * 10f;
            directionY = 2f;
        }
        directionY -= -Physics.gravity.y * Time.deltaTime;
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
        //transform.GetChild(0).LookAt(lookAt);
        transform.LookAt(lookAt);
    }

    [Command] //call from client, run in server
    void CmdSpawnPrefab() {
        //Debug.Log(transform.position);
        GameObject spawn = Instantiate(prefab, transform.position, transform.rotation);
        NetworkServer.Spawn(spawn, transform.gameObject);
        Debug.LogError("Spawn Success");
    }

    Vector3 directionVec = Vector3.zero;
    Vector3 lastDirectionVec = Vector3.zero;
    Vector3 deltaDirVec = Vector3.zero;
    [Command]
    void CmdMoveItem() { //run server side movement.
        if (pickedItemRB != null) {
            Vector3 targetPoint = (transform.forward * 2f + transform.position + new Vector3(0, 1f, 0));
            Vector3 itemPosition = pickedItemRB.position;
            directionVec = (targetPoint - itemPosition);
            deltaDirVec = directionVec - lastDirectionVec;
            //float speedRB = Mathf.SmoothStep(0f, 300f, directionVec.magnitude / 5f);
            //speedRB *= Time.fixedDeltaTime;
            pickedItemRB.AddForce((directionVec * pickedItemRB.mass - (pickedItemRB.velocity * 0.1f)) * 20f);
            //pickedItemRB.AddForce(directionVec + (directionVec * Time.deltaTime) * 50f);
            lastDirectionVec = directionVec;
            //pickedItemRB.AddForce(directionVec.normalized * speed); //* Vector3.SqrMagnitude(direction));
            //pickedItemRB.AddForce(-pickedItemRB.velocity * Time.deltaTime, ForceMode.VelocityChange);
            //pickedItemRB.MovePosition(transform.GetChild(0).forward * 1.25f + transform.position);
            //pickedItemRB.MoveRotation(transform.GetChild(0).rotation);
            //RpcMoveItem();
        }
    }

    [ClientRpc]
    void RpcMoveItem() { //run a client side approximation of server side movement.
        if (pickedItemRB != null) {
            Vector3 targetPoint = (transform.forward * 2f + transform.position + new Vector3(0, 1f, 0));
            Vector3 itemPosition = pickedItemRB.position;
            directionVec = (targetPoint - itemPosition);
            deltaDirVec = directionVec - lastDirectionVec;
            //float speedRB = Mathf.SmoothStep(0f, 300f, directionVec.magnitude / 5f);
            //speedRB *= Time.fixedDeltaTime;
            pickedItemRB.AddForce((directionVec * pickedItemRB.mass - (pickedItemRB.velocity * 0.1f)) * 20f);
            //pickedItemRB.AddForce(directionVec + (directionVec * Time.deltaTime) * 50f);
            lastDirectionVec = directionVec;
            //pickedItemRB.AddForce(directionVec.normalized * speed); //* Vector3.SqrMagnitude(direction));
            //pickedItemRB.AddForce(-pickedItemRB.velocity * Time.deltaTime, ForceMode.VelocityChange);
            //pickedItemRB.MovePosition(transform.GetChild(0).forward * 1.25f + transform.position);
            //pickedItemRB.MoveRotation(transform.GetChild(0).rotation);
            //RpcMoveItem();
        }
    }

    [Command]
    void CmdSetItem() {
        if (hit.rigidbody != null) {
            pickedItem = hit.transform.gameObject;
            pickedItemRB = hit.rigidbody;
            //Debug.Log("ITEM: " + pickedItem);
        }
    }

    [Command]
    void CmdSphereCast() {
        Physics.SphereCast(transform.position, 1.0f, transform.forward, out hit, 4f);
    }

    void PickupItem() {
        CmdSphereCast();
        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

        if (Input.GetMouseButtonDown(1)) {
            //Debug.LogError("Pressed Right Click");
            CmdSpawnPrefab();
        }

        if (Input.GetMouseButtonDown(0)) { //press e, ray cast
            //Debug.LogError("Pressed Left Click");
            CmdSetItem();
        }

        if (Input.GetMouseButton(0)) { //press and hold e
            //Debug.LogError("Pressed and Hold Left Click");
            CmdMoveItem();
        }

        if (Input.GetMouseButtonUp(0)) {
            //Debug.LogError("Released Left Click");
            pickedItemRB = null;
        }
    }
}
