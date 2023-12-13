using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW4.RW.Core;

namespace FW4.RW.Core.Arena
{
    public class ArenaSectionTypes : ArenaSection
    {
        public uint dictOffset { get; set; }
        public ERWObjectTypes[] dict { get; set; }
    }
}
