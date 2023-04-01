using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandableEvents
{
    public event Action clicked;
    public event Action<bool> selected;

    public void Clicked()
    {
        clicked?.Invoke();
    }

    public void Selected(bool _selected)
    {
        selected?.Invoke(_selected);
    }
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class Expandable : MonoBehaviour
{
    protected Button _button;
    protected bool _selected = false;

    protected virtual void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => { OnClicked(); });

        EventManager.instance.pageChanged += OnPageChanged;
    }

    protected virtual void OnDestroy()
    {
        // _button.onClick.RemoveAllListeners();

        EventManager.instance.pageChanged -= OnPageChanged;
    }

    protected virtual void OnClicked()
    {
        _selected = !_selected;
        GetComponent<Animator>().SetBool("selected", _selected);
    }

    protected virtual void OnPageChanged()
    {
        ResetExpandable();
    }

    public virtual void ResetExpandable()
    {
        _selected = false;
        GetComponent<Animator>().SetBool("selected", _selected);
    }

}
