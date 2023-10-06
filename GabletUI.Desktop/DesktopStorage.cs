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
            if(!Directory.Exists(Path.GetDirectoryName(_filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

            if (!File.Exists(_filePath))
            {
                using (File.Create(_filePath)) ;
            }

            var settings = File.ReadAllText(_filePath);
            _settings = Toml.ToModel(settings, _filePath);
        }

        public byte GetByte(string key) => (byte)_settings[key];

        public double GetDouble(string key) => (double)_settings[key];

        public float GetFloat(string key) => (float)_settings[key];

        public int GetInt(string key) => (int)_settings[key];

        public long GetLong(string key) => (long)_settings[key];

        public sbyte GetSByte(string key) => (sbyte)_settings[key];

        public short GetShort(string key) => (short)_settings[key];

        public string GetString(string key) => (string)_settings[key];

        public uint GetUInt(string key) => (uint)_settings[key];

        public ulong GetULong(string key) => (ulong)_settings[key];

        public ushort GetUShort(string key) => (ushort)_settings[key];

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
