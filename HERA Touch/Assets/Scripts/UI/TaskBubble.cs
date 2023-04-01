using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBubble : Expandable
{
    public int index;

    public Image edge;

    public Color startColor = new Color(138 / 255, 236 / 255, 204 / 255);
    public Color pauseColor = new Color(248 / 255, 160 / 255, 27 / 255);

    protected override void Start()
    {
        base.Start();
        EventManager.instance.taskButtonEvents.clicked += ResetExpandable;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventManager.instance.taskButtonEvents.clicked -= ResetExpandable;
    }

    protected override void OnClicked()
    {
        base.OnClicked();
        EventManager.instance.taskBubbleEvents.Clicked();
        EventManager.instance.taskBubbleEvents.Selected(_selected);
    }

    public void SetEdgeColor(bool start)
    {
        edge.color = start ? startColor : pauseColor;
    }

}
