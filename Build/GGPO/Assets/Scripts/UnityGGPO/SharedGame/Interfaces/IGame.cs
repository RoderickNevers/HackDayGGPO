using Unity.Collections;

namespace SharedGame
{
    public interface IGame
    {
        int Framenumber { get; }
        int Checksum { get; }

        void UpdateSimulation(long[] inputs, int disconnectFlags);

        void FromBytes(NativeArray<byte> data);

        NativeArray<byte> ToBytes();

        long ReadInputs(int controllerId);

        void LogInfo(string filename);

        void FreeBytes(NativeArray<byte> data);
    }
}