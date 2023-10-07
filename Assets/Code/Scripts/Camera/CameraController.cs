using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;

    public float cameraOffset = 20f;

    public float Smoothness = 5f;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }


    private void LateUpdate()
    {
        var cameraPosition = new Vector3(camera.transform.position.x, cameraOffset, camera.transform.position.z);
        var targetPosition = new Vector3(Target.position.x, cameraPosition.y, Target.position.z);

        camera.transform.position = Vector3.Lerp(cameraPosition, targetPosition, Time.deltaTime * Smoothness);
    }
}