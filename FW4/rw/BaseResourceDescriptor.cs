using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW4.RW
{
    public class BaseResourceDescriptor
    {
        public UInt32 m_size;
        public UInt32 m_alignment;

        public BaseResourceDescriptor(UInt32 size, UInt32 alignment)
        {
            this.m_size = size;
            this.m_alignment = alignment;
        }
    }
}
