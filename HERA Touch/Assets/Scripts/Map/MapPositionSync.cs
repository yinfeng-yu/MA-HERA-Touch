using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPositionSync : MonoBehaviour
{
    [SerializeField] Camera roomCamera;
    public RectTransform robodyVisual;
    public RectTransform map;

    int realWidth = 15;
    int realHeight = 8;
    float roomScale = 4f;

    float cameraSize;
    float ratio;

    // Start is called before the first frame update
    void Start()
    {
        cameraSize = roomCamera.orthographicSize;
        ratio = map.sizeDelta.y / 2f / cameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        // targetVisual.position = Input.GetTouch(0).position;
        // 
        // Vector2 visualRelativeLocation = targetVisual.position - rectTransform.position;
        // Vector2 realLocation = visualRelativeLocation / ratio * roomScale;
        // 
        // commandSender.targetLocation = realLocation;
        // commandSender.screenLocation = Input.GetTouch(0).position;
        // locationText.text = $"On picture: {visualRelativeLocation / ratio} \n" +
        //     "Choose the target location on the map \n" + $"{realLocation}";

        Vector3 robodyPosition = Vector3.zero;

        try
        {
            robodyPosition = GlobalVariableManager.Instance.GetVector3("robodyPosition");
            // Debug.Log(robodyPosition);
        }
        catch (Exception e)
        {

        }

        var realLocation = new Vector2(robodyPosition.x, robodyPosition.z);
        var visualRelativeLocation = realLocation * ratio / roomScale;
        var visualAbsoluteLocation = visualRelativeLocation + new Vector2(map.position.x, map.position.y);

        // Debug.Log($"robodyPosition: {robodyPosition}, realLocation: {realLocation}, visualRelLoc: {visualRelativeLocation}, visualAbsLoc: {visualAbsoluteLocation}");

        robodyVisual.position = visualAbsoluteLocation;
    }
}
