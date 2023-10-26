using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResetPoseButton : MonoBehaviour, IPointerDownHandler
{
    public Transform arCamera;
    public Quaternion rotation;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        Quaternion cameraRot = arCamera.rotation;
        if (Mathf.Abs(cameraRot.eulerAngles.x) < 45f && Mathf.Abs(cameraRot.eulerAngles.z) < 45f) 
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;  
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rotation = arCamera.rotation;

        Debug.Log($"eulers: {rotation}");
        // phone.Rotate(0, -eulers.y, 0);
        // phone.rotation = Quaternion.Euler(eulers.x, 0, eulers.z);
    }


}
