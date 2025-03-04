using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] float CameraSpeed = 15f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + CameraSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.position = new Vector3(transform.position.x + CameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - CameraSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector3(transform.position.x - CameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
