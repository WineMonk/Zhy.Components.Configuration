namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置项特性
    /// </summary>
    public class ConfigurationItemAttribute : Attribute
    {
        private string? _name;
        private IConfigurationEncipher? _encipher;
        private IConfigurationConverter? _converter;
        /// <summary>
        /// 键值
        /// </summary>
        public string? Name { get => _name; }
        /// <summary>
        /// 加密器
        /// </summary>
        public IConfigurationConverter? Converter { get => _converter; }
        /// <summary>
        /// 转换器
        /// </summary>
        public IConfigurationEncipher? Encipher { get => _encipher; }

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
        /// <param name="configurationEncipherType">加密器</param>
        /// <param name="configurationConverterType">转换器</param>
        public ConfigurationItemAttribute(Type? configurationEncipherType = null, Type? configurationConverterType = null)
        {
            if (configurationEncipherType != null)
            {
                _encipher = Activator.CreateInstance(configurationEncipherType) as IConfigurationEncipher;
            }
            if (configurationConverterType != null)
            {
                _converter = Activator.CreateInstance(configurationConverterType) as IConfigurationConverter;
            }
        }
        /// <summary>
        /// 配置项特性
        /// </summary>
        /// <param name="name">键值</param>
        /// <param name="configurationEncipherType">加密器</param>
        /// <param name="configurationConverterType">转换器</param>
        public ConfigurationItemAttribute(string name, Type configurationEncipherType, Type configurationConverterType)
        {
            _name = name;
            if (configurationEncipherType != null)
            {
                _encipher = Activator.CreateInstance(configurationEncipherType) as IConfigurationEncipher;
            }
            if (configurationConverterType != null)
            {
                _converter = Activator.CreateInstance(configurationConverterType) as IConfigurationConverter;
            }
        }
    }
}
