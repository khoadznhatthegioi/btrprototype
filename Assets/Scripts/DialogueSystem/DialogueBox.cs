using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public bool clickable;
    public Dialogue dialogue;
    Queue<string> sentences = new Queue<string>();
    public Image dialogueArea;
    public Text dialogueText;

    bool initialized;
    public int id;

    Interact interact;

    // Start is called before the first frame update
    void Start()
    {
        dialogueArea = GetComponent<Image>();
        interact = Camera.main.GetComponent<CameraFollow>().target.GetComponent<Interact>();
        dialogueArea.enabled = false;
        dialogueText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(interact.interactives[id].transform.position);
        

        transform.position = screenPos;
        if (!initialized)
        {
            dialogueArea.enabled = true;
            dialogueText.enabled = true;
            initialized = true;
        }
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
            DisplayNextDialogue();
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
        DisplayNextDialogue();
    }


    public void StartDialogue() 
    {
        count = 0;
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
        sentences.Clear();
        dialogue.durations.Clear();
        switch (LanguageManager.language)
        {
            default:
                foreach (string sentence in dialogue.sentencesVi)
                {
                    QueueSentences(sentence);
                }
                break;
            case LanguageManager.Language.English:
                foreach (string sentence in dialogue.sentencesEng)
                {
                    QueueSentences(sentence);
                }
                break;
            case LanguageManager.Language.Francais:
                foreach (string sentence in dialogue.sentencesFra)
                {
                    QueueSentences(sentence);
                }
                break;
            case LanguageManager.Language.Nihon:
                foreach (string sentence in dialogue.sentencesJa)
                {
                    QueueSentences(sentence);
                }
                break;
            case LanguageManager.Language.Korean:
                foreach (string sentence in dialogue.sentencesKo)
                {
                    QueueSentences(sentence);
                }
                break;
            case LanguageManager.Language.TraditionalChinese:
                foreach (string sentence in dialogue.sentencesTChi)
                {
                    QueueSentences(sentence);
                }
                break;
            case LanguageManager.Language.SimplifiedChinese:
                foreach (string sentence in dialogue.sentencesSChi)
                {
                    QueueSentences(sentence);
                }
                break;
        }

        DisplayNextDialogue();
    }
    int count = 0;
    void DisplayNextDialogue()
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
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        if (dialogue.useManualDurations)
            dialogue.duration = dialogue.durations[dialogue.sentenceCount];
        StartCoroutine(WaitTilDisplayNextSentenceOrTurnOff(dialogue.duration));
        dialogue.sentenceCount++;
        
        //if (dialogue.titles[dialogue.sentenceCount - 1] != null)
        //    DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount - 1];
    }

    void EndDialogue()
    {
        dialogue.sentenceCount = 0;
        this.gameObject.SetActive(false);
        dialogueArea = null;
    }

    void QueueSentences(string sentence)
    {
        if (dialogue.useManualDurations)
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
}