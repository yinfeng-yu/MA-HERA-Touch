using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwipeMenuToggleGroup : MonoBehaviour
{
    public delegate void ChangedEventHandler(int newActive);

    public event ChangedEventHandler OnChange;
    public void Start()
    {
        foreach (Transform transformToggle in gameObject.transform)
        {
            var toggle = transformToggle.gameObject.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((isSelected) => {
                if (!isSelected)
                {
                    return;
                }
                var activeIndex = transformToggle.gameObject.GetComponent<SwipeMenuToggle>().index;
                DoOnChange(activeIndex);
            });
        }
    }

    protected virtual void DoOnChange(int newactive)
    {
        var handler = OnChange;
        if (handler != null) handler(newactive);
    }

    public void SetToggleOn(int index)
    {
        var toggle = gameObject.transform.GetChild(index).gameObject.GetComponent<Toggle>();
        toggle.isOn = true;
    }
}
