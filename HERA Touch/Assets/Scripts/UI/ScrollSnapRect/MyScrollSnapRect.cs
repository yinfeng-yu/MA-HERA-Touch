using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class MyScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [Tooltip("Set starting page index - starting from 0")]
    public int startingPage = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;

    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int _fastSwipeThresholdMaxLimit;

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;

    // private bool _horizontal;
    
    // number of pages in container
    protected int _pageCount;
    protected int _currentPage;

    // whether lerping is in progress and target lerp position
    private bool _lerp = false;
    private Vector2 _lerpTo;

    // target position of every page
    private List<Vector2> _pagePositions = new List<Vector2>();

    // in draggging, when dragging started and where it started
    private bool _dragging;
    private float _timeStamp;
    private Vector2 _startPosition;

    public List<int> _offsetsX;
    private int _containerWidth;
    private int _containerSpacing;
    // private Vector2 _containerOriginalPos;

    //------------------------------------------------------------------------
    protected virtual void Start() {
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _pageCount = _container.childCount;

        _offsetsX = new List<int>();
        // EventManager.instance.taskButtonEvents.clicked += SetPagePositions;
        _containerSpacing = (int)_container.GetComponent<HorizontalLayoutGroup>().spacing;

        // is it horizontal or vertical scrollrect
        // if (_scrollRectComponent.horizontal && !_scrollRectComponent.vertical) {
        //     _horizontal = true;
        // } else if (!_scrollRectComponent.horizontal && _scrollRectComponent.vertical) {
        //     _horizontal = false;
        // } else {
        //     Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
        //     _horizontal = true;
        // }

        // init
        if (_pageCount > 0)
        {

            // SetPagePositions();
            // SetPage(startingPage);
        }
    }

    //------------------------------------------------------------------------
    protected virtual void Update() 
    {
        SetContentWidth();

        int selectedChild = ChildSelected();

        if (selectedChild != _container.childCount)
        {
            // A child is selected
            _lerpTo = new Vector2(_offsetsX[selectedChild], 0);
            _lerp = true;
        }
        else
        {
            _lerpTo = new Vector2(0, 0);
            _lerp = true;
        }

        

        // if moving to target position
        if (_lerp) 
        {
            // Debug.Log(ChildSelected());
            // Debug.Log(_lerpTo);
            // Debug.Log(_offsetsX[selectedChild]);
            //_container.anchoredPosition = _lerpTo;
            //_lerp = false;
            //_scrollRectComponent.velocity = Vector2.zero;


            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 1f) 
            {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }

            else
            {
                // prevent overshooting with values greater than 1
                float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
                // Debug.Log((_container.anchoredPosition, _lerpTo));
                _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            }
            

        }
    }

    public void UpdatePages(int newPageCount)
    {
        int focus_page = GetComponent<MyScrollSnapRect>()._currentPage - 1;
        if (newPageCount > _pageCount)
        {
            focus_page = newPageCount - 1;
        }
        GetComponentInChildren<MyScrollSnapRect>()._pageCount = newPageCount;

        if (newPageCount != 0)
        {
            // GetComponentInChildren<MyScrollSnapRect>().SetPagePositions();
            // LerpToPage(focus_page);
        }
        
    }

    private int ChildSelected()
    {
        for (int i = 0; i < _container.childCount; i++)
        {
            if (_container.GetChild(i).gameObject.GetComponent<Animator>().GetBool("selected"))
            {
                return i;
            }
        }
        return _container.childCount;
    }

    private void SetContentWidth()
    {
        _containerWidth = 40;
        _offsetsX.Clear();

        int width = (int)_scrollRectRect.rect.width;

        foreach (RectTransform child in _container)
        {
            int childWidth = (int)child.rect.width;
            int childOffset = (width - childWidth) / 2;

            _offsetsX.Add(-_containerWidth + childOffset);

            _containerWidth += childWidth;
            _containerWidth += _containerSpacing;
        }

        // set width of container
        Vector2 newSize = new Vector2(_containerWidth, _container.rect.height);
        _container.sizeDelta = newSize;

    }

    protected virtual void LerpToPosition(Vector2 offset, int pageIndex)
    {
        _lerpTo = offset;
        // Debug.Log(_lerpTo);
        _lerp = true;
        _currentPage = pageIndex;

    }


    //------------------------------------------------------------------------
    // private void SetPagePositions() {
    //     int width = 0;
    //     int height = 0;
    // 
    //     int offsetX = 0;
    // 
    //     int offsetY = 0;
    //     int containerWidth = 0;
    //     int containerHeight = 0;
    // 
    //     if (_horizontal) 
    //     {
    //         // screen width in pixels of scrollrect window
    //         width = (int)_scrollRectRect.rect.width;
    // 
    //         offsetX = width / 2;
    //         
    //         // total width
    //         containerWidth = width * _pageCount;
    //         // Debug.Log(containerWidth);
    //         // limit fast swipe length - beyond this length it is fast swipe no more
    //         _fastSwipeThresholdMaxLimit = width;
    //     } 
    //     else 
    //     {
    //         height = (int)_scrollRectRect.rect.height;
    //         offsetY = height / 2;
    //         containerHeight = height * _pageCount;
    //         _fastSwipeThresholdMaxLimit = height;
    //     }
    // 
    //     // set width of container
    //     Vector2 newSize = new Vector2(containerWidth, _container.rect.height);
    //     _container.sizeDelta = newSize;
    // 
    //     Vector2 newPosition = new Vector2(containerWidth / 2, 0);
    //     _container.anchoredPosition = newPosition;
    // 
    //     // delete any previous settings
    //     _pagePositions.Clear();
    // 
    //     // iterate through all container childern and set their positions
    //     for (int i = 0; i < _pageCount; i++) {
    //         RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
    //         Vector2 childPosition;
    //         if (_horizontal) 
    //         {
    //             childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
    //         } 
    //         else 
    //         {
    //             childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));
    //         }
    //         child.anchoredPosition = childPosition;
    //         _pagePositions.Add(-childPosition);
    //     }
    // }

    //------------------------------------------------------------------------
    // private void SetPage(int pageIndex) {
    //     pageIndex = Mathf.Clamp(pageIndex, 0, _pageCount - 1);
    //     _container.anchoredPosition = _pagePositions[pageIndex];
    //     _currentPage = pageIndex;
    // }

    //------------------------------------------------------------------------
    // protected virtual void LerpToPage(int pageIndex) {
    //     pageIndex = Mathf.Clamp(pageIndex, 0, _pageCount - 1);
    //     _lerpTo = _pagePositions[pageIndex];
    //     _lerp = true;
    //     _currentPage = pageIndex;
    // 
    // }





    //------------------------------------------------------------------------
    private void NextScreen()
    {
        // LerpToPage(_currentPage + 1);
    }

    //------------------------------------------------------------------------
    private void PreviousScreen()
    {
        // LerpToPage(_currentPage - 1);
    }

    //------------------------------------------------------------------------
    private int GetNearestPage() {
        // based on distance from current position, find nearest page
        Vector2 currentPosition = _container.anchoredPosition;
        
        float distance = float.MaxValue;
        int nearestPage = _currentPage;
        
        for (int i = 0; i < _pagePositions.Count; i++) {
            float testDist = Vector2.SqrMagnitude(currentPosition - _pagePositions[i]);
            if (testDist < distance) {
                distance = testDist;
                nearestPage = i;
            }
        }
        
        return nearestPage;
    }

    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData) {
        // if currently lerping, then stop it as user is draging
        // _lerp = false;
        // // not dragging yet
        // _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData) {
        // how much was container's content dragged
        // float difference;
        // if (_horizontal) {
        //     difference = _startPosition.x - _container.anchoredPosition.x;
        // } else {
        //     difference = - (_startPosition.y - _container.anchoredPosition.y);
        // }
        // 
        // // test for fast swipe - swipe that moves only +/-1 item
        // if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime &&
        //     Mathf.Abs(difference) > fastSwipeThresholdDistance &&
        //     Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit) {
        //     if (difference > 0) {
        //         NextScreen();
        //     } else {
        //         PreviousScreen();
        //     }
        // } else {
        //     // if not fast time, look to which page we got to
        //     LerpToPage(GetNearestPage());
        // }
        // 
        // _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData) {
        // if (!_dragging) 
        // {
        //     // dragging started
        //     _dragging = true;
        //     // save time - unscaled so pausing with Time.scale should not affect it
        //     _timeStamp = Time.unscaledTime;
        //     // save current position of cointainer
        //     _startPosition = _container.anchoredPosition;
        // }
    
    }
}
