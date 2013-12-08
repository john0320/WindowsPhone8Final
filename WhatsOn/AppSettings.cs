using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Collections.Generic;

namespace WhatsOn
{
    public class AppSettings
    {
        IsolatedStorageSettings settings;

        const string RadioButton1SettingKeyName = "UseGeoLocation";
        const string RadioButton2SettingKeyName = "UsePostalCode";
        const string ChooseProviderIDSettingKeyName = "ChooseProviderID";
        const string ChooseProviderKeyName = "Provider";
        const string ListBoxSettingKeyName = "ListBoxSetting";
        const string PostalCodeKeyName = "PostalCodeSetting";
        const string LocationKeyName = "LocationSetting";

        const bool RadioButton1SettingDefault = true;
        const bool RadioButton2SettingDefault = false;
        const string ChooseProviderIDSettingDefault = "";
        const string ChooseProviderSettingDefault = "";
        const int ListBoxSettingDefault = 0;
        const string PostalCodeSettingDefault = "";
        const string LocationSettingDefault = "";


        public AppSettings()
        {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool)
            {
                settings = IsolatedStorageSettings.ApplicationSettings;
            }
        }


        public string apiKey
        {
            get
            {
                { return "api_key=7bd7c3b4c3ceb3c4572de29de52bc5c5"; }
            }
            set { apiKey = value; }
        }



        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            if (settings.Contains(Key))
            {
                if (settings[Key] != value)
                {
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }


        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            else
            {
                value = defaultValue;
            }
            return value;
        }


        public void Save()
        {
            settings.Save();
        }

        public int ListBoxSetting
        {
            get
            {
                return GetValueOrDefault<int>(ListBoxSettingKeyName, ListBoxSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(ListBoxSettingKeyName, value))
                {
                    Save();
                }
            }
        }


        public bool RadioButton1Setting
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton1SettingKeyName, RadioButton1SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton1SettingKeyName, value))
                {
                    Save();
                }
            }
        }


        public bool RadioButton2Setting
        {
            get
            {
                return GetValueOrDefault<bool>(RadioButton2SettingKeyName, RadioButton2SettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(RadioButton2SettingKeyName, value))
                {
                    Save();
                }
            }
        }

        public string ChooseProviderID
        {
            get
            {
                return GetValueOrDefault<string>(ChooseProviderIDSettingKeyName, ChooseProviderIDSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(ChooseProviderIDSettingKeyName, value))
                {
                    Save();
                }
            }
        }


        public string ChooseProvider
        {
            get
            {
                return GetValueOrDefault<string>(ChooseProviderKeyName, ChooseProviderSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(ChooseProviderKeyName, value))
                {
                    Save();
                }
            }
        }



        public string PostalCodeSetting
        {
            get
            {
                return GetValueOrDefault<string>(PostalCodeKeyName, PostalCodeSettingDefault);
            }
            set
            {
                if (AddOrUpdateValue(PostalCodeKeyName, value.ToUpper().Replace(" ", string.Empty)))
                {
                    Save();
                }
            }
        }

    }
}