﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class GameManager
    {
        public GameState State;
    }

    public enum GameState
    {
        Beginning,
        MainMenu,
        CreatPlayer,
    }
}