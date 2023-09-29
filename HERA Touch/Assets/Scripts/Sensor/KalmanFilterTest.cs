using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalmanFilterTest : MonoBehaviour
{
    void Start()
    {
        float[] data = new float[] { 5, 6, 8, 3, 7, 6, 2 }; // 先給一組數字, 當然之後你也可以把 Sensor 讀到的值放入這

        KalmanFilter kf = new KalmanFilter();

        kf.SetQ(0.00001f); // 理想誤差 : 數值越大, 濾波效果越小
        kf.SetR(0.0001f);  // 實際誤差

        float[] s = kf.Filter(data); // 陣列濾波

        kf.SetFirst(data[0]); // [ 可選 ] 是否要先加入 "前一個" 值

        string ss = "實際數值\t陣列濾波\t即時濾波\n\n";

        for (int i = 1; i < data.Length; i++)
        {
            float d = kf.Filter(data[i]); // 即時濾波
            ss += data[i] + "\t" + s[i] + "\t" + d + "\n";
        }

        print(ss);
    }
}
