namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置转换器接口
    /// </summary>
    public interface IConfigurationConverter
    {
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="value">配置文件项值</param>
        /// <returns>运行时值</returns>
        object Read(string value);
        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="value">运行时值</param>
        /// <returns>配置文件项值</returns>
        string Write(object value);
    }
}