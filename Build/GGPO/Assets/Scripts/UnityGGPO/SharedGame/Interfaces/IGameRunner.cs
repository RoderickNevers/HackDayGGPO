using System.Diagnostics;

namespace SharedGame
{
    public interface IGameRunner
    {
        IGame Game { get; }
        GameInfo GameInfo { get; }

        void Idle(int ms);

        void RunFrame();

        string GetStatus(Stopwatch updateWatch);

        void DisconnectPlayer(int player);

        void Shutdown();
    }
}