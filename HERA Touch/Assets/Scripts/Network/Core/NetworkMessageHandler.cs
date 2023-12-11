using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NetworkMessageHandler
{
    public static void ProcessMessage(string rawMessage, NetworkMessage currentMessage, NetworkTransmitter networkTransmitter)
    {
        // var peers = transmissionManager.GetPeers();

        switch ((NetworkMessageType)currentMessage.ty)
        {
            case NetworkMessageType.StreamMessage:
                StreamMessage receivedStreamMessage = NetworkUtilities.UnpackMessage<StreamMessage>(rawMessage);
                StreamDataHeader receivedHeader = JsonUtility.FromJson<StreamDataHeader>(receivedStreamMessage.d);
                EventManager.Instance.InvokeStreamUpdated(receivedHeader, receivedStreamMessage.v);
                break;

            case NetworkMessageType.AwakeMessage:
                //if this peer hasn't been gone long then fire a recap:
                if (networkTransmitter.peers.ContainsKey(currentMessage.a))
                {
                    // OnPeerFound?.Invoke(currentMessage.f, long.Parse(currentMessage.d));
                    networkTransmitter.peers[currentMessage.a].age = Time.realtimeSinceStartup;
                }
                break;

            case NetworkMessageType.HeartbeatMessage:
                //new peer:
                if (!networkTransmitter.peers.ContainsKey(currentMessage.a))
                {
                    networkTransmitter.peers.Add(currentMessage.a, new Peer(currentMessage.a, currentMessage.f, 0));

                }
                //catalog heartbeat time:
                networkTransmitter.peers[currentMessage.a].age = Time.realtimeSinceStartup;
                break;

            case NetworkMessageType.NotificationMessage:
                // Debug.Log($"Notification received! Message: {rawMessage}");
                Notification notification = NetworkUtilities.UnpackMessage<Notification>(currentMessage.d);
                NotificationManager.instance.Notify(notification);
                break;

            case NetworkMessageType.Vector3Message:
                // Debug.Log($"Vector3 received! Message: {rawMessage}");
                Vector3Message receivedVector3Message = NetworkUtilities.UnpackMessage<Vector3Message>(rawMessage);
                GlobalVariableManager.Instance.GlobalVector3s[receivedVector3Message.l] = receivedVector3Message.v;
                break;

            case NetworkMessageType.OperationMessage:
                OperationMessage receivedBaseControlMessage = NetworkUtilities.UnpackMessage<OperationMessage>(rawMessage);
                HandleOperation(receivedBaseControlMessage.o);
                break;

            case NetworkMessageType.CommandMessage:
                CommandMessage receivedCommandMessage = NetworkUtilities.UnpackMessage<CommandMessage>(rawMessage);
                HandController.Instance.SwitchHand();
                break;

        }

        void HandleOperation(Operation operation)
        {

            switch (operation.type)
            {
                case OperationType.SwitchControlMode:
                    ControlModeManager.Instance.currentControlMode = operation.controlMode;
                    break;
                

            }
        }
    }
}
