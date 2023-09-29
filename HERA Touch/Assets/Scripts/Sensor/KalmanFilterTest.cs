using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalmanFilterTest : MonoBehaviour
{
    void Start()
    {
        float[] data = new float[] { 5, 6, 8, 3, 7, 6, 2 }; // �Ƚoһ�M����, ��Ȼ֮����Ҳ���԰� Sensor �x����ֵ�����@

        KalmanFilter kf = new KalmanFilter();

        kf.SetQ(0.00001f); // �����`�� : ��ֵԽ��, �V��Ч��ԽС
        kf.SetR(0.0001f);  // ���H�`��

        float[] s = kf.Filter(data); // ��ОV��

        kf.SetFirst(data[0]); // [ ���x ] �Ƿ�Ҫ�ȼ��� "ǰһ��" ֵ

        string ss = "���H��ֵ\t��ОV��\t���r�V��\n\n";

        for (int i = 1; i < data.Length; i++)
        {
            float d = kf.Filter(data[i]); // ���r�V��
            ss += data[i] + "\t" + s[i] + "\t" + d + "\n";
        }

        print(ss);
    }
}
