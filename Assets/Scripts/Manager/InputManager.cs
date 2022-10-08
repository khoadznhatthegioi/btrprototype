using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
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

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PersistentManager.Instance.IsPaused = !PersistentManager.Instance.IsPaused;
        }

        if (Input.GetButtonDown("OpenInventory"))
        { 
            PersistentManager.Instance.IsInventoryOn = !PersistentManager.Instance.IsInventoryOn;
        }
    }
}
