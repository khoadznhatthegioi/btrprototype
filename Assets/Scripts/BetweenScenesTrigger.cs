using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenScenesTrigger : MonoBehaviour
{
    public bool loadScene;

    public Vector3 initialTransform;
    //public Transform cameraDesiredTransform;
    public CameraFollow camFollow;

    private float xClampLeft;
    private float xClampRight; // KHI NAO CAN THI PUBLIC

    float viewportTriggerPos;
    float viewportCameraPos;
    float cameraPos;

    public bool useTriggerClamp;

    private float initialOpClamp;
    public float clampOtherScene;

    public float playerDistance;
    public bool leftFirst;

    public string sceneToLoad;


    List<BetweenScenesTrigger> duplicators =new();
    bool duplicated;

    public void Awake()
    {
        foreach (BetweenScenesTrigger duplicator in GameObject.FindObjectsOfType<BetweenScenesTrigger>())
        {
            if(duplicator.name == this.name)
                duplicators.Add(duplicator);
        }
        if (duplicators.Count > 1)
        {
            foreach (BetweenScenesTrigger duplicate in duplicators)
            {
                if(duplicators.IndexOf(duplicate) != 0)
                {
                    Destroy(duplicate.gameObject);
                }
            }
        }
    }

    void Start()
    {
        camFollow = Camera.main.GetComponent<CameraFollow>();
        viewportTriggerPos = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewportCameraPos = leftFirst ? viewportTriggerPos - 0.5f : viewportTriggerPos + 0.5f;
        cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(viewportCameraPos, 0, 0)).x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           
            initialTransform = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
           
            if(loadScene)
                SceneManager.LoadScene(sceneToLoad);  
            else
            {
                TriggerManager.Instance.InitializeCamFollow(camFollow);
                if (!useTriggerClamp)
                {
                    camFollow.clampLeft = xClampLeft;
                    camFollow.clampRight = xClampRight;
                }
                else if (useTriggerClamp)
                {
                    if (leftFirst)
                    {
                        initialOpClamp = camFollow.clampRight;
                        camFollow.clampRight = this.transform.position.x;
                        camFollow.clampLeft = clampOtherScene;
                        clampOtherScene = initialOpClamp;
                    }
                    else
                    {
                        initialOpClamp = camFollow.clampLeft;
                        camFollow.clampLeft = this.transform.position.x;
                        camFollow.clampRight = clampOtherScene;
                        clampOtherScene = initialOpClamp;
                    }
                }

                Camera.main.transform.position = new Vector3(cameraPos, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
            cameraPos = initialTransform.x;

            if (leftFirst)
            {
                camFollow.target.position -= new Vector3(playerDistance, 0, 0);
                leftFirst = false;
            }
            else
            {
                camFollow.target.position += new Vector3(playerDistance, 0, 0);
                leftFirst=true;
            }
        }
    }
}
