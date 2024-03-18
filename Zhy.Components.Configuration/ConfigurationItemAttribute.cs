using System;

namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置项特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ConfigurationItemAttribute : Attribute
    {
        private string _key;
        private object _defaultValue;
        private IConfigurationEncipher _encipher;
        private IConfigurationConverter _converter;
        /// <summary>
        /// 键值
        /// </summary>
        public string Key { get => _key; }
        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get => _defaultValue; }
        /// <summary>
        /// 加密器
        /// </summary>
        public IConfigurationConverter Converter { get => _converter; }
        /// <summary>
        /// 类型转换器
        /// </summary>
        public IConfigurationEncipher Encipher { get => _encipher; }

        /// <summary>
        /// 配置项特性
        /// </summary>
        public ConfigurationItemAttribute() { }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="key">键值</param>
        public ConfigurationItemAttribute(string key)
        {
            _key = key;
        }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        public ConfigurationItemAttribute(string key, object defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;
        }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="converterType">转换器类型</param>
        public ConfigurationItemAttribute(string key, object defaultValue, Type converterType)
        {
            _key = key;
            _defaultValue = defaultValue;
            _converter = Activator.CreateInstance(converterType) as IConfigurationConverter;
        }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="converterType">转换器类型</param>
        /// <param name="encipherType">加密器类型</param>
        public ConfigurationItemAttribute(string key = null, object defaultValue = null, Type converterType = null, Type encipherType = null)
        {
            _key = key;
            _defaultValue = defaultValue;
            if (converterType != null)
            {
                _converter = Activator.CreateInstance(converterType) as IConfigurationConverter;
            }
            if (encipherType != null)
            {
                _encipher = Activator.CreateInstance(encipherType) as IConfigurationEncipher;
            }
        }
    }
}
