using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMenuScrollSnapRect : ScrollSnapRect
{
    // public SwipeMenuToggleGroup swipeMenuToggleGroup;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // swipeMenuToggleGroup.OnChange += GoToPage;
    }


    public void GoToPage(int index)
    {
        LerpToPage(index);
    }

    protected override void LerpToPage(int pageIndex)
    {
        base.LerpToPage(pageIndex);

        // swipeMenuToggleGroup.SetToggleOn(pageIndex);
        // EventManager.Instance.PageChanged();

        // We are at the last page (settings).
        //EventManager.instance.SettingsEntered(pageIndex == _pageCount - 1);

        // We are at the second last page (notification).
        //EventManager.instance.NotificationEntered(pageIndex == _pageCount - 2, pageIndex == _pageCount - 1);

    }
    
}
