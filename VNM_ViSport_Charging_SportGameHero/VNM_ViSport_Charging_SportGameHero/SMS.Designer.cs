﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VNM_ViSport_Charging_SpamSms {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    public sealed partial class SMS : global::System.Configuration.ApplicationSettingsBase {
        
        private static SMS defaultInstance = ((SMS)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SMS())));
        
        public static SMS Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int RetryTimes {
            get {
                return ((int)(this["RetryTimes"]));
            }
            set {
                this["RetryTimes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int SendMTThreadTimeLoop {
            get {
                return ((int)(this["SendMTThreadTimeLoop"]));
            }
            set {
                this["SendMTThreadTimeLoop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int SendMTFromQueLoop {
            get {
                return ((int)(this["SendMTFromQueLoop"]));
            }
            set {
                this["SendMTFromQueLoop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("server=123.29.68.139;database=TTND;uid=ttndacc;pwd=Swe8234Ue3ND")]
        public string cnn {
            get {
                return ((string)(this["cnn"]));
            }
            set {
                this["cnn"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string isSendToGateway {
            get {
                return ((string)(this["isSendToGateway"]));
            }
            set {
                this["isSendToGateway"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public string ConcurentThread {
            get {
                return ((string)(this["ConcurentThread"]));
            }
            set {
                this["ConcurentThread"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("55")]
        public int MaxThread {
            get {
                return ((int)(this["MaxThread"]));
            }
            set {
                this["MaxThread"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3000")]
        public string PriceSC {
            get {
                return ((string)(this["PriceSC"]));
            }
            set {
                this["PriceSC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("VMGViSport")]
        public string UserName {
            get {
                return ((string)(this["UserName"]));
            }
            set {
                this["UserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("v@#port")]
        public string Password {
            get {
                return ((string)(this["Password"]));
            }
            set {
                this["Password"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1930")]
        public string CpID {
            get {
                return ((string)(this["CpID"]));
            }
            set {
                this["CpID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3000")]
        public string PriceSM {
            get {
                return ((string)(this["PriceSM"]));
            }
            set {
                this["PriceSM"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string IsTest {
            get {
                return ((string)(this["IsTest"]));
            }
            set {
                this["IsTest"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("172")]
        public string FailCharge {
            get {
                return ((string)(this["FailCharge"]));
            }
            set {
                this["FailCharge"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(092) Chuc mung! Quy khach da gia han thanh cong DV viSportSMS cua Vietnamobile ." +
            " Truy cap ngay http://viSport.vn de su dung dich vu. Cuoc phi ( 3000d/ 07 ngay) " +
            "De huy DK, soan: SM OFF gui 979. HT: 19001255")]
        public string Message {
            get {
                return ((string)(this["Message"]));
            }
            set {
                this["Message"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(092) Chuc mung! Quy khach da gia han thanh cong DV viSport cua Vietnamobile . Tr" +
            "uy cap ngay http://viSport.vn de su dung dich vu. Cuoc phi ( 3000d/ 07 ngay) De " +
            "huy DK, soan: SC OFF gui 979. HT: 19001255")]
        public string MessageSC {
            get {
                return ((string)(this["MessageSC"]));
            }
            set {
                this["MessageSC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3600000")]
        public int ProccessTimeLoop {
            get {
                return ((int)(this["ProccessTimeLoop"]));
            }
            set {
                this["ProccessTimeLoop"] = value;
            }
        }
    }
}
