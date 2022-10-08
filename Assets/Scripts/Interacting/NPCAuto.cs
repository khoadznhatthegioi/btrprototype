using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCAuto : Interactive
{
    [SerializeField] DialogueBox dialogueBox;
    public int id;
    DialogueBox objectInstantiated;

    public float desiredDistance;
    bool doOnce;
    bool doOnce2;
    public bool doRepeat;

    private void Start()
    {
        OnStart();
        id = interact.interactives.IndexOf(this);
    }
    void Update()
    {
        OnUpdate();

        if(distanceToPlayer <= desiredDistance)
        {
            
            if (!doOnce)
            {
                objectInstantiated = Instantiate(dialogueBox, StaticCanvas.Instance.gameObject.transform);
                objectInstantiated.id = id;
                objectInstantiated.StartDialogue();
                doOnce2 = false;
                doOnce = true;
            }
        }
        if (doRepeat)
        {
            if (doOnce && !doOnce2 && distanceToPlayer > desiredDistance)
            {
                Destroy(objectInstantiated.gameObject);
                doOnce = false;
                doOnce2 = true;

            }
        }
        
    }
}
