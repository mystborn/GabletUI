using Android.App;
using Android.Content;
using GabletUI.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GabletUI.Android
{
    public class AndroidStore : IStorage
    {
        private ISharedPreferences _prefs;

        public AndroidStore(Application app)
        {
            _prefs = app.GetSharedPreferences("GabletStore", FileCreationMode.Append)
                ?? throw new InvalidOperationException("Failed to load Android shared preferences");
        }

        public byte GetByte(string key) => (byte)_prefs.GetInt(key, 0);

        // Todo: Needs to be a reinterpret cast
        public double GetDouble(string key) => (double)_prefs.GetFloat(key, 0);

        public float GetFloat(string key) => _prefs.GetFloat(key, 0);

        public int GetInt(string key) => _prefs.GetInt(key, 0);

        public long GetLong(string key) => _prefs.GetLong(key, 0);

        public sbyte GetSByte(string key) => (sbyte)_prefs.GetInt(key, 0);

        public short GetShort(string key) => (short)_prefs.GetInt(key, 0);

        public string? GetString(string key) => _prefs.GetString(key, "");

        public uint GetUInt(string key) => (uint)_prefs.GetInt(key, 0);

        public ulong GetULong(string key) => (ulong)_prefs.GetLong(key, 0);

        public ushort GetUShort(string key) => (ushort)_prefs.GetInt(key, 0);

        public void Set(string key, string value)
        {
            var result = _prefs
                .Edit()?
                .PutString(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, sbyte value)
        {
            var result = _prefs
                .Edit()?
                .PutInt(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, byte value)
        {
            var result = _prefs
                .Edit()?
                .PutInt(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, short value)
        {
            var result = _prefs
                .Edit()?
                .PutInt(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, ushort value)
        {
            var result = _prefs
                .Edit()?
                .PutInt(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, int value)
        {
            var result = _prefs
                .Edit()?
                .PutInt(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, uint value)
        {
            var result = _prefs
                .Edit()?
                .PutInt(key, (int)value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, long value)
        {
            var result = _prefs
                .Edit()?
                .PutLong(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, ulong value)
        {
            var result = _prefs
                .Edit()?
                .PutLong(key, (long)value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, float value)
        {
            var result = _prefs
                .Edit()?
                .PutFloat(key, value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }

        public void Set(string key, double value)
        {
            var result = _prefs
                .Edit()?
                .PutFloat(key, (float)value)?
                .Commit();

            if (result is null)
                throw new InvalidCastException("Failed to get editor for Android shared preferences");
        }
    }
}
