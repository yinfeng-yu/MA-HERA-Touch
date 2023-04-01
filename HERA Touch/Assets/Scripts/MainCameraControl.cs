using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    // This script is for receiving signals and react to them.
    // Not for actively controlling the camera
    [Tooltip("How fast will camera lerp to target position")]
    public float decelerationRate = 10f;


    public Vector3 cameraSettingsOffset = new Vector3(8, 0, 0);
    public Vector3 cameraTaskSelectedOffset = new Vector3(0, 0, -2);
    public Vector3 cameraTaskBubbleSelectedOffset = new Vector3(0, 24, -7f);

    private bool _cameraLerp;
    private Vector3 _cameraLerpTo;

    private Vector3 _cameraOriginPos;
    private Vector3 _cameraSettingsPos;
    private Vector3 _cameraTaskSelectedPos;
    private Vector3 _cameraTaskBubbleSelectedPos;

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.instance.taskButtonEvents.selected += OnTaskButtonSelected;
        EventManager.instance.taskBubbleEvents.selected += OnTaskBubbleSelected;
        EventManager.instance.settingsEntered += OnSettingsEntered;

        _cameraOriginPos = Camera.main.transform.position;
        _cameraTaskSelectedPos = _cameraOriginPos + cameraTaskSelectedOffset;
        _cameraTaskBubbleSelectedPos = _cameraOriginPos + cameraTaskBubbleSelectedOffset;
        _cameraSettingsPos = _cameraOriginPos + cameraSettingsOffset;
    }

    private void Update()
    {
        if (_cameraLerp)
        {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _cameraLerpTo, decelerate);
            // time to stop lerping?
            if (Vector3.SqrMagnitude(Camera.main.transform.position - _cameraLerpTo) < 0.01f)
            {
                // snap to target and stop lerping
                Camera.main.transform.position = _cameraLerpTo;
                _cameraLerp = false;
            }
        }
    }

    private void OnTaskButtonSelected(bool selected)
    {
        _cameraLerpTo = selected ? _cameraTaskSelectedPos : _cameraOriginPos;
        _cameraLerp = true;
    }

    private void OnTaskBubbleSelected(bool selected)
    {
        _cameraLerpTo = selected ? _cameraTaskBubbleSelectedPos : _cameraOriginPos;
        _cameraLerp = true;
    }

    void OnSettingsEntered(bool enter)
    {
        _cameraLerpTo = enter ? _cameraSettingsPos : _cameraOriginPos;
        _cameraLerp = true;
    }
}
