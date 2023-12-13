using FW4.RW.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FW4.Serialization
{
    public class RWSerializer
    {
        public RWSerializer() { }

        public byte[] Serialize(IRWObject obj, bool isBigEndian)
        {
            byte[] data = new byte[0];
            using (MemoryStream ms = new MemoryStream())
            {
                var Type = obj.GetType();
                FieldInfo[] fields = Type.GetFields();
                foreach (FieldInfo field in fields)
                {
                    if (Attribute.IsDefined(field, typeof(RWSerializeableAttribute)))
                    {
                        Type x = field.FieldType;
                        byte[] fieldBytes = new byte[0];
                        if (field.FieldType == typeof(sbyte))
                            fieldBytes = new byte[] { (byte)field.GetValue(obj) };
                        if (field.FieldType == typeof(byte))
                            fieldBytes = new byte[] { (byte)field.GetValue(obj) };
                        if (field.FieldType == typeof(int))
                            fieldBytes = BitConverter.GetBytes((int)field.GetValue(obj));
                        if (field.FieldType == typeof(uint))
                            fieldBytes = BitConverter.GetBytes((uint)field.GetValue(obj));
                        if (field.FieldType == typeof(short))
                            fieldBytes = BitConverter.GetBytes((short)field.GetValue(obj));
                        if (field.FieldType == typeof(ushort))
                            fieldBytes = BitConverter.GetBytes((ushort)field.GetValue(obj));
                        if (field.FieldType == typeof(long))
                            fieldBytes = BitConverter.GetBytes((long)field.GetValue(obj));
                        if (field.FieldType == typeof(ulong))
                            fieldBytes = BitConverter.GetBytes((ulong)field.GetValue(obj));
                        if (field.FieldType == typeof(float))
                            fieldBytes = BitConverter.GetBytes((float)field.GetValue(obj));

                        if (isBigEndian)
                            Array.Reverse(fieldBytes);

                        ms.Write(fieldBytes);
                    }
                }
                data = ms.ToArray();
            }
            return data;
        }
    }
}
