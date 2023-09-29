using TMPro;
using UnityEngine;

public class AccelerometerTracking : MonoBehaviour
{
    float speed = 10.0f;

    void Start()
    {
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;
        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis

        dir.x = -Input.acceleration.y;
        dir.y = Input.acceleration.z;
        dir.z = Input.acceleration.x;

        // Debug.Log(dir);

        // clamp acceleration vector to the unit sphere
        // if (dir.sqrMagnitude > 1)
        //     dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        // dir *= Time.deltaTime;

        // Move object
        // transform.Translate(dir * speed);
        // transform.position = dir;

        // Debug.Log($"Gyro rotation rate: {m_Gyro.rotationRate} \n Gyro attitude: {m_Gyro.attitude} \n Gyro enabled: {m_Gyro.enabled}");

        // string acc = "";
        // foreach (AccelerationEvent accEvent in Input.accelerationEvents)
        // {
        //     acc += accEvent.ToString() + ": " + accEvent.acceleration + "\n";
        // }
        // Debug.Log(acc);

        // Debug.Log($"acc event count: {Input.accelerationEventCount}");
    }
}
