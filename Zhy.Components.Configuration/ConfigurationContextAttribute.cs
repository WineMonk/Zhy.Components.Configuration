using System;

namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置上下文特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ConfigurationContextAttribute : Attribute
    {
        private bool _isHotLoad = true;
        /// <summary>
        /// 配置文件是否热重载，默认True。
        /// </summary>
        /// <remarks>
        /// 开启时将监测配置文件动态变化，修改后自动刷新至运行时。
        /// </remarks>
        public bool IsHotload
        {
            get { return _isHotLoad; }
            set { _isHotLoad = value; }
        }
        private bool _isInitialGeneration = true;
        /// <summary>
        /// 配置文件是否初始化生成，默认True。
        /// </summary>
        /// <remarks>
        /// 配置文件不存在时，根据默认配置生成配置文件。
        /// </remarks>
        public bool IsInitialGeneration
        {
            get { return _isInitialGeneration; }
            set { _isInitialGeneration = value; }
        }
        /// <summary>
        /// 配置上下文特性
        /// </summary>
        /// <param name="isHotLoad">配置文件是否热重载</param>
        /// <param name="isInitialGeneration">配置文件是否初始化生成</param>
        public ConfigurationContextAttribute(bool isHotLoad = true, bool isInitialGeneration = true)
        {
            _isHotLoad = isHotLoad;
            _isInitialGeneration = isInitialGeneration;
        }
    }
}
