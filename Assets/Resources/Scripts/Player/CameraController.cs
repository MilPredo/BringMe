using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float zoom = 10f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 10f;
    private float targetZoom = 10f;
    private Vector3 targetPosition;
    private float time = 0.1f;

    void Start() {
        transform.eulerAngles = new Vector3(0f, 45f, 0f);
        //Camera.main.transform.localRotation = Quaternion.Euler(45f, 0f, 0f);
        Debug.Log(Camera.main.transform.worldToLocalMatrix.MultiplyVector(transform.forward));
        Camera.main.transform.localPosition = -transform.worldToLocalMatrix.MultiplyVector(Camera.main.transform.forward) * zoom;
    }

    void Update() {
        Zoom();
        Rotate();
    }

    public Vector2 camRot = new Vector2(45f, 45f);
    public Vector2 targetCamRot = new Vector2(45f, 45f);
    public Vector2 rotVel = Vector2.zero;
    private void Rotate() {
        if (Input.GetMouseButton(2)) {
            targetCamRot += new Vector2(-Input.GetAxis("Mouse Y") * 8f, Input.GetAxis("Mouse X") * 8f);
        }
        if (targetCamRot.x < 20f) targetCamRot.x = 20f;
        if (targetCamRot.x > 90f) targetCamRot.x = 90f;
        camRot = Vector2.SmoothDamp(camRot, targetCamRot, ref rotVel, time);
        transform.eulerAngles = camRot;
    }

    private float vel = 0f;
    private void Zoom() {
        if (targetZoom < minZoom) {
            targetZoom = minZoom;
        }

        if (targetZoom > maxZoom) {
            targetZoom = maxZoom;
        }

        targetZoom -= targetPosition.magnitude / 8f * Input.mouseScrollDelta.y;
        targetPosition = -Camera.main.transform.forward * targetZoom;
        zoom = Mathf.SmoothDamp(zoom, targetZoom, ref vel, time);
        Camera.main.transform.localPosition = -transform.worldToLocalMatrix.MultiplyVector(Camera.main.transform.forward) * zoom; //-Camera.main.transform.forward * zoom;

    }
}