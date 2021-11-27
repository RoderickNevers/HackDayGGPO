using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using SharedGame;

public interface FacepunchConnectionInterface
{
    void ForwardGGPOPacketToSteamworkConnection(byte[] data);
}

public class GGPOSocketLayer
{
    private const ushort DEFAULT_LOCAL_GGPO_PORT = 7000; // receiving
    private const ushort DEFAULT_REMOTE_GGPO_PORT = 7001; // sending

    private const int PACKET_BUFFER_SIZE = 200;

    private GGPOGameManager gameManager;
    private bool isHost;
    
    private Thread ggpoForwardThread;

    private IPEndPoint localEndPoint;
    private IPEndPoint remoteEndPoint;

    private UdpClient ggpoRemoteSocketSend;
    private UdpClient ggpoRemoteSocketReceive;
    private byte[] packetReceiveBuffer;

    private FacepunchConnectionInterface facepunchConnection;

    public void InitializeGGPOSocketLayer(GGPOGameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void StartGGPOSession(bool isHost)
    {
        this.isHost = isHost;

        if (isHost)
        {
            gameManager.StartGGPOGame(null, GetConnections(), 0);
        }
        else
        {
            gameManager.StartGGPOGame(null, GetConnections(), 1);
        }
    }

    public void StopGGPOSession()
    {
        gameManager.StopGGPOGame();
    }

    private List<Connections> GetConnections()
    {
        var list = new List<Connections>();
        list.Add(new Connections()
        {
            ip = "127.0.0.1",
            port = isHost ? DEFAULT_LOCAL_GGPO_PORT : DEFAULT_REMOTE_GGPO_PORT,
            spectator = false
        });
        list.Add(new Connections()
        {
            ip = "127.0.0.1",
            port = isHost ? DEFAULT_REMOTE_GGPO_PORT : DEFAULT_LOCAL_GGPO_PORT,
            spectator = false
        });
        return list;
    }

    public void OnFacepunchMessageReceived(IntPtr data, int size)
    {
        try
        {
            // forward data to listening GGPO socket
            if (size > packetReceiveBuffer.Length)
            {
                packetReceiveBuffer = new byte[size];
            }
            System.Runtime.InteropServices.Marshal.Copy(data, packetReceiveBuffer, 0, size);
            
            ggpoRemoteSocketSend.Send(packetReceiveBuffer, size, localEndPoint);
        }
        catch
        {
            Debug.Log("Unable to forward message to receiving socket");
        }
    }

    public void InitGGPOForwardSockets(FacepunchConnectionInterface facepunchConnection)
    {
        this.facepunchConnection = facepunchConnection;

        // This socket is for receiving data from the remote client
        localEndPoint = new IPEndPoint(IPAddress.Loopback, DEFAULT_LOCAL_GGPO_PORT);
        remoteEndPoint = new IPEndPoint(IPAddress.Loopback, DEFAULT_REMOTE_GGPO_PORT);

        ggpoRemoteSocketReceive = new UdpClient();
        ggpoRemoteSocketReceive.ExclusiveAddressUse = false;
        ggpoRemoteSocketReceive.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        ggpoRemoteSocketReceive.Client.Bind(remoteEndPoint);

        ggpoRemoteSocketSend = new UdpClient();
        ggpoRemoteSocketSend.ExclusiveAddressUse = false;
        ggpoRemoteSocketSend.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        ggpoRemoteSocketSend.Client.Bind(remoteEndPoint);

        packetReceiveBuffer = new byte[PACKET_BUFFER_SIZE];

        // Spawn thread
        ggpoForwardThread = new Thread(ListenForForwardPackets);
        ggpoForwardThread.Start();
    }

    public void CloseGGPOForwardSockets()
    {
        try
        {
            ggpoForwardThread.Abort();
        }
        catch
        {
            Debug.Log("Forward thread aborted");
        }

        ggpoRemoteSocketReceive.Close();
        ggpoRemoteSocketSend.Close();

        this.facepunchConnection = null;
    }

    private void ListenForForwardPackets()
    {
        while (true)
        {
            byte[] recData = ggpoRemoteSocketReceive.Receive(ref localEndPoint);

            if (recData.Length > 0)
            {
                // Debug.Log("send to remote client");
                ForwardGGPOPacketToSteamworkConnection(recData);
            }

            Thread.Sleep(0);
        }
    }

    private void ForwardGGPOPacketToSteamworkConnection(byte[] data)
    {
        // Send data to either SocketManager or ConnectionManager
        facepunchConnection.ForwardGGPOPacketToSteamworkConnection(data);
    }
}
