using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = ((int)(SliderReader.Value * 100)).ToString() + '%';
    }
}
