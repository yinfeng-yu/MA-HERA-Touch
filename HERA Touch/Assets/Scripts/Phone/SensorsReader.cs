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

    public static float range = 5f;

    [SerializeField] float speed = 10f;
    [SerializeField] float velocityThreshold = 0.05f;
    [SerializeField] float accelerationThreshold = 0.04f;

    // public Slider leftRangeSlider;
    // public Slider rightRangeSlider;
    // public LineRenderer line;

    Quaternion _orientation;

    bool decelerationFlag = false;

    void Start()
    {
        // Set up and enable the gyroscope (check your device has one)
        _gyro = Input.gyro;
        _gyro.enabled = true;

        Input.compass.enabled = true;

        // leftRangeSlider.value = 0.5f;
        // rightRangeSlider.value = 0.5f;

        // line.gameObject.SetActive(true);

    }

    void Update()
    {

        _orientation = GyroToUnity(_gyro.attitude);

    }

    public Quaternion ReadOrientation()
    {
        return _orientation;
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

    // public void SetSlider()
    // {
    //     // if (rangeSlider.value > 0.5f)
    //     // {
    //     //     rangeSlider.value = 0.5f;
    //     // }
    //     // range = minRange + rangeSlider.value * 2 * (maxRange - minRange);
    //     if (leftRangeSlider.IsActive())
    //     {
    //         range = leftRangeSlider.value;
    //         leftRangeSlider.GetComponentInChildren<TextMeshProUGUI>().text = ((int)(leftRangeSlider.value * 100)).ToString() + '%';
    //     }
    //     else
    //     {
    //         range = rightRangeSlider.value;
    //         rightRangeSlider.GetComponentInChildren<TextMeshProUGUI>().text = ((int)(rightRangeSlider.value * 100)).ToString() + '%';
    //     }
    // 
    // }
}