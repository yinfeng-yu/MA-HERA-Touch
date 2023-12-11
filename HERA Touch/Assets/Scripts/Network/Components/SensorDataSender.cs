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

    /// <summary>
    /// Interval of which the phone transform data is sent
    /// </summary>
    private float _interval = 1f / 24;

    /// <summary>
    /// The model with phone transform
    /// </summary>
    public Transform phonePose;

    private void Start()
    {
        _interval = 1f / frequency;
    }

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

    /// <summary>
    /// Send device transform data
    /// </summary>
    void SendSensorData()
    {
        TransmissionManager.Instance.SendTo(new QuaternionMessage("deviceOrientation", phonePose.rotation), Platform.AR);
        TransmissionManager.Instance.SendTo(new Vector3Message("devicePosition", phonePose.position), Platform.AR);
        TransmissionManager.Instance.SendTo(new FloatMessage("deviceRange", SliderReader.Value), Platform.AR);

    }
}
