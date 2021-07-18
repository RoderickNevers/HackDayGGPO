using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ReplayManager : MonoBehaviour
{
    [SerializeField] private PlayerController _Player;
    [SerializeField] private ReplayPanelController _ReplayPanelController;

    private MemoryStream memoryStream = null;
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;

    private bool recordingInitialized = false;
    private bool recording = false;
    private bool replaying = false;

    private int currentRecordingFrames = 0;
    public int maxRecordingFrames = 360;

    public int replayFrameLength = 2;
    private int replayFrameTimer = 0;

    public Action OnStartedRecording;
    public Action OnStoppedRecording;
    public Action OnStartedReplaying;
    public Action OnStoppedReplaying;


    private void OnAwake()
    {
        _ReplayPanelController.enabled = false;
    }

    private void OnEnable()
    {
        _ReplayPanelController.enabled = true;
    }

    private void OnDisable()
    {
        if (!enabled)
        {
            _ReplayPanelController.enabled = false;
        }
    }

    public void LateUpdate()
    {
        if (recording)
        {
            UpdateRecording();
        }
        else if (replaying)
        {
            UpdateReplaying();
        }
    }

    public void StartStopRecording()
    {
        if (!recording)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
        }
    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;
    }

    private void StartRecording()
    {

        if (!recordingInitialized)
        {
            InitializeRecording();
        }
        else
        {
            memoryStream.SetLength(0);
        }

        ResetReplayFrame();

        StartReplayFrameTimer();
        recording = true;
        OnStartedRecording?.Invoke();
    }

    private void UpdateRecording()
    {
        if (replayFrameTimer == 0)
        {
            SaveTransform(_Player);
            ResetReplayFrameTimer();
        }

        --replayFrameTimer;
        ++currentRecordingFrames;
    }

    private void StopRecording()
    {
        recording = false;
        OnStoppedRecording?.Invoke();
    }

    private void ResetReplayFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    public void StartStopReplaying()
    {
        if (!replaying)
        {
            StartReplaying();
        }
        else
        {
            StopReplaying();
        }
    }

    private void StartReplaying()
    {
        ResetReplayFrame();
        StartReplayFrameTimer();
        replaying = true;
        OnStartedReplaying?.Invoke();
    }

    private void UpdateReplaying()
    {
        if (memoryStream.Position >= memoryStream.Length)
        {
            StopReplaying();
            return;
        }

        if (replayFrameTimer == 0)
        {
            LoadTransform(_Player);
            ResetReplayFrameTimer();
        }

        --replayFrameTimer;
    }

    private void StopReplaying()
    {
        replaying = false;
        OnStoppedReplaying?.Invoke();
    }

    private void ResetReplayFrameTimer()
    {
        replayFrameTimer = replayFrameLength;
    }

    private void StartReplayFrameTimer()
    {
        replayFrameTimer = 0;
    }

    private void SaveTransform(PlayerController player)
    {
        //Inputs inputs = new Inputs(player.MoveDirection, player.Jump);
        //BinaryFormatter format = new BinaryFormatter();
        //format.Serialize(memoryStream, inputs);

        binaryWriter.Write(player.MoveDirection.x);
        binaryWriter.Write(player.MoveDirection.z);
        binaryWriter.Write(player.Jump);
    }

    private void LoadTransform(PlayerController player)
    {
        float x = binaryReader.ReadSingle();
        float z = binaryReader.ReadSingle();
        bool jump = binaryReader.ReadBoolean();

        player.MoveDirection = new Vector3(x, 0, z);
        player.Jump = jump;
    }
}