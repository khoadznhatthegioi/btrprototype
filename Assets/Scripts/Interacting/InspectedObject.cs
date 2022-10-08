using UnityEngine;

public class InspectedObject : Interactive
{
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public void Inspect()
    {
        Debug.Log("Inspecting");
        Time.timeScale = 0;
        PersistentManager.Instance.IsInspecting = true; 
    }

    public void StopInspecting()
    {
        Time.timeScale = 1;
        PersistentManager.Instance.IsInspecting = false;
    }
    // su dung dictionary, list de check bool xem dang gan object hay khong
    // vi du: cai den:true, cai tu:false
    // neu ca hai cung true thi xem cai nao gan hon thi interact duoc voi cai do
    // thay vi examine doi thanh cai khac, nhu interact
}
