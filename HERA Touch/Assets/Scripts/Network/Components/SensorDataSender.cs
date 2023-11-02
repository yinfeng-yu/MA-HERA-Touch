using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDataSender : MonoBehaviour
{
    /// <summary>
    /// Frequency of streaming message (FPS)
    /// </summary>
    [Range(1, 60)]
    public uint frequency = 24;

    private float _interval = 1f / 24;

    public Transform phonePose;


    private void FixedUpdate()
    {
        if (_interval > 0)
        {
            _interval -= Time.deltaTime;
        }
        else
        {
            SendSensorData();
            _interval = 1f / frequency;
        }
    }

    void SendSensorData()
    {
        TransmissionManager.Instance.SendTo(new QuaternionMessage("deviceOrientation", phonePose.rotation), Platform.AR);
        TransmissionManager.Instance.SendTo(new Vector3Message("devicePosition", phonePose.position), Platform.AR);
        TransmissionManager.Instance.SendTo(new FloatMessage("deviceRange", SliderReader.Value), Platform.AR);

    }
}
