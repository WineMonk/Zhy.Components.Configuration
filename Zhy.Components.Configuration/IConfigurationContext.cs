namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置上下文接口
    /// </summary>
    public interface IConfigurationContext
    {
        /// <summary>
        /// 重载
        /// </summary>
        void Reload();
        /// <summary>
        /// 持久化
        /// </summary>
        void Persistent();
        /// <summary>
        /// 获取持久化路径
        /// </summary>
        /// <returns>持久化路径</returns>
        string GetPersistentPath();
    }
}
