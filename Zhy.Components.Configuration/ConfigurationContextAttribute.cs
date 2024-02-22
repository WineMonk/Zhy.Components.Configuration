using System;

namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置上下文特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ConfigurationContextAttribute : Attribute
    {
        private bool _isHotLoad;
        /// <summary>
        /// 配置文件是否热重载（开启时将监测配置文件动态变化，修改后自动刷新至运行时）
        /// </summary>
        public bool IsHotload
        {
            get { return _isHotLoad; }
            set { _isHotLoad = value; }
        }
        /// <summary>
        /// 配置上下文特性
        /// </summary>
        public ConfigurationContextAttribute() { }
        /// <summary>
        /// 配置上下文特性
        /// </summary>
        /// <param name="isHotLoad">是否热重载</param>
        public ConfigurationContextAttribute(bool isHotLoad)
        {
            _isHotLoad = isHotLoad;
        }
    }
}
