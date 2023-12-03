using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW4.rw.core.arena
{
    public class ArenaSectionSubreferences : ArenaSection
    {
        public struct Record
        {
            public uint objectID { get; set; }
            public uint offset { get; set; }
        }

        public Record[] RecordEntries { get; set; }

        public uint m_dictAfterRefix { get; set; }
        public uint m_recordsAfterRefix { get; set; }
        public uint dict { get; set; }
        public uint records { get; set; }
        public uint numUsed { get; set; }
    }
}
