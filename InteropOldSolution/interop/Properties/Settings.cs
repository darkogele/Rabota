// Decompiled with JetBrains decompiler
// Type: interop.Properties.Settings
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace interop.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings settings = Settings.defaultInstance;
        return settings;
      }
    }

    [SpecialSetting(SpecialSetting.WebServiceUrl)]
    [DefaultSettingValue("http://192.168.1.40:8010/SendEmailService.asmx")]
    [DebuggerNonUserCode]
    [ApplicationScopedSetting]
    public string interop_WSSendEmail_Service1
    {
      get
      {
        return (string) this["interop_WSSendEmail_Service1"];
      }
    }
  }
}
