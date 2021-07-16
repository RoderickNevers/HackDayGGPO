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

    private GGPOComponent gameManager;
    private bool isHost;

    private UdpClient ggpoForwardReceiveSocket;
    private UdpClient ggpoForwardSendSocket;
    private Thread ggpoForwardThread;

    private IPEndPoint localEndPoint;
    private IPEndPoint remoteEndPoint;

    private UdpClient ggpoRemoteSocketSend;
    private UdpClient ggpoRemoteSocketReceive;
    private ConcurrentQueue<byte[]> byteDataQueue;

    private FacepunchConnectionInterface facepunchConnection;

    public void InitializeGGPOSocketLayer(GGPOComponent gameManager)
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
            byte[] ggpoPacket = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(data, ggpoPacket, 0, size);

            // byteDataQueue.Enqueue(ggpoPacket);
            ggpoRemoteSocketSend.Send(ggpoPacket, size, localEndPoint);
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


        byteDataQueue = new ConcurrentQueue<byte[]>();

        //ggpoForwardReceiveSocket = new UdpClient();
        //ggpoForwardReceiveSocket.Connect(IPAddress.Parse("127.0.0.1"), DEFAULT_LOCAL_GGPO_PORT);

        //// This socket receives "outgoing" packets which we forward to Steamworks
        //ggpoForwardSendSocket = new UdpClient(DEFAULT_REMOTE_GGPO_PORT);

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

        //ggpoForwardReceiveSocket.Close();
        //ggpoForwardSendSocket.Close();

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
