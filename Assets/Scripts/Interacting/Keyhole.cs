using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : Interactive
{
    public static string currentName;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public void GetName()
    {
        currentName = gameObject.name;
        Time.timeScale = 0;
        PersistentManager.Instance.IsInspecting = true;
    }

    public void GetNameWhileInspecting()
    {
        currentName = gameObject.name;
    }

    public void Out()
    {
        currentName = null;
        Time.timeScale = 1;
        PersistentManager.Instance.IsInspecting = false;
    }
    
    public void OutWhileInspecting()
    {
        currentName = null;
    }
}
