using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public static bool doorPosition;
    public bool isLocked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            doorPosition = true;
            
        }
    }
    private void Update()
    {
        if (doorPosition && !isLocked && !PersistentManager.Instance.IsInventoryOn && !PersistentManager.Instance.IsPaused)
        {
            if (Input.GetButtonDown("Interact"))
            {
                IndentifyDoor();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorPosition = false;
            //adasjdsakdla
        }
    }
    public void IndentifyDoor()
    {
        doorPosition = false;
        switch (name)
        {
            case "DoorHouseFirstScene":
                SceneManager.LoadScene("02");
                break;
        }
    }
}
