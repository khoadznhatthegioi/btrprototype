using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    //public Dictionary<GameObject, bool> isInteractivesNear = new Dictionary<GameObject, bool>();
    //public GameObject[] interactives;

    //public Collective[] collectives;
    public List<Interactive> interactives = new List<Interactive>();
    //public InspectedObject[] inspectedObjs;
    //public Collective[] collectivesFromIns;
    //public Dictionary<InspectedObject, Collective> inspectedColPairs;
    //public Dictionary<Collective, GameObject> inspectedObjectDesPairs;

    //public GameObject[][] descriptions;
    //public GameObject[] description
    //public Dictionary<Collective, GameObject[]> objectDesPairs = new Dictionary<Collective, GameObject[]>();

    //public Dictionary<Key, Collective> keyObjectPairs = new Dictionary<Key,Collective>();
    public Dictionary<Keyhole, Sprite> keySpr = new Dictionary<Keyhole, Sprite>();
    [SerializeField] public readonly float desiredDistance = 2.5f;
    Interactive current;
    //public GameObject inventory;

    Controller controller;
    #region
    //------------

    [SerializeField] float desriredSaturation;
    [SerializeField] float desiredTemperature;
    [SerializeField] float desiredFilmGrain;
    [SerializeField] float desiredPExposire;
    [SerializeField] float desiredContrast;
    [SerializeField] float desiredDof;
    [SerializeField] float desiredVignette;
    [SerializeField] float duration;

    [SerializeField] Volume volume;
    ColorAdjustments colorAdjustments;
    WhiteBalance whiteBalance;
    FilmGrain filmGrain;
    DepthOfField depthOfField;
    Vignette vignette;


    bool l, l1, e, e1;
    float startTime, tempS, tempS1, tempT, tempT1, tempF, tempF1, tempE, tempE1, tempC, tempC1, tempD, tempD1, tempV, tempV1;
    public static float initS, initT, initF, initE, initC, initD, initV; //if default values change in any time of the scene, please specify the init value again;
                                                                         //bool stopHalt;
                                                                         //float tempDuration;
                                                                         //float initSaturation, initT, initF, initE, 
    #endregion
    private void Start()
    {
        if (interactives[0] != null)
        {
            //for (var i = 0; i < interactives.Length; i++)
            //{
            //    if (interactives[i].GetType() == typeof(Collective))
            //        objectDesPairs.Add((Collective)interactives[i], descriptions[i]);
            //}
            #region
            volume.profile.TryGet(out colorAdjustments);
            volume.profile.TryGet(out whiteBalance);
            volume.profile.TryGet(out filmGrain);
            volume.profile.TryGet(out depthOfField);
            volume.profile.TryGet(out vignette);

            //initSaturation = colorAdjustments.saturation.value;
            //initT = 
            initS = colorAdjustments.saturation.value;
            initT = whiteBalance.temperature.value;
            initF = filmGrain.intensity.value;
            initE = colorAdjustments.postExposure.value;
            initC = colorAdjustments.contrast.value;
            initD = depthOfField.focalLength.value;
            initV = vignette.intensity.value;
            #endregion
        }
        controller = GetComponent<Controller>();
        //PersistentManager.Instance.InventoryObject = inventory;

    }

    void Update()
    {
        if (interactives[0] != null)
        {
            FindNearestObject();
            //FindNearestObject2();
            foreach (Interactive i in interactives)
            {
                if (i.isNearCam && Input.GetButtonDown("Interact") && !PersistentManager.Instance.IsInspecting && !PersistentManager.Instance.IsExamining && !PersistentManager.Instance.IsInventoryOn && !PersistentManager.Instance.IsPaused)
                {
                    InteractWithObject(i);

                }
            }

            if (Input.GetButtonDown("Accept"))
            {
                if (current)
                {
                    if (current.GetType() == typeof(Collective))
                    {
                        if ((PersistentManager.Instance.IsExamining || PersistentManager.Instance.IsInspecting) && !PersistentManager.Instance.IsInventoryOn && !PersistentManager.Instance.IsPaused && colorAdjustments.saturation.value == desriredSaturation)
                        {
                            current.StopBeingInteracted();
                            if (current.GetType() == typeof(Collective))
                            {
                                l = false;
                                e = false;

                                if (!l1)
                                {
                                    l1 = true;
                                    startTime = Time.unscaledTime;
                                    tempS1 = colorAdjustments.saturation.value;
                                    tempT1 = whiteBalance.temperature.value;
                                    tempF1 = filmGrain.intensity.value;
                                    tempE1 = colorAdjustments.postExposure.value;
                                    tempC1 = colorAdjustments.contrast.value;
                                    tempD1 = depthOfField.focalLength.value;
                                    tempV1 = vignette.intensity.value;
                                }
                                //already = false;

                                e1 = true;

                                if (controller.enabled == false && DialogueManager.Instance.collectiveTalk)
                                {
                                    controller.enabled = true;
                                    DialogueManager.Instance.collectiveTalk = false;
                                }
                            }


                        }
                    }
                    else
                    {
                        if ((PersistentManager.Instance.IsExamining || PersistentManager.Instance.IsInspecting) && !PersistentManager.Instance.IsInventoryOn && !PersistentManager.Instance.IsPaused)
                        {
                            current.StopBeingInteracted();

                        }
                    }
                }




            }

            if (e)
            {
                var t = (Time.unscaledTime - startTime) / duration;
                colorAdjustments.saturation.value = Mathf.SmoothStep(tempS, desriredSaturation, t);
                whiteBalance.temperature.value = Mathf.SmoothStep(tempT, desiredTemperature, t);
                filmGrain.intensity.value = Mathf.SmoothStep(tempF, desiredFilmGrain, t);
                colorAdjustments.postExposure.value = Mathf.SmoothStep(tempE, desiredPExposire, t);
                colorAdjustments.contrast.value = Mathf.SmoothStep(tempC, desiredContrast, t);
                depthOfField.focalLength.value = Mathf.SmoothStep(tempD, desiredDof, t);
                vignette.intensity.value = Mathf.SmoothStep(tempV, desiredVignette, t);
            }

            if (e1 /*&& !stopHalt*/)
            {

                var t = (Time.unscaledTime - startTime) / duration;
                colorAdjustments.saturation.value = Mathf.SmoothStep(tempS1, initS, t);
                whiteBalance.temperature.value = Mathf.SmoothStep(tempT1, initT, t);
                filmGrain.intensity.value = Mathf.SmoothStep(tempF1, initF, t);
                colorAdjustments.postExposure.value = Mathf.SmoothStep(tempE1, initE, t);
                colorAdjustments.contrast.value = Mathf.SmoothStep(tempC1, initC, t);
                depthOfField.focalLength.value = Mathf.SmoothStep(tempD1, initD, t);
                vignette.intensity.value = Mathf.SmoothStep(tempV1, initV, t);
                StartCoroutine(wait());
                IEnumerator wait()
                {
                    if (e1)
                    {
                        yield return new WaitForSecondsRealtime(duration);
                        e1 = false;
                        yield return null;
                    }

                }

                //if (already)
                //{
                //stopHalt = true;
                //}
                //StartCoroutine(waitToStop());
                //IEnumerator waitToStop()
                //{
                //    {
                //        yield return new WaitForSeconds(duration);
                //        //yield return new WaitForSecondsRealtime(duration);
                //        stopHalt = true;
                //        yield break; 
                //        //Debug.Log("dafuq");
                //    }

                //}
                //IEnumerator wait()
                //{
                //    yield return new WaitForSeconds(duration);
                //    already = true;
                //    yield break;
                //}

            }
        }


        if (Input.GetKey(KeyCode.K))
        {
            SceneManager.LoadScene("02");
            LevelSystem.Instance.scene = "02";
            //PersistentManager.loading = true;
        }
    }

    void FindNearestObject()
    {
        var smallest = interactives[0];
        for (var i = 0; i < interactives.Count; i++)
        {
            if (interactives[i].distanceToPlayer < smallest.distanceToPlayer && interactives[i].distanceToPlayer <= desiredDistance)
            {
                smallest = interactives[i];
            }
        }
        foreach (Interactive i in interactives)
        {
            i.isNearCam = false;
        }
        //if(!Interactive.isExamining)
        if (smallest.gameObject.activeInHierarchy && !smallest.forcedUninteractive && !DoorTrigger.doorPosition)
            smallest.isNearCam = smallest.distanceToPlayer <= desiredDistance;
        //else
        //{
        //    smallest.isNearCam = true;
        //}
    }

    public void InteractWithObject(Interactive i)
    {
        current = i;
        i.BeInteracted();

        if (i.GetType() == typeof(Collective))
        {
            l1 = false;
            e1 = false;
            if (!l)
            {
                l = true;
                startTime = Time.unscaledTime;
                tempS = colorAdjustments.saturation.value;
                tempT = whiteBalance.temperature.value;
                tempF = filmGrain.intensity.value;
                tempE = colorAdjustments.postExposure.value;
                tempC = colorAdjustments.contrast.value;
                tempD = depthOfField.focalLength.value;
                tempV = vignette.intensity.value;
            }
            e = true;
        }
    }
}
