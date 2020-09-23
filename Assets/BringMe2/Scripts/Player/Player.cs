using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
namespace RummageBattle {
    [RequireComponent(typeof(CharacterController), typeof(NetworkTransform), typeof(NetworkIdentity))]
    public class Player : PlayerManager, IDamageable<float>, IEliminable, IFreezeable, ITeleportable {
        [SerializeField] [SyncVar] private string playerName;
        private const float maxHealth = 100f;
        [SyncVar] private float health;
        private Color color;
        [SyncVar]
        private float colorR, colorG, colorB;
        [SerializeField] private float speed = 20f;
        //private Item item;
        [SerializeField] private Powerup powerup;
        [SyncVar] public bool isReady = false;
        [SyncVar] public bool isFrozen = true;
        //misc
        [SerializeField] private TextMeshProUGUI playerNameUI;
        private Vector3 direction;
        private float directionY;
        private Vector3 camVel = Vector3.zero;

        private void Move() {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            direction = Vector3.ClampMagnitude(direction, 1f);
        }

        void Jump() {
            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<CharacterController>().isGrounded) {
                //player.velocity += Vector3.up * 10f;
                directionY = 2f;
            }
            directionY -= -Physics.gravity.y * Time.deltaTime;
            direction.y = directionY;
        }

        void LookAtMouse() {
            Vector3 lookAt = transform.forward;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane hPlane = new Plane(Vector3.up, -transform.position.y);
            float distance = 0;
            if (hPlane.Raycast(ray, out distance)) {
                lookAt = ray.GetPoint(distance);
                lookAt.y = transform.position.y;
            }
            transform.LookAt(lookAt);
        }

        void Start() {
            health = maxHealth;
            players.Add(GetComponent<Player>());
            colorR = Random.value;
            colorG = Random.value;
            colorB = Random.value;
        }

        void Update() {
            GetComponent<Renderer>().material.color = new Color(colorR, colorG, colorB);
            if (!isLocalPlayer) return;
            if (Input.GetMouseButtonDown(1)) {
                CmdSpawnPrefab();
            }

            Move();
            Jump();
            LookAtMouse();
        }

        void FixedUpdate() {
            if (!isLocalPlayer) return;
            GetComponent<CharacterController>().Move(direction * Time.deltaTime * speed);
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, transform.position + new Vector3(0, 10, -10), ref camVel, 0.1f);
        }

        void OnDestroy() {
            players.Remove(GetComponent<Player>());
        }

        public void SetColor(Color color) {
            this.color = color;
        }

        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation) {
            transform.rotation = rotation;
        }

        public void ApplyDamage(float amount) {
            health -= amount;
            if (health <= 0f) {
                Destroy(gameObject);
            }
        }

        public float GetHealth() {
            return health;
        }

        public void Eliminate() {
            Destroy(gameObject);
        }

        public void Freeze() {
            gameObject.SetActive(isFrozen = true);
        }

        public void UnFreeze() {
            gameObject.SetActive(isFrozen = false);
        }

        [SerializeField] private List<GameObject> hacc = new List<GameObject>();
        [SerializeField] private ItemManager itemManager;
        [Command] //call from client, run in server
        void CmdSpawnPrefab() {
            GameObject hax = itemManager.SpawnItem(hacc, transform.position, transform.rotation);
            if (hax == null) return;
            NetworkServer.Spawn(hax, transform.gameObject);
        }
    }
}