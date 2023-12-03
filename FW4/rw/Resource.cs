using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW4.rw
{
    enum BaseResourceTypeNames
    {
        MainMemory,
        Disposable,
        Physical,
        Uninitialized,
        DisposableUnin
    }

    internal class Resource
    {
        public BaseResourceTypeNames GetBaseResourceTypeName(uint index)
        {
            if(index >= 5)
            {
                throw new Exception("index > number");
            }
            return (BaseResourceTypeNames)index;
        }
    }
}
