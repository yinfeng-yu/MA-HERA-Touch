using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    #region Singleton
    public static SwipeManager instance;
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }
    #endregion

    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;

    public bool _horizontal = false;

    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int _fastSwipeThresholdMaxLimit;

    

    // in draggging, when dragging started and where it started
    public bool _dragging;
    public bool _swipeDetected = false;

    private float _timeStamp;

    // private Vector2 _startPosition;

    private Vector2 _deltaPosition;



    public event Action swipeUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !_dragging)
        {
            _dragging = true;
            // _startPosition = Input.GetTouch(0).position;
            _timeStamp = Time.unscaledTime;
        }

        if (Input.touchCount > 0 && _dragging)
        {
            _deltaPosition += Input.GetTouch(0).deltaPosition;
            if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime && 
                Mathf.Abs(_horizontal ? _deltaPosition.x : -_deltaPosition.y) > fastSwipeThresholdDistance)
            {
                _swipeDetected = true;
            }
        }

        if (Input.touchCount == 0 && _dragging)
        {
            _dragging = false;

            // test for fast swipe - swipe that moves only +/-1 item
            if (_swipeDetected)
            {
                _swipeDetected = false;
                
                if (!_horizontal)
                {
                    if (_deltaPosition.y > 0)
                    {
                        swipeUp?.Invoke();
                    }
                    
                }
                _deltaPosition = Vector2.zero;

            }

        }
    }

}
