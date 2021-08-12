using System;
using System.Collections.Generic;

[Serializable]
public class ReplayData
{
    public GGPOGameState m_InitialState;
    public List<PlayerInputs> m_PlayerInputs;

    public ReplayData()
    {
        m_InitialState = new GGPOGameState();
        m_PlayerInputs = new List<PlayerInputs>();
    }
}
