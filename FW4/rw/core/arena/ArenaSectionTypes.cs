using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW4.rw.core;

namespace FW4.rw.core.arena
{
    public class ArenaSectionTypes : ArenaSection
    {
        public uint dictOffset { get; set; }
        public RWObjectTypes[] dict { get; set; }
    }
}
