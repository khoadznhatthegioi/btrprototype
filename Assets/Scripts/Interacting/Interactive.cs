using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Interact interact;
    public float distanceToPlayer;
    public bool isNearCam;
    public bool forcedUninteractive;
    public bool uninractive;
    //public Interactive()
    //{
    //    cam = Camera.main;
    //    player = cam.GetComponent<CameraFollow>().target.gameObject;
    //    interact = player.GetComponent<Interact>();
    //}

    protected void OnStart()
    {   
        cam = Camera.main;
        player = cam.GetComponent<CameraFollow>().target.gameObject;
        interact = player.GetComponent<Interact>();
    }

    protected void OnUpdate()
    {

        if (!uninractive)
            distanceToPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);
    }
    public void BeInteracted()
    {
        //Examine()

        if (this.GetType() == typeof(Collective))
        {
            Collective collective = (Collective)this;
            collective.Examine();
        }

        else if(this.GetType() == typeof(InspectedObject))
        {
            InspectedObject inspectedObject = (InspectedObject)this;    
            inspectedObject.Inspect();
        }
        else if(this.GetType() == typeof(Keyhole))
        {
            Keyhole key = (Keyhole)this;
            key.GetName();
        }
        else if (this.GetType() == typeof(Look))
        {
            Look look = (Look)this;
            look.Looked();
        }
    }

    public void StopBeingInteracted()
    {
        if (this.GetType() == typeof(Collective))
        {
            Collective collective = (Collective)this;
            collective.StopExamine();
        }

        else if (this.GetType() == typeof(InspectedObject))
        {
            InspectedObject inspectedObject = (InspectedObject)this;
            inspectedObject.StopInspecting();
        }
        else if (this.GetType() == typeof(Keyhole))
        {
            Keyhole key = (Keyhole)this;
            key.Out();
        }
    }

}
