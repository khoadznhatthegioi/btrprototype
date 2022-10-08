using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class StaticCanvas : MonoBehaviour, ISaveable
{
    public static StaticCanvas Instance { get; private set; }
    public Button continueButton;
    public GameObject warningMenu;
    public GameObject warningNewGame;

    public GameObject[] buttons;
    public Sprite[] sprites;
    private bool[] buttonsStatus;
    public bool[] ButtonsStatus
    {
        get { return buttonsStatus; }
        set { buttonsStatus = value; }
    }
    //public static GameObject[] Buttons
    //{
    //    get { return buttons; }
    //    set { buttons = value; }
    //}

    //public GameObject[] ButtonsPass;

    private void Start()
    {
        //for(int i = 0; i < ButtonsPass.Length; i++)
        //{
        //    Buttons[i] = ButtonsPass[i];
        //}
        if (!File.Exists(SavingLoading.Instance.SavePath))
        {
            continueButton.interactable = false;
        }
        if (SceneManager.GetActiveScene().name != "mainmenu")
            PersistentManager.Instance.mainMenu.SetActive(false);
    }
    private void Awake()
    {
        buttonsStatus = new bool[buttons.Length];
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            if (!File.Exists(SavingLoading.Instance.SavePath))
            {
                continueButton.interactable = false;
            }
        } 
    }
    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(buttonsStatus[i]);
        }
        
    }

    public object CaptureState()
    {
        return new SaveData
        {
            buttonsStatus = buttonsStatus,

        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        //scene = saveData.scene;
        buttonsStatus = saveData.buttonsStatus;
    }

    [Serializable]
    private struct SaveData
    {
        public bool[] buttonsStatus;
        //public bool[] buttonsStatus;
    }

    public void NewGame()
    {
        if (!File.Exists(SavingLoading.Instance.SavePath))
        {
            ConfirmNewGame();
            return;
        }
        warningNewGame.SetActive(true);
    }

    public void ConfirmNewGame()
    {
        PersistentManager.Instance.mainMenu.SetActive(false);
        warningNewGame.SetActive(false);
        SavingLoading.Instance.Delete();
        SceneManager.LoadSceneAsync("01");
    }

    public void CancelNewGame()
    {
        warningNewGame.SetActive(false);
    }

    public void Continue()
    {
        PersistentManager.Instance.mainMenu.SetActive(false);
        SavingLoading.Instance.Load();
    }

    public void Settings()
    {
        PersistentManager.Instance.IsMenuSettingsOn = true;
    }

    public void ConfirmSettings()
    {
        PersistentManager.Instance.gameObject.GetComponent<PreferencesSave>().SavePrefs();
        if(SceneManager.GetActiveScene().name == "mainmenu")
        {
            PersistentManager.Instance.IsMenuSettingsOn = false;
            return;
        }
        PersistentManager.Instance.IsPauseSettingsOn = false;

    }

    public void CancelSettings()
    {
        PersistentManager.Instance.gameObject.GetComponent<PreferencesSave>().LoadPrefs();
        if (SceneManager.GetActiveScene().name == "mainmenu")
        {
            PersistentManager.Instance.IsMenuSettingsOn = false;
            return;
        }
        PersistentManager.Instance.IsPauseSettingsOn = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        PersistentManager.Instance.IsPaused = false;
    }

    public void SettingsInGame()
    {
        PersistentManager.Instance.IsPauseSettingsOn = true;
    }

    public void BackToMenu()
    {
        warningMenu.SetActive(true);
    }

    public void RealBack()
    {
        PersistentManager.Instance.pauseMenu.SetActive(false);
        warningMenu.SetActive(false);
        PersistentManager.Instance.IsPaused = false;
        PersistentManager.Instance.mainMenu.SetActive(true);
        SceneManager.LoadSceneAsync("mainmenu");
    }

    public void CancelRealBack()
    {
        warningMenu.SetActive(false);
    }
}
