using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandednessDependentUI : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    // Update is called once per frame
    void Update()
    {
        switch (HandController.Instance.handedness)
        {
            case Handedness.Left:
                left.SetActive(true);
                right.SetActive(false);
                break;
            case Handedness.Right:
                left.SetActive(false);
                right.SetActive(true);
                break;
        }
    }
}
