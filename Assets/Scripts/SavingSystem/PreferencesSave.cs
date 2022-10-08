using UnityEngine;
using System;

public class PreferencesSave : MonoBehaviour
{
    const string MasterVolumeKey = "Volume";
    const string AntiAliasingModeKey = "AA";
    const string GraphicsQuality = "GraphicsQuality";
    const string FullScreenMode = "FullScreenMode";
    const string MsaaMode = "M";
    const string BrightnessValue = "BrightnessValue";
    const string VsyncOn = "VsyncOn";
    const string LanguagesKey = "Langueash";
    
    [ContextMenu("Save")]
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, SettingsManager.Instance.MasterVolume.value);
        PlayerPrefs.SetInt(GraphicsQuality, SettingsManager.Instance.QualityIndex.value);
        PlayerPrefs.SetInt(AntiAliasingModeKey, SettingsManager.Instance.AaDropdown.value);
        PlayerPrefs.SetInt(FullScreenMode, /*PersistentManager.Instance.ConvertBoolToInt(SettingsManager.Instance.IsFullscreen.isOn*/ Convert.ToInt32(SettingsManager.Instance.IsFullscreen.isOn));
        PlayerPrefs.SetFloat(BrightnessValue, SettingsManager.Instance.Brightness.normalizedValue);
        PlayerPrefs.SetInt(MsaaMode, SettingsManager.Instance.MsaaMode.value);
        PlayerPrefs.SetInt(VsyncOn, Convert.ToInt32(SettingsManager.Instance.VsyncOn.isOn));
        PlayerPrefs.SetInt(LanguagesKey, SettingsManager.Instance.Languages.value);
        PlayerPrefs.Save();
    }

    [ContextMenu("Load")]
    public void LoadPrefs()
    {
        var masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1);
        var antiAliasingMode = PlayerPrefs.GetInt(AntiAliasingModeKey, 2);
        var graphicsQuality = PlayerPrefs.GetInt(GraphicsQuality, 4);
        var fullScreenMode = PlayerPrefs.GetInt(FullScreenMode, 1);
        var brightnessValue = PlayerPrefs.GetFloat(BrightnessValue, 0.5f);
        var msaaMode = PlayerPrefs.GetInt(MsaaMode, 0);
        var vsyncOn = PlayerPrefs.GetInt(VsyncOn, 0);
        var languageValue = PlayerPrefs.GetInt(LanguagesKey, 0);
        SettingsManager.Instance.MasterVolume.value = masterVolume;
        SettingsManager.Instance.QualityIndex.value = graphicsQuality;
        SettingsManager.Instance.AaDropdown.value = antiAliasingMode;
        SettingsManager.Instance.IsFullscreen.isOn = /*PersistentManager.Instance.ConvertIntToBool(fullScreenMode)*/ Convert.ToBoolean(fullScreenMode);
        SettingsManager.Instance.MsaaMode.value = msaaMode;
        SettingsManager.Instance.Brightness.normalizedValue = brightnessValue;
        SettingsManager.Instance.VsyncOn.isOn = Convert.ToBoolean(vsyncOn);
        SettingsManager.Instance.Languages.value = languageValue;
        LanguageManager.language = (LanguageManager.Language)SettingsManager.Instance.Languages.value;
    }

    [ContextMenu("Reset")]

    private void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveLanguage()
    {
        PlayerPrefs.SetInt(LanguagesKey, SettingsManager.Instance.Languages.value);
        PlayerPrefs.Save();
    }
}
