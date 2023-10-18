using TMPro;
using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.UI;

// using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorsReader : MonoBehaviour
{
    public struct SensorsData
    {
        public Quaternion orientation;
        public float range;
    }

    Gyroscope _gyro;
    // [SerializeField] private TextMeshProUGUI text;

    // [SerializeField] Transform obj;
    // [SerializeField] Transform pivot;
    // [SerializeField] Transform hand;

    float range = 5f;
    [SerializeField] float maxRange = 10f;
    [SerializeField] float minRange = 3f;

    [SerializeField] float speed = 10f;
    [SerializeField] float velocityThreshold = 0.05f;
    [SerializeField] float accelerationThreshold = 0.04f;

    public Slider leftRangeSlider;
    public Slider rightRangeSlider;
    // public LineRenderer line;

    Vector3 velocity;

    Vector3 _acceleration;
    Vector3 _userAcceleration;
    Vector3 _worldUserAcceleration;
    Quaternion _orientation;
    Vector3 _gravity;

    bool decelerationFlag = false;

    void Start()
    {
        // Set up and enable the gyroscope (check your device has one)
        _gyro = Input.gyro;
        _gyro.enabled = true;

        Input.compass.enabled = true;

        velocity = Vector3.zero;
        leftRangeSlider.value = 0.5f;
        rightRangeSlider.value = 0.5f;

        // line.gameObject.SetActive(true);

    }

    void Update()
    {
        _acceleration.x = -Input.acceleration.y;
        _acceleration.y = -Input.acceleration.z;
        _acceleration.z = Input.acceleration.x;

        _userAcceleration.x = -_gyro.userAcceleration.x;
        _userAcceleration.y = -_gyro.userAcceleration.z;
        _userAcceleration.z = -_gyro.userAcceleration.y;

        _gravity.x = -_gyro.gravity.y;
        _gravity.y = -_gyro.gravity.z;
        _gravity.z = _gyro.gravity.x;

        ReadSlider();
        // line.transform.localScale = new Vector3(line.transform.localScale.x, line.transform.localScale.y, range);

        _orientation = GyroToUnity(_gyro.attitude);

        _worldUserAcceleration = Vector3.zero;

        // Vector3[] localAxes = { obj.right, obj.up, obj.forward };
        Vector3[] localAxes = { _orientation * Vector3.right, _orientation * Vector3.up, _orientation * Vector3.forward };
        Vector3[] worldAxes = { Vector3.right, Vector3.up, Vector3.forward };

        for (int i = 0; i < 3; i ++)
        {
            for (int j = 0; j < 3; j++)
            {
                _worldUserAcceleration += Vector3.Project((_userAcceleration[i] * localAxes[i]), worldAxes[j]);
            }
        }

        // Filter jittering
        // for (int i = 0; i < 3; i ++)
        // {
        //     if (Mathf.Abs(_worldUserAcceleration[i]) < accelerationThreshold)
        //     {
        //         _worldUserAcceleration[i] = 0;
        //     }
        // }

        if (_worldUserAcceleration.magnitude < accelerationThreshold)
        {
            _worldUserAcceleration = Vector3.zero;
        }

        // float filtered = kalmanFilter.Filter(_userAcceleration.y);
        // Debug.Log($"Real value: {_userAcceleration.y}, Real-time filter: {filtered}");


        if (Vector3.Dot(velocity, _worldUserAcceleration) < 0) // Now the device is decelerating
        {
            decelerationFlag = true;
        }


        velocity += _worldUserAcceleration * Time.deltaTime * speed;

        if (velocity.magnitude < velocityThreshold && decelerationFlag)
        {
            velocity = Vector3.zero;
            decelerationFlag = false;
        }

        if (_worldUserAcceleration.magnitude == 0 && decelerationFlag)
        {
            velocity = Vector3.zero;
            decelerationFlag = false;
        }

        // obj.position += velocity * speed * Time.deltaTime;
        // obj.position += new Vector3(0, velocity.y * Time.deltaTime, 0);

        // if (Vector3.Distance(obj.position, pivot.position) > range)
        // {
        //     velocity = Vector3.zero;
        //     obj.position = (obj.position - pivot.position).normalized * range + pivot.position;
        // }

        // obj.position = pivot.position;
        // hand.position = obj.forward * range + pivot.position;
        // hand.rotation = obj.rotation;


        // text.text = $"Gyro Rotation Rate \nX={_gyro.rotationRate.x:#0.00} Y={_gyro.rotationRate.y:#0.00} Z={_gyro.rotationRate.z:#0.00}\n\n" +
        //                 $"Acceleration\nX={_acceleration.x:#0.00} Y={_acceleration.y:#0.00} Z={_acceleration.z:#0.00}\n\n" +
        //                     $"Attitude\nX={_gyro.attitude.x:#0.00} Y={_gyro.attitude.y:#0.00} Z={_gyro.attitude.z:#0.00} W={_gyro.attitude.w:#0.00}\n\n" +
        //                      $"Gravity\nX={_gravity.x:#0.00} Y={_gravity.y:#0.00} Z={_gravity.z:#0.00}\n\n" +
        //                      $"User Acceleration\nX={_gyro.userAcceleration.x:#0.00} Y={_gyro.userAcceleration.y:#0.00} Z={_gyro.userAcceleration.z:#0.00}\n\n" +
        //                      // $"World User Acceleration\nX={_worldUserAcceleration.x:#0.00} Y={_worldUserAcceleration.y:#0.00} Z={_worldUserAcceleration.z:#0.00}\n\n" +
        //                      $"Velocity\nX={velocity.x:#0.00} Y={velocity.y:#0.00} Z={velocity.z:#0.00}\n\n";

        
        // obj.rotation = _orientation;

    }

    public static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.blue;
        // Gizmos.DrawLine(obj.position, obj.position + obj.forward * 5f);
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(obj.position, obj.position + obj.right * 5f);
        // Gizmos.color = Color.green;
        // Gizmos.DrawLine(obj.position, obj.position + obj.up * 5f);
        // 
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(pivot.position, range);
    }

    public SensorsData GetSensorsData()
    {
        SensorsData sensorsData;
        sensorsData.orientation = _orientation;
        sensorsData.range = range;

        return sensorsData;
    }

    void ReadSlider()
    {
        // if (rangeSlider.value > 0.5f)
        // {
        //     rangeSlider.value = 0.5f;
        // }
        // range = minRange + rangeSlider.value * 2 * (maxRange - minRange);
        if (leftRangeSlider.IsActive())
        {
            range = leftRangeSlider.value;
            leftRangeSlider.GetComponentInChildren<TextMeshProUGUI>().text = ((int)(leftRangeSlider.value * 100)).ToString() + '%';
        }
        else
        {
            range = rightRangeSlider.value;
            rightRangeSlider.GetComponentInChildren<TextMeshProUGUI>().text = ((int)(rightRangeSlider.value * 100)).ToString() + '%';
        }


        
    }
}