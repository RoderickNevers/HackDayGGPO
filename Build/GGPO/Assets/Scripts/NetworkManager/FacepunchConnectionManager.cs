using Steamworks;
using Steamworks.Data;
using System;
using UnityEngine;

public class FacepunchConnectionManager : ConnectionManager, FacepunchConnectionInterface
{
    private GGPOSocketLayer ggpoSocketLayer;

    public override void OnConnecting(ConnectionInfo data)
    {
        base.OnConnecting(data);//The base class will accept the connection
        Debug.Log("ConnectionManager OnConnecting");
    }

    public override void OnConnected(ConnectionInfo data)
    {
        base.OnConnected(data);
        Debug.Log("ConnectionManager OnConnected!!");

        // Initialize GGPO session
        ggpoSocketLayer.StartGGPOSession(false);
    }

    public override void OnDisconnected(ConnectionInfo data)
    {
        base.OnDisconnected(data);
        Debug.Log("ConnectionManager Player disconnected");

        // Disconnect GGPO session
        CloseGGPOForwardSockets();
    }

    public override void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
    {
        // Message Received!!
        // Debug.Log("ConnectionManager Connection Got A Message");

        ggpoSocketLayer.OnFacepunchMessageReceived(data, size);
    }

    public void ForwardGGPOPacketToSteamworkConnection(byte[] data)
    {
        // Send data received from "remote" socket to steamworks connection
        Connection.SendMessage(data, SendType.NoDelay);
    }

    public void InitGGPOForwardSockets(GGPOComponent gameManager)
    {
        ggpoSocketLayer = new GGPOSocketLayer();
        ggpoSocketLayer.InitializeGGPOSocketLayer(gameManager);
        ggpoSocketLayer.InitGGPOForwardSockets(this);
    }

    public void CloseGGPOForwardSockets()
    {
        ggpoSocketLayer.StopGGPOSession();
        ggpoSocketLayer.CloseGGPOForwardSockets();
    }
}