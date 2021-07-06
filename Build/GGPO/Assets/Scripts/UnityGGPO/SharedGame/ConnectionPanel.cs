using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGGPO;

namespace SharedGame {

    public class ConnectionPanel : MonoBehaviour {
        public Button btnLocal;
        public Button btnRemote;
        public Button btnHost;
        public InputField inpIp;
        public InputField inpPort;
        public InputField hostIP;
        public InputField hostPort;
        public Text txtIp;
        public Text txtPort;

        private GameManager gameManager;
        private GgpoPerformancePanel perf;

        private void Awake() {
            gameManager = GameManager.Instance;
            gameManager.OnRunningChanged += OnRunningChanged;

            perf = FindObjectOfType<GgpoPerformancePanel>();
            perf.Setup();
            btnHost.onClick.AddListener(OnHostClick);
            btnRemote.onClick.AddListener(OnRemoteClick);
            btnLocal.onClick.AddListener(OnLocalClick);
            inpIp.text = "127.0.0.1";
            inpPort.text = "7000";
            hostIP.text = "127.0.0.1";
            hostPort.text = "7001";
        }

        private void OnDestroy() {
            gameManager.OnRunningChanged -= OnRunningChanged;
            btnHost.onClick.RemoveListener(OnHostClick);
            btnRemote.onClick.RemoveListener(OnRemoteClick);
            btnLocal.onClick.RemoveListener(OnLocalClick);
        }

        private List<Connections> GetConnections() {
            var list = new List<Connections>();
            list.Add(new Connections() {
                ip = inpIp.text,
                port = ushort.Parse(inpPort.text),
                spectator = false
            });
            list.Add(new Connections() {
                ip = txtIp.text,
                port = ushort.Parse(txtPort.text),
                spectator = false
            });
            return list;
        }

        private void OnHostClick() {
            gameManager.StartGGPOGame(perf, GetConnections(), 0);
        }

        private void OnRemoteClick() {
            gameManager.StartGGPOGame(perf, GetConnections(), 1);
        }

        private void OnLocalClick() {
            gameManager.StartLocalGame();
        }

        private void OnRunningChanged(bool isRunning) {
            gameObject.SetActive(!isRunning);
        }
    }
}