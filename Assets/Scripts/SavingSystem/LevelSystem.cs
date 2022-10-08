using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour, ISaveable
{
    public static LevelSystem Instance { get; private set; }
    [SerializeField] public string scene = "01";
    public bool[] gameStates;


    private void Awake()
    {
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

    public object CaptureState()
    {
        return new SaveData
        {
            scene = scene,
            gameStates = gameStates
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        scene = saveData.scene;
        gameStates = saveData.gameStates;
    }


    [Serializable]
    private struct SaveData
    {
        public string scene;
        //public bool[] buttonsStatus;
        public bool[] gameStates;
    }
}
