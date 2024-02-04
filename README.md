# Zhy.Components.Configuration

## Doc

[主 页](https://shaoshao.net.cn)

[GitHub](https://github.com/WineMonk/Zhy.Components.Configuration.git)

[API帮助文档]()

Config配置文件组件，支持运行时读取、写入配置，动态监测配置文件无感刷新、加解密、运行时类型转换器...

## Demo

加密器：

```csharp
/// <summary>
/// 加密器
/// </summary>
public class TestEncipher : IConfigurationEncipher
{
    public string Decrypt(string ciphertext)
    {
        return ciphertext.Replace("加密了：", "");
    }

    public string Encrypt(string plaintext)
    {
        return "加密了：" + plaintext;
    }
}
```

转换器：

```csharp
/// <summary>
/// 转换器
/// </summary>
public class IntNullableConfigurationConverter : IConfigurationConverter
{
    public object Read(string value)
    {
        if (value == null)
        {
            return null;
        }
        int? valueAsInt = int.Parse(value);
        return valueAsInt;
    }

    public string Write(object value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        return value + "";
    }
}
```

配置上下文：

```csharp
/// <summary>
/// 配置上下文
/// </summary>
[ConfigurationContext(IsHotload = true)]
public class AppConfig : ConfigurationContextBase
{
    public string? ActiveToken { get; set; }
    
    [ConfigurationItem("active_user")]
    public string? ActiveUser { get; set; }

    [ConfigurationItem("active_user_pwd", encipherType: typeof(TestEncipher))]
    public string? ActiveUserPassword { get; set; }

    [ConfigurationItem("request_timeout_s", converterType: typeof(IntNullableConfigurationConverter))]
    public int? TimeOut { get; set; }

    private static AppConfig? _instance;
    private AppConfig() { }
    public static AppConfig Instance
    {
        get => _instance ??= new AppConfig();
    }
    public override string GetPersistentPath()
    {
        return AppDomain.CurrentDomain.BaseDirectory + "bin\\conf\\app.config";
    }
}
```

配置文件：

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <appSettings>
        <add key="active_user" value="admin" />
        <add key="active_user_pwd" value="加密了：admin123" />
        <add key="request_timeout_s" value="10" />
    </appSettings>
</configuration>
```

