using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance { get; private set; }
    [SerializeField] List<string> TriggerNames = new List<string>();  
    //public List<Transform> bscTriggersTransforms = new List<Transform>();
    [SerializeField] List<BetweenScenesTrigger> bscTriggers = new List<BetweenScenesTrigger>();

    public Dictionary<string, BetweenScenesTrigger> nameTriggersPairs = new Dictionary<string, BetweenScenesTrigger>();
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
    private void Start()
    {
        for (int i = 0; i < TriggerNames.Count; i++)
        {
            nameTriggersPairs.Add(TriggerNames[i], bscTriggers[i]);
        }
    }



    public void InitializeCamFollow(CameraFollow cameraFollow)
    {
        cameraFollow.enabled = false;
        cameraFollow.edgeLeft = false;
        cameraFollow.edgeRight = false;
        cameraFollow.left = false;
        cameraFollow.right = false;
        cameraFollow.viewportHitLeft = false;
        cameraFollow.viewportHitRight = false;
        cameraFollow.enabled = true;
    }
}
