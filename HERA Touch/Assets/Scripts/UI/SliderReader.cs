using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderReader : MonoBehaviour
{
    private static float _value;
    public static float Value => _value;

    public static void SetSlider(float value)
    {
        _value = value;
    }
}
