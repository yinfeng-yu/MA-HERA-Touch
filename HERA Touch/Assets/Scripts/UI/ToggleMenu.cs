using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public List<GameObject> menus;
    public int curToggleIndex = 0;

    public void TurnOnMenu(int menuIndex)
    {
        curToggleIndex = menuIndex;
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].SetActive(i == menuIndex);
        }
    }
}
