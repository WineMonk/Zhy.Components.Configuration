using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置上下文基类
    /// </summary>
    public abstract class ConfigurationContextBase : IConfigurationContext, IDisposable
    {

        private ConfigurationFileSystemWatcher _watcher;
        /// <summary>
        /// 配置上下文基类
        /// </summary>
        protected ConfigurationContextBase()
        {
            Reload();
            InitWatcher();
        }
        /// <summary>
        /// 获取持久化路径
        /// </summary>
        /// <returns>持久化路径</returns>
        public abstract string GetPersistentPath();
        /// <summary>
        /// 持久化
        /// </summary>
        public void Persistent()
        {
            System.Configuration.Configuration configuration = GetConfiguration();
            PropertyInfo[] propertyInfos = GetType().GetProperties();
            foreach (var prop in propertyInfos)
            {
                InjectItem(configuration, prop);
            }
            configuration.Save(ConfigurationSaveMode.Modified);
        }
        /// <summary>
        /// 重载
        /// </summary>
        public void Reload()
        {
            System.Configuration.Configuration configuration = GetConfiguration();
            PropertyInfo[] propertyInfos = GetType().GetProperties();
            foreach (var prop in propertyInfos)
            {
                ExtractItem(configuration, prop);
            }
        }

        private void InitWatcher()
        {
            Type type = GetType();
            ConfigurationContextAttribute configurationContextAttribute = type.GetCustomAttribute<ConfigurationContextAttribute>();
            if (configurationContextAttribute == null)
            {
                return;
            }
            bool isHotload = configurationContextAttribute.IsHotload;
            if (isHotload)
            {
                _watcher = new ConfigurationFileSystemWatcher(this);
            }
        }
        private System.Configuration.Configuration GetConfiguration()
        {
            string persistentPath = GetPersistentPath();
            string persistentDirectoryPath = Path.GetDirectoryName(persistentPath);
            if (persistentDirectoryPath == null)
            {
                throw new NotSupportedException($"非法路径：{persistentPath}");
            }
            if (!Directory.Exists(persistentDirectoryPath))
            {
                Directory.CreateDirectory(persistentDirectoryPath);
            }
            ExeConfigurationFileMap configurationFileMap = new ExeConfigurationFileMap();
            configurationFileMap.ExeConfigFilename = persistentPath;
            System.Configuration.Configuration configObject = ConfigurationManager
                .OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            return configObject;
        }
        private void InjectItem(System.Configuration.Configuration configuration, PropertyInfo prop)
        {
            ConfigurationItemAttribute configurationItemAttribute =
                    prop.GetCustomAttribute<ConfigurationItemAttribute>();
            if (configurationItemAttribute == null)
            {
                return;
            }
            object value = prop.GetValue(this) ?? string.Empty;
            string key = configurationItemAttribute.Name ?? prop.Name;
            if (configurationItemAttribute.Encipher != null)
            {
                value = configurationItemAttribute
                    .Encipher
                    .Encrypt(value.ToString());
            }
            if (configurationItemAttribute.Converter != null)
            {
                value = configurationItemAttribute
                    .Converter
                    .Write(value);
            }
            if (configuration.AppSettings.Settings.AllKeys.Contains(key))
            {
                configuration.AppSettings.Settings[key].Value = value.ToString();
            }
            else
            {
                configuration.AppSettings.Settings.Add(key, value.ToString());
            }

        }
        private void ExtractItem(System.Configuration.Configuration configuration, PropertyInfo prop)
        {
            ConfigurationItemAttribute configurationItemAttribute =
                     prop.GetCustomAttribute<ConfigurationItemAttribute>();
            if (configurationItemAttribute == null)
            {
                return;
            }
            string key = configurationItemAttribute.Name ?? prop.Name;
            object value = configuration.AppSettings.Settings[key]?.Value;
            if (configurationItemAttribute.Converter != null)
            {
                value = configurationItemAttribute
                    .Converter
                    .Read(value.ToString());
            }
            if (value == null)
            {
                return;
            }
            if (configurationItemAttribute.Encipher != null)
            {
                value = configurationItemAttribute.Encipher.Decrypt(value.ToString());
            }
            object convertedValue = value;
            prop.SetValue(this, convertedValue);
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _watcher?.Dispose();
        }
    }
}
