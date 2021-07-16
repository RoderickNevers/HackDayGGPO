using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityGGPO;

namespace SharedGame {

    public abstract class GameManager : MonoBehaviour {
        private static GameManager _instance;

        public static GameManager Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

        public const float FRAME_LENGTH_SEC = 1f / 60f;
        protected float next;
        protected float nextIdle;

        public event Action<string> OnStatus;

        public event Action<string> OnChecksum;

        public event Action<bool> OnRunningChanged;

        public event Action OnInit;

        public event Action OnStateChanged;

        public Stopwatch updateWatch = new Stopwatch();

        public bool IsRunning { get; private set; }

        public IGameRunner Runner { get; private set; }

        public void DisconnectPlayer(int player) {
            if (Runner != null) {
                Runner.DisconnectPlayer(player);
            }
        }

        public virtual void Shutdown() {
            if (Runner != null) {
                Runner.Shutdown();
                Runner = null;
            }
        }

        private void OnDestroy() {
            Shutdown();
            _instance = null;
        }

        protected virtual void OnPreRunFrame() {
        }

        private void Update()
        {
            var now = Time.time;

            if (IsRunning != (Runner != null)) {
                IsRunning = Runner != null;
                OnRunningChanged?.Invoke(IsRunning);
                if (IsRunning) {
                    OnInit?.Invoke();

                    next = now;
                    nextIdle = now;
                }
            }
            if (Runner != null)
            {
                if (now >= next)
                {
                    next += FRAME_LENGTH_SEC;

                    //if (updateWatch.IsRunning)
                    //{
                    //    updateWatch.Stop();

                    //    string status = Runner.GetStatus(updateWatch);
                    //    OnStatus?.Invoke(status);
                    //    OnChecksum?.Invoke(RenderChecksum(Runner.GameInfo.periodic) + RenderChecksum(Runner.GameInfo.now));
                    //}

                    //updateWatch.Start();

                    OnPreRunFrame();
                    Runner.RunFrame();
                    OnStateChanged?.Invoke();

                    now = Time.time;
                    var extraMs = Mathf.Max(0, (int)((next - now) * 1000f) - 1);
                    Runner.Idle(extraMs);

                    // UnityEngine.Debug.Log(string.Format("Next: {0}, Now: {1}, Diff: {2}, Extra ms: {3}", next, now, next-now, extraMs));
                }
            }
        }

        private string RenderChecksum(GameInfo.ChecksumInfo info) {
            return string.Format("f:{0} c:{1}", info.framenumber, info.checksum); // %04d  %08x
        }

        public void StartGame(IGameRunner runner) {
            Runner = runner;
        }

        public abstract void StartLocalGame();

        public abstract void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex);

        public virtual void OnFrameDelay(int framesToDelay)
        {
            next += framesToDelay * FRAME_LENGTH_SEC;
        }
    }
}