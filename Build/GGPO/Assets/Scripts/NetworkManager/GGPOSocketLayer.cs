using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using SharedGame;


public interface FacepunchConnectionInterface
{
    public void ForwardGGPOPacketToSteamworkConnection(byte[] data);
}

public class GGPOSocketLayer
{
    private const int DEFAULT_LOCAL_GGPO_PORT = 7000; // receiving
    private const int DEFAULT_REMOTE_GGPO_PORT = 7001; // sending

    private GGPOComponent gameManager;

    private UdpClient ggpoForwardReceiveSocket;
    private UdpClient ggpoForwardSendSocket;
    private Thread ggpoForwardThread;

    private FacepunchConnectionInterface facepunchConnection;

    public void InitializeGGPOSocketLayer(GGPOComponent gameManager)
    {
        this.gameManager = gameManager;
    }

    public void StartGGPOSession(bool isHost)
    {
        if (isHost)
        {
            gameManager.StartGGPOGame(null, GetConnections(), 0);
        }
        else
        {
            gameManager.StartGGPOGame(null, GetConnections(), 1);
        }
    }

    private List<Connections> GetConnections()
    {
        var list = new List<Connections>();
        list.Add(new Connections()
        {
            ip = "127.0.0.1",
            port = DEFAULT_REMOTE_GGPO_PORT,
            spectator = false
        });
        list.Add(new Connections()
        {
            ip = "127.0.0.1",
            port = DEFAULT_LOCAL_GGPO_PORT,
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

            ggpoForwardReceiveSocket.Send(ggpoPacket, size);
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
        ggpoForwardReceiveSocket = new UdpClient(DEFAULT_LOCAL_GGPO_PORT);

        // This socket receives "outgoing" packets which we forward to Steamworks
        ggpoForwardSendSocket = new UdpClient();

        // Spawn thread
        ggpoForwardThread = new Thread(ListenForForwardPackets);
        ggpoForwardThread.IsBackground = true;
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

        ggpoForwardReceiveSocket.Close();
        ggpoForwardSendSocket.Close();
    }

    private void ListenForForwardPackets()
    {
        IPEndPoint forwardEndPoint = new IPEndPoint(IPAddress.Any, DEFAULT_REMOTE_GGPO_PORT);
        while (true)
        {
            byte[] data = ggpoForwardSendSocket.Receive(ref forwardEndPoint);

            if (data.Length > 0)
            {
                ForwardGGPOPacketToSteamworkConnection(data);
            }

            Thread.Sleep(1);
        }
    }

    private void ForwardGGPOPacketToSteamworkConnection(byte[] data)
    {
        // Send data to either SocketManager or ConnectionManager
        facepunchConnection.ForwardGGPOPacketToSteamworkConnection(data);
    }
}
