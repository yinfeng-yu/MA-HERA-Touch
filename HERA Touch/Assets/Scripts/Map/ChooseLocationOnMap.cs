using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChooseLocationOnMap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform targetVisual;
    [SerializeField] TextMeshProUGUI locationText;
    [SerializeField] CommandSender commandSender;

    [SerializeField] Camera roomCamera;
    [SerializeField] RenderTexture roomTexture;

    bool _touched = false;

    /// <summary>
    /// If the room size changes, change these parameters according to the new size
    /// </summary>
    int realWidth = 15;
    int realHeight = 8;
    float roomScale = 4f;

    float widthRatio = 1f;
    float heightRatio = 1f;

    float cameraSize;
    float ratio;

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        widthRatio = rectTransform.sizeDelta.x / (realWidth * roomScale);
        heightRatio = rectTransform.sizeDelta.y / (realHeight * roomScale);

        cameraSize = roomCamera.orthographicSize;
        ratio = rectTransform.sizeDelta.y / 2f / cameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (_touched)
        {
            targetVisual.position = Input.GetTouch(0).position;

            Vector2 visualRelativeLocation = targetVisual.position - rectTransform.position;
            Vector2 realLocation = visualRelativeLocation / ratio * roomScale;

            commandSender.targetLocation = realLocation;
            commandSender.screenLocation = Input.GetTouch(0).position;
            locationText.text = $"On picture: {visualRelativeLocation / ratio} \n" +
                "Choose the target location on the map \n" + $"{realLocation}";
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _touched = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _touched = false;
    }

}
