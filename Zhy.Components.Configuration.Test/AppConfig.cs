using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components.Configuration.Test
{
    /// <summary>
    /// 配置上下文
    /// </summary>
    [ConfigurationContext(isHotLoad: true, isInitialGeneration: true)]
    public class AppConfig : ConfigurationContextBase
    {
        public string? ActiveToken { get; set; }

        [ConfigurationItem(key: "active_user")]
        public string? ActiveUser { get; set; }

        [ConfigurationItem(key: "active_user_pwd", encipherType: typeof(TestEncipher))]
        public string? ActiveUserPassword { get; set; }

        [ConfigurationItem(key: "request_timeout_s", defaultValue: 10, converterType: typeof(IntNullableConfigurationConverter))]
        public int? TimeOut { get; set; }

        #region 单例
        private static AppConfig? _instance;
        private AppConfig() { }
        public static AppConfig Instance
        {
            get => _instance ??= new AppConfig();
        }
        #endregion // 单例
        public override string GetPersistentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "conf\\app.config";
        }
    }
}
