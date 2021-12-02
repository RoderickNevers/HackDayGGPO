using System.Diagnostics;

namespace SharedGame
{
    public interface IGameRunner
    {
        IGame Game { get; }
        GameInfo GameInfo { get; }
        StateInputManager m_StateInputManager { get; }
        void Idle(int ms);
        void RunFrame();
        string GetStatus(Stopwatch updateWatch);
        void DisconnectPlayer(int player);
        void Shutdown();
        void SetGame(IGame game);
    }
}