using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public Text nameText;
    public Text dialogueText;
    public Image dialogueArea;
    public bool collectiveTalk;
    private Queue<string> sentences;
    public static bool hasUnpausibleDialoguePlaying;

    bool initializeDialogueArea;
    private Dialogue dialogue;
    GameObject player;
    Interactive interactive;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, float interval, GameObject player,Interactive interactive)
    {

        Debug.Log("Superpupup");
        this.interactive = interactive;
        count = 0;
        this.player = player;
        initializeDialogueArea = true;
        setOnce = false;
        sentences.Clear();
        dialogue.durations.Clear();
        dialogue.titles.Clear();
        if (!dialogue.useImagesReferenceSizesInsteadOfVector2)
        {
            dialogueArea.rectTransform.sizeDelta = new Vector2(dialogue.initialBgSize.x, dialogue.initialBgSize.y);
            if(!dialogue.isConversation && dialogue.isTextDependsOnBackgroundSize)
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.tsizeOverBgSize*dialogue.initialBgSize.x, dialogue.tsizeOverBgSize * dialogue.initialBgSize.y);
            }
            else
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.InitialTextSize.x, dialogue.InitialTextSize.y);
            }
                
        }
        else
        {
            dialogueArea.rectTransform.sizeDelta = new Vector2(dialogue.InitialBgImageRef.rectTransform.sizeDelta.x, dialogue.InitialBgImageRef.rectTransform.sizeDelta.y);
            if (!dialogue.isConversation && dialogue.isTextDependsOnBackgroundSize)
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.tsizeOverBgSize * dialogue.InitialBgImageRef.rectTransform.sizeDelta.x, dialogue.tsizeOverBgSize * dialogue.InitialBgImageRef.rectTransform.sizeDelta.y);
            }
            else
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.InitialTextImageRef.rectTransform.sizeDelta.x, dialogue.InitialTextImageRef.rectTransform.sizeDelta.y);
            }
                
        }
        
        this.dialogue = dialogue;
        if (dialogue.isConversation)
        {
            hasUnpausibleDialoguePlaying = true;
            if (!dialogue.useImagesReferenceSizesInsteadOfVector2)
            {
                dialogueArea.rectTransform.localPosition = dialogue.positionBackground;
                dialogueText.rectTransform.localPosition = dialogue.positionText;
                nameText.rectTransform.localPosition = dialogue.positionTitle;
                nameText.rectTransform.sizeDelta = new Vector2(dialogue.initialTitleSize.x, dialogue.initialTitleSize.y);
            }
            else
            {
                dialogueArea.rectTransform.localPosition = dialogue.InitialBgImageRef.rectTransform.localPosition;
                dialogueText.rectTransform.localPosition = dialogue.InitialTextImageRef.rectTransform.localPosition;
                nameText.rectTransform.localPosition = dialogue.IninitialTitleImageRef.rectTransform.localPosition;
                nameText.rectTransform.sizeDelta = new Vector2(dialogue.IninitialTitleImageRef.rectTransform.sizeDelta.x, dialogue.IninitialTitleImageRef.rectTransform.sizeDelta.y);
            }

        }
        Debug.Log(LanguageManager.language);
        switch (LanguageManager.language)
        {
            default:
                foreach (string sentence in dialogue.sentencesVi)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.English:
                foreach (string sentence in dialogue.sentencesEng)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.Francais:
                foreach (string sentence in dialogue.sentencesFra)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.Nihon:
                foreach (string sentence in dialogue.sentencesJa)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.Korean:
                foreach (string sentence in dialogue.sentencesKo)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.TraditionalChinese:
                foreach (string sentence in dialogue.sentencesTChi)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.SimplifiedChinese:
                foreach (string sentence in dialogue.sentencesSChi)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
        }
        DisplayNextSentence(dialogue, interval, player);
        
    }

    public void StartDialogue(Dialogue dialogue, GameObject player, Interactive interactive)
    {
        this.interactive = interactive;
        count = 0;
        this.player = player;
        initializeDialogueArea = true;
        setOnce = false;
        sentences.Clear();
        dialogue.durations.Clear();
        dialogue.titles.Clear();
        this.dialogue = dialogue;
        if (!dialogue.useImagesReferenceSizesInsteadOfVector2)
        {
            dialogueArea.rectTransform.sizeDelta = new Vector2(dialogue.initialBgSize.x, dialogue.initialBgSize.y);
            if (!dialogue.isConversation && dialogue.isTextDependsOnBackgroundSize)
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.tsizeOverBgSize * dialogue.initialBgSize.x, dialogue.tsizeOverBgSize * dialogue.initialBgSize.y);
            }
            else
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.InitialTextSize.x, dialogue.InitialTextSize.y);
            }
        }
        else
        {
            dialogueArea.rectTransform.sizeDelta = new Vector2(dialogue.InitialBgImageRef.rectTransform.sizeDelta.x, dialogue.InitialBgImageRef.rectTransform.sizeDelta.y);
            if (!dialogue.isConversation && dialogue.isTextDependsOnBackgroundSize)
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.tsizeOverBgSize * dialogue.InitialBgImageRef.rectTransform.sizeDelta.x, dialogue.tsizeOverBgSize * dialogue.InitialBgImageRef.rectTransform.sizeDelta.y);
            }
            else
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.InitialTextImageRef.rectTransform.sizeDelta.x, dialogue.InitialTextImageRef.rectTransform.sizeDelta.y);
            }
        }
        if (dialogue.isConversation)
        {
            hasUnpausibleDialoguePlaying = true;
            if (!dialogue.useImagesReferenceSizesInsteadOfVector2)
            {
                dialogueArea.rectTransform.localPosition = dialogue.positionBackground;
                dialogueText.rectTransform.localPosition = dialogue.positionText;
                nameText.rectTransform.localPosition = dialogue.positionTitle;
                nameText.rectTransform.sizeDelta = new Vector2(dialogue.initialTitleSize.x, dialogue.initialTitleSize.y);
            }
            else
            {
                dialogueArea.rectTransform.localPosition = dialogue.InitialBgImageRef.rectTransform.localPosition;
                dialogueText.rectTransform.localPosition = dialogue.InitialTextImageRef.rectTransform.localPosition;
                nameText.rectTransform.localPosition = dialogue.IninitialTitleImageRef.rectTransform.localPosition;
                nameText.rectTransform.sizeDelta = new Vector2(dialogue.IninitialTitleImageRef.rectTransform.sizeDelta.x, dialogue.IninitialTitleImageRef.rectTransform.sizeDelta.y);
            }

        }
        switch (LanguageManager.language)
        {
            default:
                //nameText.text = dialogue.titleVi;
                foreach (string sentence in dialogue.sentencesVi)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.English:
                foreach (string sentence in dialogue.sentencesEng)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.Francais:
                foreach (string sentence in dialogue.sentencesFra)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.Nihon:
                foreach (string sentence in dialogue.sentencesJa)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.Korean:
                foreach (string sentence in dialogue.sentencesKo)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.TraditionalChinese:
                foreach (string sentence in dialogue.sentencesTChi)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
            case LanguageManager.Language.SimplifiedChinese:
                foreach (string sentence in dialogue.sentencesSChi)
                {
                    QueueSentences(dialogue, sentence);
                }
                break;
        }
        DisplayNextSentence(dialogue, player);
    }
    int count;
    public void DisplayNextSentence(Dialogue dialogue,GameObject player)
    {
        if (dialogue.intervalsSpriteChange.Count > 0 && count < dialogue.intervalsSpriteChange.Count)
        {
            dialogue.intervalSpriteChange = dialogue.intervalsSpriteChange[count];
            if (!dialogue.useImagesReferenceSizesInsteadOfVector2)
            {
                dialogue.nextBgSize = dialogue.followingSizes[count];
                dialogue.nextTextSize = dialogue.followingTextSizes[count];
            }
            else
            {
                dialogue.nextImageRefBgSize = dialogue.followingImagesRef[count];
                dialogue.nextImageRefTextSize = dialogue.followingImagesTextRef[count];
            }
            count++;
        }
        if (sentences.Count == 0)
        {
            EndDialogue(dialogue, player);
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        if (dialogue.useManualDurations)
        {
            Debug.Log(dialogue.sentenceCount);
            dialogue.duration = dialogue.durations[dialogue.sentenceCount];
        }
            
        StartCoroutine(WaitTilDisplayNextSentenceOrTurnOff(dialogue.duration));
        //string forcedUninteractiveString = Convert.ToString(forcedUninteractive);
    }
    IEnumerator WaitTilDisplayNextSentenceOrTurnOff(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (dialogue.changeBgSpriteEverySentence && count <= dialogue.intervalsSpriteChange.Count)
        {
            dialogueArea.enabled = false;
            dialogueText.enabled = false;
            StartCoroutine(WaitTilDisplayNextSentence());
            yield return null;
        }
        else
        {
            DisplayNextSentence(dialogue, player);
        }
        
    }

    IEnumerator WaitTilDisplayNextSentence()
    {
        yield return new WaitForSeconds(dialogue.intervalSpriteChange);
        if (!dialogue.useImagesReferenceSizesInsteadOfVector2)
        {
            dialogueArea.rectTransform.sizeDelta = new Vector2(dialogue.nextBgSize.x, dialogue.nextBgSize.y);
            if (!dialogue.isConversation && dialogue.isTextDependsOnBackgroundSize)
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.tsizeOverBgSize * dialogue.nextBgSize.x, dialogue.tsizeOverBgSize * dialogue.nextBgSize.y);
            }
            else
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.nextTextSize.x, dialogue.nextTextSize.y);
            }

        }
        else
        {
            dialogueArea.rectTransform.sizeDelta = new Vector2(dialogue.nextImageRefBgSize.rectTransform.sizeDelta.x, dialogue.nextImageRefBgSize.rectTransform.sizeDelta.y);
            if (!dialogue.isConversation && dialogue.isTextDependsOnBackgroundSize)
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.tsizeOverBgSize * dialogue.nextImageRefBgSize.rectTransform.sizeDelta.x, dialogue.tsizeOverBgSize * dialogue.nextImageRefBgSize.rectTransform.sizeDelta.y);
            }
            else
            {
                dialogueText.rectTransform.sizeDelta = new Vector2(dialogue.nextImageRefTextSize.rectTransform.sizeDelta.x, dialogue.nextImageRefTextSize.rectTransform.sizeDelta.y);
            }
        }
        
        dialogueArea.enabled = true;
        dialogueText.enabled = true;
        DisplayNextSentence(dialogue, player);
    }

    public void DisplayNextSentence(Dialogue dialogue, float interval, GameObject player)
    {
        a = false;
        b = false;
        if(sentences.Count == 0)
        {
            EndDialogue(dialogue, player);
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, interval, dialogue, dialogue.intervalPunc));
        //StartCoroutine(WaitTilSkip(dialogue));
    }

    bool a = false;
    bool b = false;
    //IEnumerator WaitTilSkip(Dialogue dialogue)
    //{
    //    yield return new WaitForSeconds(dialogue.duration/2);
    //    a = true;
    //}

    IEnumerator TypeSentence(string sentence, float interval, Dialogue dialogue, float intervalPunc) 
    {
        var charArray = sentence.ToCharArray();
        dialogueText.text = "";
        for (var i = 0; i<charArray.Length; i++)
        {
            dialogueText.text += charArray[i];
            if (!dialogue.useManualDurations)
            {
                if (i == charArray.Length-1)
                {

                    StartCoroutine(wait());

                    IEnumerator wait()
                    {
                        yield return new WaitForSeconds(0.24f);
                        dialogue.ableToGo = true;
                        dialogue.ableToNavigate = true;
                        yield return null;
                    }
                }
                if (i >= ((charArray.Length-1)/1.98f))
                {
                    a = true;
                }    
            }

            if(b)
            {
                interval = 0.01f;
                intervalPunc = 0.01f;
            }

            if (charArray[i] == '.' || charArray[i] == ',' || charArray[i] == '?' || charArray[i] == '!')
            {
                yield return new WaitForSeconds(intervalPunc);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    void EndDialogue(Dialogue dialogue, GameObject player)
    {
        Collective col;
        Look look;
        dialogue.sentenceCount = 0;
        dialogueArea.gameObject.SetActive(false);
        player.GetComponent<Controller>().enabled = true;
        if (dialogue.replayable)
        {
            interactive.forcedUninteractive = false;
        }

        dialogue.ableToGo = false;
        if (!dialogue.replayable)
        {
            if (look = dialogue.trigger.GetComponent<Look>())
            {
                look.uninractive = true;
                look.distanceToPlayer = float.PositiveInfinity;
            }
        }

        if (col = dialogue.trigger.GetComponentInChildren<Collective>())
        {
            player.GetComponent<Interact>().interactives.Add(col);
            player.GetComponent<Controller>().enabled = false;
            StartCoroutine(EndToCollectGap(col, player));
        }
        hasUnpausibleDialoguePlaying = false;
    }

    IEnumerator EndToCollectGap(Collective col, GameObject player)
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<Interact>().InteractWithObject(col);
        collectiveTalk = true;
    }

    void QueueSentences(Dialogue dialogue, string sentence)
    {
        Debug.Log("hehe");
        if(dialogue.useManualDurations)
            dialogue.durations.Add(float.Parse(sentence[(sentence.IndexOf(";") + 1)..]));
        if (sentence.Contains(":"))
        {
            string stemp = sentence.Remove(sentence.IndexOf(";"));
            dialogue.titles.Add(stemp[(stemp.IndexOf(":") + 1)..]);
            sentences.Enqueue(sentence.Remove(sentence.IndexOf(":")));
        }
        else
        {
            dialogue.titles.Add(null);
            sentences.Enqueue(sentence.Remove(sentence.IndexOf(";")));
        }
    }
    bool setOnce;
    private void Update()
    {
        if (initializeDialogueArea)
        {
            dialogueArea.enabled = false;
            dialogueArea.gameObject.SetActive(true);
            initializeDialogueArea = false;
        }
        if(!initializeDialogueArea)
        {
            if (!setOnce)
            {
                dialogueArea.enabled = true;
                setOnce = true;
            }
           
        }
        if (dialogue != null)
        {
            if (!dialogue.isConversation)
            {
                var screenPos = Camera.main.WorldToScreenPoint(dialogue.objectFollow.transform.position);
                dialogueArea.transform.position = screenPos;
            }
        }
        if (Input.GetButtonDown("Interact"))
        {
            if (a)
                b = true;
        }
    }
}
