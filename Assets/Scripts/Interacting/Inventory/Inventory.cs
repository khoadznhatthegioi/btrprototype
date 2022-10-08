using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [SerializeField] Image DisplayField;
    static Image displayField;
    private Button[] buttons;
    private Sprite[] sprites;
    Dictionary<Button, Sprite> buttonSpritePairs = new Dictionary<Button, Sprite>();
    [SerializeField] GameObject DocumentList;
    static GameObject documentList;
    //public static Sprite originalFieldSprite;
    [SerializeField] Sprite OriginalFieldSprite;
    static Sprite originalFieldSprite;

    public static string currentSpriteName;
    public static Button enableDocumentList;
    static bool selected;

    private void Start()
    {
        buttons = new Button[StaticCanvas.Instance.buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = StaticCanvas.Instance.buttons[i].GetComponent<Button>(); 
        }
        sprites = new Sprite[StaticCanvas.Instance.sprites.Length];
        for(int i = 0;i < sprites.Length;i++)
        {
            sprites[i] = StaticCanvas.Instance.sprites[i];
        }
        for (var i = 0; i < buttons.Length; i++)
        {
            buttonSpritePairs.Add(buttons[i], sprites[i]);
        }
        displayField = DisplayField;
        originalFieldSprite = OriginalFieldSprite;
        documentList = DocumentList;
    }

    public static void SetOriginal()
    {
        //Inventory instance = new Inventory();
        displayField.sprite = originalFieldSprite;
        currentSpriteName = null;
        selected = false;
        documentList.SetActive(false);
        if(enableDocumentList)
            enableDocumentList.gameObject.SetActive(true);
    }

    public void Button(Button btn)
    {
        displayField.sprite = buttonSpritePairs[btn];
        currentSpriteName = displayField.sprite.name;
        selected = true;
    }

    public void EnableDocumentList(Button btn)
    {
        documentList.SetActive(true);
        btn.gameObject.SetActive(false);
        enableDocumentList = btn;
    }

    private void Update()
    {
        if (selected)
        {
            if (Input.GetButtonDown("Use"))
            {
                Use();
            }
        }
    }

    public void Use()
    {
        //PersistentManager.Instance.IsInventoryOn = false;
        if(currentSpriteName == Keyhole.currentName)
        {
            switch(currentSpriteName)
            {
                case "UISprite":
                    LevelSystem.Instance.gameStates[0] = true;
                    StaticCanvas.Instance.ButtonsStatus[1] = false;
                    SavingLoading.Instance.Save();
                    break;
            }
        }
    }
}
