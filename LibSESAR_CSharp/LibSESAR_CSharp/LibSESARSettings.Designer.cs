﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibSESAR_CSharp {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
    internal sealed partial class LibSESARSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static LibSESARSettings defaultInstance = ((LibSESARSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new LibSESARSettings())));
        
        public static LibSESARSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public uint UserCodeMinLength {
            get {
                return ((uint)(this["UserCodeMinLength"]));
            }
            set {
                this["UserCodeMinLength"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public uint IGSNMinLength {
            get {
                return ((uint)(this["IGSNMinLength"]));
            }
            set {
                this["IGSNMinLength"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("9")]
        public uint IGSNMaxLength {
            get {
                return ((uint)(this["IGSNMaxLength"]));
            }
            set {
                this["IGSNMaxLength"] = value;
            }
        }
    }
}
