using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableManager : Singleton<GlobalVariableManager>
{
    // Global Variables
    public Dictionary<string, Vector3> GlobalVector3s = new Dictionary<string, Vector3>();


    public Vector3 GetVector3(string label)
    {
        return GlobalVector3s[label];
    }
}
