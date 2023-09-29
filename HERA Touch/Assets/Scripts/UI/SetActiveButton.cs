using UnityEngine;

public class SetActiveButton : MonoBehaviour
{
    public GameObject target;
    public void TargetSetActive(bool b)
    {
        target.SetActive(b);
    }

    public void TargetToggleActive()
    {
        if (target.activeInHierarchy)
        {
            target.SetActive(false);
        }
        else
        {
            target.SetActive(true);
        }
    }
}
