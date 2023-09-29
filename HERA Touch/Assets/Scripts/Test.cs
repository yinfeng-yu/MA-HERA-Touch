using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = SensorDataSender.ARCameraToObject(Camera.main.transform.rotation);
        transform.rotation = Camera.main.transform.rotation;
    }
}
