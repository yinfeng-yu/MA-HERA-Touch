using UnityEngine;
using UnityEngine.Events;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

[Serializable]
public class Peer
{
    public string key;
    public string ipAddress;
    public float age;

    public Peer(string _key, string _ipAddress, float _age)
    {
        key = _key;
        ipAddress = _ipAddress;
        age = _age;
    }
}

[RequireComponent(typeof(NetworkMessageHandler))]
public class NetworkTransmitter : Transmitter
{
    //Public Variables:
    public int Port = 23000;

    // For Unity Editor debug use
    public int SendPort = 23001;
    public int ReceivePort = 23002;

    public int BufferSize = 2048;

    public bool DebugOutgoing;
    public bool DebugIncoming;

    private UdpClient udpClient;
    private Dictionary<string, Peer> peers = new Dictionary<string, Peer>();

    private static Thread receiveThread;
    private static IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, 0);
    private static bool receiveThreadAlive;

    private bool initialized;

    private static ConcurrentBag<string> receivedMessages = new ConcurrentBag<string>();

    [SerializeField] private const float heartbeatInterval = 2;
    private const float stalePeerTimeout = 8;
    private static long age;

    public Dictionary<string, Peer> Peers
    {
        get => peers;
    }

    public override void Initialize()
    {
        // flag initializtion complete:
        if (initialized)
        {
            return;
        }
        initialized = true;

        //establish socket:
        bool socketOpen = false;
        while (!socketOpen)
        {
            try
            {
#if UNITY_EDITOR
                udpClient = new UdpClient(ReceivePort);
#else
                udpClient = new UdpClient(Port);
#endif

                udpClient.Client.SendBufferSize = BufferSize;
                udpClient.Client.ReceiveBufferSize = BufferSize;
                socketOpen = true;
            }
            catch (Exception)
            {
            }
        }

        age = DateTime.Now.Ticks;

        //establish receive thread:
        receiveThreadAlive = true;
        receiveThread = new Thread(new ThreadStart(Receive));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        
        //fire off an awake event:
        Send(new NetworkMessage(NetworkMessageType.AwakeMessage, NetworkAudience.NetworkBroadcast, "", true, age.ToString()));
        
        StartCoroutine(Heartbeat());
    }

    public override void Receive()
    {
        while (receiveThreadAlive)
        {
            byte[] bytes = udpClient.Receive(ref receiveEndPoint);

            //get raw message for key evaluation:
            string serialized = Encoding.UTF8.GetString(bytes);
            NetworkMessage rawMessage = JsonUtility.FromJson<NetworkMessage>(serialized);

            // keys evaluations:
            // if (rawMessage.a != instance.appKey)
            // {
            //     //we send the serialized string for easier debug messages:
            //     _receivedMessages.Add(serialized);
            // }
            receivedMessages.Add(serialized);

        }
    }

    public override void ReceiveMessages()
    {
        while (receivedMessages.Count > 0)
        {
            string rawMessage;
            if (receivedMessages.TryTake(out rawMessage))
            {
                // get message:
                NetworkMessage currentMessage = JsonUtility.FromJson<NetworkMessage>(rawMessage);

                // debug:
                if (DebugIncoming)
                {
                    Debug.Log($"Received {rawMessage} from {currentMessage.f}");
                }

                GetComponent<NetworkMessageHandler>().ProcessMessage(rawMessage, currentMessage, this);
            }
        }
    }

    public override void Send(Message message)
    {
        NetworkMessage networkMessage = (NetworkMessage) message;

        string serialized = message.ProduceJsonString();
        byte[] data = message.ProduceData();

#if UNITY_EDITOR
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, SendPort);
        // _udpClient = new UdpClient(instance.receivePort);
#else
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, instance.port);
#endif

        // Size check:
        if (data.Length > udpClient.Client.SendBufferSize)
        {
            Debug.Log($"Message too large to send! Buffer is currently {BufferSize} bytes and you are tring to send {data.Length} bytes. Try increasing the buffer size.");
            return;
        }

        // Send:
        if (string.IsNullOrEmpty(networkMessage.t))
        {
            //send to all peers:
            foreach (var item in peers)
            {
                endPoint.Address = IPAddress.Parse(item.Value.ipAddress);
                udpClient.Send(data, data.Length, endPoint);
        
                // Debug:
                if (DebugOutgoing)
                {
                    Debug.Log($"Sent {serialized} to {endPoint}");
                }
            }
        }
        else
        {
            endPoint.Address = IPAddress.Parse(networkMessage.t);
            udpClient.Send(data, data.Length, endPoint);
        
            // Debug:
            if (DebugOutgoing)
            {
                Debug.Log($"Sent {serialized} to {endPoint}");
            }
        }

    }

    private IEnumerator Heartbeat()
    {
        while (true)
        {
            //transmit message - set startup time as our data for oldest peer evaluations:
            Send(new NetworkMessage(NetworkMessageType.HeartbeatMessage, NetworkAudience.NetworkBroadcast, "", false, age.ToString()));

            //stale peer identification:
            List<string> stalePeers = new List<string>();
            foreach (var item in peers.ToList())
            {
                if (Time.realtimeSinceStartup - item.Value.age > stalePeerTimeout)
                {
                    stalePeers.Add(item.Key);
                }
            }

            //stale peer removal:
            foreach (var item in stalePeers)
            {
                peers.Remove(item);
            }

            //loop:
            yield return new WaitForSeconds(heartbeatInterval);
            yield return null;
        }
    }


}
