using System;
using System.Collections.Generic;

public abstract class AbstractStateBlock
{
    public abstract Player UpdatePlayer(Player player, long input);
}