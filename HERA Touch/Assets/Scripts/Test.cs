using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform ARCamera;
    public ResetPoseButton resetPoseButton;
    public Vector3 angles;
    public Vector3 rotation;

    public Vector3 startPos;
    public float posScale = 10f;
    private void Start()
    {
        startPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = SensorDataSender.ARCameraToObject(Camera.main.transform.rotation);
        // transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y - resetPoseButton.eulers.y, Camera.main.transform.rotation.z);
        transform.rotation = ARCamera.rotation;
        transform.rotation = Quaternion.Euler(Vector3.up * -resetPoseButton.rotation.eulerAngles.y) * transform.rotation;

        transform.position = startPos  - resetPoseButton.position * posScale + (Quaternion.Euler(Vector3.up * -resetPoseButton.rotation.eulerAngles.y) * ARCamera.position) * posScale;
        // transform.Rotate(0, 0, -resetPoseButton.eulers.z);
    }
}
