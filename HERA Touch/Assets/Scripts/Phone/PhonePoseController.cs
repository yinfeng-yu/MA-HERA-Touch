using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePoseController : MonoBehaviour
{
    public SensorsReader sensorsReader;
    public ResetPoseButton resetPoseButton;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = sensorsReader.ReadOrientation();

        transform.rotation = Quaternion.Euler(Vector3.up * -resetPoseButton.rotation.eulerAngles.y) * transform.rotation;

        transform.rotation *= Quaternion.Euler(Vector3.right * 90f);

        transform.position = Vector3.zero; // startPos  - resetPoseButton.position + (Quaternion.Euler(Vector3.up * -resetPoseButton.rotation.eulerAngles.y) * ARCamera.position);
    }
}
