using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandednessDependentUI : MonoBehaviour
{
    public Handedness handedness;

    // Update is called once per frame
    void Update()
    {
        if (HandController.Instance.handedness == handedness)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
