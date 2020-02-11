using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    Camera cam;
    public float zoomSpeed = 1f;
    private void Awake() {
        cam = GetComponent<Camera>();
    }
    private void Update() {
        if (Input.GetKey(KeyCode.O))
            cam.fieldOfView *= 1f - Time.deltaTime * zoomSpeed;
        if (Input.GetKey(KeyCode.I))
            cam.fieldOfView *= 1f + Time.deltaTime * zoomSpeed;
    }
}
