﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW4.RW.Core.Arena
{

    public class ArenaSectionExternalArenas : ArenaSection
    {
        public uint dictOffset { get; set; }
        public uint[] dict { get; set; }
    }
}
