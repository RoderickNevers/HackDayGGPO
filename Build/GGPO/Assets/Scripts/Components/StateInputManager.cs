using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInputManager
{
    public class PlayerInputs
    {
        public long m_P1Input = -1;
        public long m_P2Input = -1;
    }

    // Key: Framenumber
    // Value: Player Inputs
    public Dictionary<int, PlayerInputs> m_FrameInputs { get; private set; }

    public StateInputManager()
    {
        m_FrameInputs = new Dictionary<int, PlayerInputs>();
    }

    public void UpdateFrameInputs(int frameNumber, long[] inputs)
    {
        if (inputs.Length < 2)
        {
            return;
        }

        if (m_FrameInputs.ContainsKey(frameNumber))
        {
            m_FrameInputs[frameNumber].m_P1Input = inputs[0];
            m_FrameInputs[frameNumber].m_P2Input = inputs[1];
        }
        else
        {
            m_FrameInputs[frameNumber] = new PlayerInputs()
            {
                m_P1Input = inputs[0],
                m_P2Input = inputs[1]
            };
        }
    }
}