using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControlSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendConfirmMessage()
    {
        Debug.Log("Confirm");
        BaseControlInfo baseControlInfo = new BaseControlInfo();
        baseControlInfo.confirm = true;

        // TransmissionManager.instance.Send(new BaseControlMessage(baseControlInfo));
        // Transmitter.Instance.Send(new BaseControlMessage(baseControlInfo));
        TransmissionManager.Instance.SendTo(new BaseControlMessage(baseControlInfo), Platform.AR);
    }
}
