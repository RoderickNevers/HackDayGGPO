using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs
{
    public int m_FrameNumber = -1;
    public long m_P1Input = -1;
    public long m_P2Input = -1;

    public void Reset()
    {
        m_FrameNumber = -1;
        m_P1Input = -1;
        m_P2Input = -1;
    }

    public PlayerInputs Clone()
    {
        PlayerInputs copy = new PlayerInputs();
        copy.m_FrameNumber = m_FrameNumber;
        copy.m_P1Input = m_P1Input;
        copy.m_P2Input = m_P2Input;

        return copy;
    }
}

public class StateInputManager
{

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
            m_FrameInputs[frameNumber].m_FrameNumber = frameNumber;
            m_FrameInputs[frameNumber].m_P1Input = inputs[0];
            m_FrameInputs[frameNumber].m_P2Input = inputs[1];
        }
        else
        {
            m_FrameInputs[frameNumber] = new PlayerInputs()
            {
                m_FrameNumber = frameNumber,
                m_P1Input = inputs[0],
                m_P2Input = inputs[1]
            };
        }
    }
}