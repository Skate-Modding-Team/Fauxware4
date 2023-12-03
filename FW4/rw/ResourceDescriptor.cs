using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW4.rw
{
    public class ResourceDescriptor
    {
        public BaseResourceDescriptor[] m_baseResourceDescriptors = new BaseResourceDescriptor[5]
        {
            new BaseResourceDescriptor(0,0),
            new BaseResourceDescriptor(0,0),
            new BaseResourceDescriptor(0,0),
            new BaseResourceDescriptor(0,0),
            new BaseResourceDescriptor(0,0)
        };

        public ResourceDescriptor(uint size, uint alignment)
        {
            for(byte i = 0; i<=4; i++)
            {
                m_baseResourceDescriptors[i].m_size = 0;
                m_baseResourceDescriptors[i].m_alignment = 1;
            }
            m_baseResourceDescriptors[0] = new BaseResourceDescriptor(size, alignment);
        }
    }
}
