using System;

namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置项特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ConfigurationItemAttribute : Attribute
    {
        private string _name;
        private IConfigurationEncipher _encipher;
        private IConfigurationConverter _converter;
        /// <summary>
        /// 键值
        /// </summary>
        public string Name { get => _name; }
        /// <summary>
        /// 加密器
        /// </summary>
        public IConfigurationConverter Converter { get => _converter; }
        /// <summary>
        /// 转换器
        /// </summary>
        public IConfigurationEncipher Encipher { get => _encipher; }

        /// <summary>
        /// 配置项特性
        /// </summary>
        public ConfigurationItemAttribute() { }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="name">键值</param>
        public ConfigurationItemAttribute(string name)
        {
            _name = name;
        }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="encipherType">加密器</param>
        /// <param name="converterType">转换器</param>
        public ConfigurationItemAttribute(Type encipherType = null, Type converterType = null)
        {
            if (encipherType != null)
            {
                _encipher = Activator.CreateInstance(encipherType) as IConfigurationEncipher;
            }
            if (converterType != null)
            {
                _converter = Activator.CreateInstance(converterType) as IConfigurationConverter;
            }
        }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="name">键值</param>
        /// <param name="encipherType">加密器</param>
        /// <param name="converterType">转换器</param>
        public ConfigurationItemAttribute(string name, Type encipherType, Type converterType)
        {
            _name = name;
            if (encipherType != null)
            {
                _encipher = Activator.CreateInstance(encipherType) as IConfigurationEncipher;
            }
            if (converterType != null)
            {
                _converter = Activator.CreateInstance(converterType) as IConfigurationConverter;
            }
        }
    }
}
