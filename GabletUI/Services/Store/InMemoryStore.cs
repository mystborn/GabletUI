using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Services.Store
{
    public class InMemoryStore : IStorage
    {
        private Dictionary<string, object> _store = new Dictionary<string, object>();

        public byte GetByte(string key) => (byte)_store[key];

        public double GetDouble(string key) => (double)_store[key];

        public float GetFloat(string key) => (float)_store[key];

        public int GetInt(string key) => (int)_store[key];

        public long GetLong(string key) => (long)_store[key];

        public sbyte GetSByte(string key) => (sbyte)_store[key];

        public short GetShort(string key) => (short)_store[key];

        public string GetString(string key) => (string)_store[key];

        public uint GetUInt(string key) => (uint)_store[key];

        public ulong GetULong(string key) => (ulong)_store[key];

        public ushort GetUShort(string key) => (ushort)_store[key];

        public void Set(string key, string value) => _store[key] = value;

        public void Set(string key, sbyte value) => _store[key] = value;

        public void Set(string key, byte value) => _store[key] = value;

        public void Set(string key, short value) => _store[key] = value;

        public void Set(string key, ushort value) => _store[key] = value;

        public void Set(string key, int value) => _store[key] = value;

        public void Set(string key, uint value) => _store[key] = value;

        public void Set(string key, long value) => _store[key] = value;

        public void Set(string key, ulong value) => _store[key] = value;

        public void Set(string key, float value) => _store[key] = value;

        public void Set(string key, double value) => _store[key] = value;
    }
}
