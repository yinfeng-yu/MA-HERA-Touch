using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GrabButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public UnityEvent onPointerDownEvent;
    [SerializeField] public UnityEvent onPointerUpEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDownEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUpEvent.Invoke();
    }
}
