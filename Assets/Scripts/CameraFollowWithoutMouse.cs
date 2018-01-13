﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithoutMouse : MonoBehaviour {
    public float cameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 FollowPos;
    public float clampAngle = 66.0f;
    public float inputSensitivity = 150.0f;
    public GameObject Cam;
    public GameObject Player;
    public float camDistanceXtoPlayer;
    public float camDistanceYtoPlayer;
    public float camDistanceZtoPlayer;
    public float mouseX;
    public float mouseY;

    public float smoothX;
    public float smoothY;
    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotationY = rot.y;
        rotationX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;// Disables movement of mouse press escape while in play mode to move freely
        Cursor.visible = false; // Disables the mouses visiblity when playing the game
    }
    private void Update()
    {
        mouseY = Input.GetAxis("Mouse X");
        mouseX = Input.GetAxis("Mouse Y");

        rotationY += mouseY * inputSensitivity * Time.deltaTime;
        rotationX += mouseX * inputSensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        transform.rotation = localRotation;
    }
    private void LateUpdate()
    {
        CameraUpdater();
    }
    private void CameraUpdater()
    {
        Transform target = CameraFollowObj.transform;
        //move toward the game object that is the target
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
