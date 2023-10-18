using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Services.Store
{
    public interface IStorage
    {
        public string GetString(string key);
        public sbyte GetSByte(string key);
        public byte GetByte(string key);
        public short GetShort(string key);
        public ushort GetUShort(string key);
        public int GetInt(string key);
        public uint GetUInt(string key);
        public long GetLong(string key);
        public ulong GetULong(string key);
        public float GetFloat(string key);
        public double GetDouble(string key);

        public void Set(string key, string value);
        public void Set(string key, sbyte value);
        public void Set(string key, byte value);
        public void Set(string key, short value);
        public void Set(string key, ushort value);
        public void Set(string key, int value);
        public void Set(string key, uint value);
        public void Set(string key, long value);
        public void Set(string key, ulong value);
        public void Set(string key, float value);
        public void Set(string key, double value);
    }
}
