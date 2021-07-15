using Steamworks;
using Steamworks.Data;
using System;
using UnityEngine;

public class FacepunchSocketManager : SocketManager, FacepunchConnectionInterface
{
    private GGPOSocketLayer ggpoSocketLayer;
    private Connection clientConnection;

    public override void OnConnecting(Connection connection, ConnectionInfo data)
    {
        base.OnConnecting(connection, data);
        Debug.Log($"{data.Identity} is connecting");
    }

    public override void OnConnected(Connection connection, ConnectionInfo data)
    {
        base.OnConnected(connection, data);
        Debug.Log($"{data.Identity} has joined the game");

        // Assumed to be second player
        clientConnection = connection;

        // Initialize GGPO session
        // ggpoSocketLayer.InitializeGGPOSocketLayer( _ );
        // ggpoSocketLayer.StartGGPOSession(true);
    }

    public override void OnDisconnected(Connection connection, ConnectionInfo data)
    {
        base.OnDisconnected(connection, data);
        Debug.Log($"{data.Identity} is out of here");

        if (connection.Id == clientConnection.Id)
        {
            // Disconnect GGPO session
        }
        else
        {
            Debug.Assert(false, "Someone that isn't our opponent disconnected...");
        }
    }

    public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
    {
        Debug.Log($"We got a message from {identity}!");

        if (connection.Id == clientConnection.Id)
        {
            ggpoSocketLayer.OnFacepunchMessageReceived(data, size);
        }
        else
        {
            Debug.Assert(false, "Someone that isn't our opponent sent us a message...");
        }
    }

    public void ForwardGGPOPacketToSteamworkConnection(byte[] data)
    {
        // Send data received from "remote" socket to steamworks connection
        clientConnection.SendMessage(data);
    }

    public void InitGGPOForwardSockets()
    {
        ggpoSocketLayer = new GGPOSocketLayer();
        ggpoSocketLayer.InitGGPOForwardSockets(this);
    }

    public void CloseGGPOForwardSockets()
    {
        ggpoSocketLayer.CloseGGPOForwardSockets();
    }
}
