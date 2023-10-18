using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RangeSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Slider slider;
    float startY = 0;
    float startValue;
    float deltaY = 0;
    bool isSwiping = false;

    public float swipeSpeed = 600; // pixels needed to be swiped through to go from 0 to 1

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (isSwiping && Input.touches.Length > 0)
        {
            deltaY = Input.touches[0].position.y - startY;

            slider.value = Mathf.Clamp01(startValue + deltaY / swipeSpeed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSwiping = true;
        startY = eventData.position.y;
        startValue = slider.value;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        deltaY = 0;
        isSwiping = false;
    }

}
