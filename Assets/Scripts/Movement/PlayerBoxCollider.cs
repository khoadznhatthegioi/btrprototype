using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxCollider : MonoBehaviour
{
    public static float speed, interval;
    bool startedTrembling1;
    bool endTrembling1;
    public float duration1;
    float startTime1;
    public float slowedDownSpeed1;
    public float slowedDownInterval1;
    float temp1, temp01, tempi1, tempi01;
    bool firstTime1;

    [SerializeField] Controller controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (this.name)
            {
                case "Box Volume 1":
                    endTrembling1 = false;
                    startedTrembling1 = true;
                    if (firstTime1 == false)
                    {
                        temp1 = controller.speed;
                        tempi1 = controller.stepInterval;
                        firstTime1 = true;
                    }
                    startTime1 = Time.time;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (this.name)
            {
                case "Box Volume 1":
                    startedTrembling1 = false;
                    endTrembling1 = true;
                    temp01 = speed;
                    tempi01 = interval;
                    startTime1 = Time.time;
                    break;
            }
        }
    }
    private void Update()
    {
        
        if (startedTrembling1)
        {
            float t1 = (Time.time - startTime1) / duration1;
            speed = Mathf.Lerp(temp1, slowedDownSpeed1, t1);
            interval = Mathf.Lerp(tempi1, slowedDownInterval1, t1);
        }

        if (endTrembling1)
        {
            float t1 = (Time.time - startTime1) / duration1;
            speed = Mathf.Lerp(temp01, temp1, t1);
            interval = Mathf.Lerp(tempi01, tempi1, t1);
           
        }
    }
}
