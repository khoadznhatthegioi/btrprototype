using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }

    private bool isPaused;
    private bool isExamining;
    private bool isInspecting;
    private bool isInventoryOn; // True -> on
    private bool isMenuSettingsOn;
    private bool isPauseSettingsOn;

    public GameObject inventory; //make sure that inventory will not be destroy onload
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject settingOptions;
    


    public bool IsPaused 
    { 
        get 
        {
            return this.isPaused;
        }
        set 
        {
            if(!isInventoryOn && !isExamining && !isPauseSettingsOn && SceneManager.GetActiveScene().name != "mainmenu" && !DialogueManager.hasUnpausibleDialoguePlaying)
            {
                if (!isInspecting)
                {
                    isPaused = value;
                    pauseMenu.SetActive(value);
                    if (value == true)
                    {
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                }
                else
                {
                    isPaused = value;
                    pauseMenu.SetActive(value);
                }
            }
            
        } 
    }

    public bool IsExamining
    {
        get
        {
            return this.isExamining;
        }
        set
        {
            this.isExamining = value;
        }
    }
    
    public bool IsInspecting
    {
        get
        {
            return isInspecting;
        }
        set
        {
            isInspecting = value;
        }
    }

    public bool IsInventoryOn
    {
        get
        {
            return isInventoryOn;
        }
        set
        {
            if(!isExamining && !isPaused && SceneManager.GetActiveScene().name != "mainmenu" && !DialogueManager.hasUnpausibleDialoguePlaying)
            {
                if (!isInspecting)
                {
                    isInventoryOn = value;
                    inventory.SetActive(value);
                    if (value == true)
                    {
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Inventory.SetOriginal();
                        Time.timeScale = 1;
                    }
                }
                else
                {
                    isInventoryOn = value;
                    inventory.SetActive(value);
                    if (value == false)
                    {
                        Inventory.SetOriginal();
                    }
                }
            }
        }
    }

    public bool IsMenuSettingsOn
    {
        get { return isMenuSettingsOn; }    
        set 
        {
            if (value)
            {
                //
                mainMenu.SetActive(false);
                settingOptions.SetActive(true);

            }
            else
            {
                //
                mainMenu.SetActive(true);
                settingOptions.SetActive(false);
                //GetComponent<PreferencesSave>().SavePrefs();
            }
            isMenuSettingsOn = value; 
        }
    }

    public bool IsPauseSettingsOn
    {
        get { return isPauseSettingsOn; }
        set
        {
            if (value)
            {
                pauseMenu.SetActive(false);
                settingOptions.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(true);
                settingOptions.SetActive(false);
            }
            isPauseSettingsOn = value;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
