using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    UniversalAdditionalCameraData universalAdditional;
    [SerializeField] private UniversalRenderPipelineAsset urpAsset;
    [SerializeField] Image panel;

    //------------------GRAPHIC SETTINGS------------------//
    public Dropdown AaDropdown;
    public Dropdown QualityIndex;
    public Dropdown MsaaMode;
    public Toggle IsFullscreen;
    public Slider Brightness;
    public Button _DefaultBrightness;
    public Toggle VsyncOn;
    
    //------------------VOLUME SETTINGS--------------------//
    public Slider MasterVolume;

    // INITIAL SETTINGS
    public Dropdown Languages;
    
    //------------------------------------------------------------------------------------
    private void Awake()
    {
        universalAdditional = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GetComponent<PreferencesSave>().LoadPrefs();
        LoadAllSettings();
    }
    private void OnLevelWasLoaded(int level)
    {
        universalAdditional = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        OnAAChange();
        OnLanguageChange();
    }
    public void OnQualityIndexChange()
    {
        QualitySettings.SetQualityLevel(QualityIndex.value);
        switch (QualityIndex.value)
        {
            default:
                AaDropdown.value = 0;
                MsaaMode.value = 0;
                break;
            case 1:
                AaDropdown.value = 0;
                MsaaMode.value = 0;
                break;
            case 2:
                AaDropdown.value = 1;
                MsaaMode.value = 1;
                break;
            case 3:
                AaDropdown.value = 1;
                MsaaMode.value = 2;
                break;
            case 4:
                AaDropdown.value = 1;
                MsaaMode.value = 3;
                break;
            case 5:
                AaDropdown.value = 1;
                MsaaMode.value = 3;
                break;
        }
    }
    public void QualityIndexLoad()
    {
        QualitySettings.SetQualityLevel(QualityIndex.value);
    }
    public void OnAAChange()
    {
        universalAdditional.antialiasing = (AntialiasingMode)AaDropdown.value;
    }
    public void OnMsaaChange()
    {
        switch (MsaaMode.value)
        {
            case 0:
                urpAsset.msaaSampleCount = (int)MsaaQuality.Disabled;
                break;
            case 1:
                urpAsset.msaaSampleCount = (int)MsaaQuality._2x;
                break;
            case 2:
                urpAsset.msaaSampleCount = (int)MsaaQuality._4x;
                break;
            case 3:
                urpAsset.msaaSampleCount = (int)MsaaQuality._8x;
                break;
        }   
    }
    public void OnFullscreenModeChange()
    {
        Screen.fullScreen = IsFullscreen.isOn;
    }
    public void OnVolumeChange()
    {
        AudioListener.volume = MasterVolume.value;
    }
    public void OnVsyncChange()
    {
        switch (VsyncOn.isOn)
        {
            case true:
                QualitySettings.vSyncCount = 1;
                break;
            default:
                QualitySettings.vSyncCount = 0;
                break;
        }
    }
    public void OnBrightnessChange()
    {
        //brightnessca.postExposure.value = Brightness.value;
        var alphaValue = (Brightness.value-Brightness.minValue)/(Brightness.maxValue-Brightness.minValue)*(110)-55f;
        panel.color = new Color32((byte)Brightness.value, (byte)Brightness.value, (byte)Brightness.value, (byte)Mathf.Abs(alphaValue)); 

    }

    public void DefaultBrightness()
    {
        Brightness.normalizedValue = 0.5f;
    }

    public void OnLanguageChange()
    {
        LanguageManager.language = (LanguageManager.Language)Languages.value;
        GetComponent<PreferencesSave>().SaveLanguage();
    }

    public void LoadAllSettings()
    {
        QualityIndexLoad();
        OnAAChange();
        OnFullscreenModeChange();
        OnVolumeChange();
        OnMsaaChange();
        OnBrightnessChange();
        OnVsyncChange();
        OnLanguageChange();
    }
}
