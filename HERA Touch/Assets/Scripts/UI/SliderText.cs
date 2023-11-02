using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = ((int)(SliderReader.Value * 100)).ToString() + '%';
    }
}
