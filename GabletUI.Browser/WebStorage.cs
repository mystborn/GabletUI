using GabletUI.Store;
using System.Runtime.InteropServices.JavaScript;

namespace GabletUI.Browser
{
    public partial class WebStorage : IStorage
    {
        [JSImport("globalThis.testing")]
        public static partial void Testing();

        [JSImport("globalThis.gabletStorageGet")]
        public static partial string LocalStorageGet(string key);

        [JSImport("globalThis.gabletStorageSet")]
        public static partial void LocalStorageSet(string key, string value);

        public string GetString(string key) => LocalStorageGet(key);

        public sbyte GetSByte(string key) => sbyte.Parse(LocalStorageGet(key));

        public byte GetByte(string key) => byte.Parse(LocalStorageGet(key));

        public short GetShort(string key) => short.Parse(LocalStorageGet(key));

        public ushort GetUShort(string key) => ushort.Parse(LocalStorageGet(key));

        public int GetInt(string key) => int.Parse(LocalStorageGet(key));

        public uint GetUInt(string key) => uint.Parse(LocalStorageGet(key));

        public long GetLong(string key) => long.Parse(LocalStorageGet(key));

        public ulong GetULong(string key) => ulong.Parse(LocalStorageGet(key));

        public float GetFloat(string key) => float.Parse(LocalStorageGet(key));

        public double GetDouble(string key) => double.Parse(LocalStorageGet(key));

        public void Set(string key, string value)
        {
            LocalStorageSet(key, value);
        }

        public void Set(string key, sbyte value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, byte value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, short value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, ushort value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, int value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, uint value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, long value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, ulong value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, float value)
        {
            LocalStorageSet(key, value.ToString());
        }

        public void Set(string key, double value)
        {
            LocalStorageSet(key, value.ToString());
        }
    }
}
