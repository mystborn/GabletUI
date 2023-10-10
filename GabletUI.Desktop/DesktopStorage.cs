using GabletUI.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomlyn;
using Tomlyn.Model;

namespace GabletUI.Desktop
{
    public class DesktopStorage : IStorage
    {
        private string _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Gablet",
            "Data.toml");

        private TomlTable _settings;

        public DesktopStorage()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

            if (!File.Exists(_filePath))
            {
                using (File.Create(_filePath)) ;
            }

            var settings = File.ReadAllText(_filePath);
            _settings = Toml.ToModel(settings, _filePath);
        }

        public byte GetByte(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (byte)result;

            return default;
        }

        public double GetDouble(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (double)result;
            return default;
        }

        public float GetFloat(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (float)result;
            return default;
        }

        public int GetInt(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (int)result;
            return default;
        }

        public long GetLong(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (long)result;
            return default;
        }

        public sbyte GetSByte(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (sbyte)result;
            return default;
        }

        public short GetShort(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (short)result;
            return default;
        }

        public string GetString(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (string)result;
            return default;
        }

        public uint GetUInt(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (uint)result;
            return default;
        }

        public ulong GetULong(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (ulong)result;
            return default;
        }

        public ushort GetUShort(string key)
        {
            if (_settings.TryGetValue(key, out var result))
                return (ushort)result;
            return default;
        }

        public void Set(string key, string value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, sbyte value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, byte value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, short value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, ushort value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, int value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, uint value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, long value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, ulong value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, float value)
        {
            _settings[key] = value;
            SaveModel();
        }

        public void Set(string key, double value)
        {
            _settings[key] = value;
            SaveModel();
        }

        private void SaveModel()
        {
            var text = Toml.FromModel(_settings);
            File.WriteAllText(_filePath, text);
        }
    }
}
