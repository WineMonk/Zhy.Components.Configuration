using System;
using System.IO;
using System.Threading.Tasks;

namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置文件监视器
    /// </summary>
    internal class ConfigurationFileSystemWatcher : FileSystemWatcher
    {
        private IConfigurationContext _configurationContext;
        /// <summary>
        /// 配置文件监视器
        /// </summary>
        /// <param name="configurationContext">配置文件上下文</param>
        /// <exception cref="ArgumentNullException">配置文件上下文参数为空异常</exception>
        internal ConfigurationFileSystemWatcher(IConfigurationContext configurationContext)
        {
            if (configurationContext == null)
            {
                throw new ArgumentNullException(nameof(configurationContext));
            }
            //if (!File.Exists(configurationContext.GetPersistentPath()))
            //{
            //    throw new FileNotFoundException(configurationContext.GetPersistentPath());
            //}
            _configurationContext = configurationContext;
            string confPath = configurationContext.GetPersistentPath();
            string confFolder = System.IO.Path.GetDirectoryName(confPath);
            string confFileName = System.IO.Path.GetFileName(confPath);
            if (!string.IsNullOrEmpty(confFolder))
            {
                Path = confFolder;
            }
            Filter = confFileName;
            NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Size;
            Changed += OnChanged;
            Error += OnError;
            EnableRaisingEvents = true;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            ConfigurationFileSystemWatcher configurationFileSystemWatcher = sender as ConfigurationFileSystemWatcher;
            if (configurationFileSystemWatcher == null)
            {
                return;
            }
            Task.Run(async () =>
            {
                bool isSuccess = false;
                int retryNumber = 0;
                while (retryNumber < 10 && !isSuccess)
                {
                    try
                    {
                        await Task.Delay(500);
                        configurationFileSystemWatcher._configurationContext.Reload();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        retryNumber++;
                    }
                }
            });
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            throw e.GetException();
        }
    }
}
