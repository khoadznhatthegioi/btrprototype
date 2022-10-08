using UnityEngine;

public class Collective : Interactive
{
    //public GameObject player;
    //Camera cam;
    //Interact interact;
    //public float distanceToPlayer;
    //public bool isNearCam;
    //public static bool isExamining;
    bool isExamined;
    public GameObject[] descriptions;
    [SerializeField] bool isDocument;
    [SerializeField] GameObject[] doc;
    //bool initialized;
    bool isReading;
    public bool takeable;
    public int objectOrder;

    int languageIndex;
    DialogueTrigger dialogueTrigger;
    Controller controller;
    //[SerializeField] GameObject inventoryButton;

    private void Start()
    {
        OnStart();
        controller = player.GetComponent<Controller>();
        
    }

    void Update()
    {
        base.OnUpdate();
        switch (LanguageManager.language)
        {
            default:
                languageIndex = 0;
                break;
            case LanguageManager.Language.English:
                languageIndex = 1;
                break;
            case LanguageManager.Language.Francais:
                languageIndex = 2;
                break;
            case LanguageManager.Language.Nihon:
                languageIndex = 3;
                break;
            case LanguageManager.Language.Korean:
                languageIndex = 4;
                break;
            case LanguageManager.Language.TraditionalChinese:
                languageIndex = 5;
                break;
            case LanguageManager.Language.SimplifiedChinese:
                languageIndex= 6;
                break;
        }

        //if (!initialized)
        //{
        //    descriptions = interact.objectDesPairs[this];
        //    initialized = true;
        //}
        if (isDocument && isExamined)
        {
            if (Input.GetButtonDown("Read"))
            {
                doc[languageIndex].SetActive(!isReading);
                isReading = doc[languageIndex].activeInHierarchy;
            }
        }
    }

    public void Examine()
    {
        descriptions[languageIndex].SetActive(true);
        //isNearCam = true;
        Time.timeScale = 0;
        isExamined = true;
        //StaticCanvas.Buttons[objectOrder].SetActive(true);
        StaticCanvas.Instance.ButtonsStatus[objectOrder] = true;
        PersistentManager.Instance.IsExamining = true;
    }

    public void StopExamine()
    {
        if (isDocument)
            doc[languageIndex].SetActive(false);
        descriptions[languageIndex].SetActive(false);
        Time.timeScale = 1;
        isExamined = false;
        if (!isDocument)
        {
            isNearCam = false;
            //gameObject.SetActive(false);
            //isNearCam = false;
        }
        if (takeable)
        {
            gameObject.SetActive(false);

            distanceToPlayer = float.PositiveInfinity;
        }
        if (dialogueTrigger = GetComponentInParent<DialogueTrigger>())
        {
            dialogueTrigger.WaitTilTalk();
            controller.enabled = false;
        }
        PersistentManager.Instance.IsExamining = false;
        switch (this.name)
        {
            case "Báo":
                Instantiate(TriggerManager.Instance.nameTriggersPairs["Báo"]);
                break;

        }
    }
    public void ExamineWhileInspecting()
    {
        descriptions[languageIndex].SetActive(true);
        isExamined = true;
        //inventoryButton.SetActive(true);
        StaticCanvas.Instance.ButtonsStatus[objectOrder] = true;
        //StaticCanvas.Buttons[objectOrder].SetActive(true);
    }

    public void StopExamineWhileInspecting()
    {
        if (isDocument)
            doc[languageIndex].SetActive(false);
        descriptions[languageIndex].SetActive(false);
        isExamined = false;
        if (!isDocument)
        {
            isNearCam = false;
            //gameObject.SetActive(false);
            //isNearCam = false;
        }
        if (takeable)
        {
            gameObject.SetActive(false);

            distanceToPlayer = float.PositiveInfinity;
        }

        if (dialogueTrigger = GetComponentInParent<DialogueTrigger>())
        {
            dialogueTrigger.WaitTilTalk();
        }
    }
}
