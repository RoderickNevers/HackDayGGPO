
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReplayPanelController : MonoBehaviour
{
    public Button startStopRecordButton;
    public TextMeshProUGUI startStopRecordButtonText;
    public Button startStopReplayButton;
    public TextMeshProUGUI startStopReplayButtonText;
    public ReplayManager replayManager;

    private void OnEnable()
    {
        replayManager.OnStartedRecording += OnStartedRecording;
        replayManager.OnStoppedRecording += OnStoppedRecording;
        replayManager.OnStartedReplaying += OnStartedReplaying;
        replayManager.OnStoppedReplaying += OnStoppedReplaying;
    }

    private void OnDisable()
    {
        replayManager.OnStartedRecording -= OnStartedRecording;
        replayManager.OnStoppedRecording -= OnStoppedRecording;
        replayManager.OnStartedReplaying -= OnStartedReplaying;
        replayManager.OnStoppedReplaying -= OnStoppedReplaying;
    }

    void OnStartedRecording()
    {
        startStopRecordButtonText.text = "Stop Recording";
    }

    void OnStoppedRecording()
    {
        startStopRecordButtonText.text = "Start Recording";
        startStopReplayButton.interactable = true;
    }

    void OnStartedReplaying()
    {
        startStopReplayButtonText.text = "Stop Replay";
        startStopRecordButton.interactable = false;
    }

    void OnStoppedReplaying()
    {
        startStopReplayButtonText.text = "Start Replay";
        startStopRecordButton.interactable = true;
    }
}
