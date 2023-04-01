using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TouchMate/Task")]
public class TaskData : ScriptableObject
{
    public new string name;
    public string description;

    public string localReferenceKey;

}
