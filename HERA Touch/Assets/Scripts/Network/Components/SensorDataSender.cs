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

    [SerializeField] SensorsReader _sensorsReader;

    private float _interval = 1f / 24;

    public Transform arCamera;


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
        var sensorsData = _sensorsReader.GetSensorsData();

        // TransmissionManager.Instance.SendTo(new QuaternionMessage("deviceOrientation", sensorsData.orientation), Platform.AR);
        TransmissionManager.Instance.SendTo(new QuaternionMessage("deviceOrientation", arCamera.rotation), Platform.AR);
        TransmissionManager.Instance.SendTo(new Vector3Message("devicePosition", arCamera.localPosition), Platform.AR);

        TransmissionManager.Instance.SendTo(new FloatMessage("deviceRange", sensorsData.range), Platform.AR);

    }

    public static Quaternion ARCameraToObject(Quaternion q)
    {
        return Quaternion.Inverse(new Quaternion(q.x, q.z, q.y, -q.w));
    }
}
