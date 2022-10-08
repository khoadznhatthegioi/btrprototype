using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : Interactive
{

    public Dialogue dialogue;

    private void Start()
    {
        OnStart();
        dialogue.trigger = this.gameObject;
    }
    private void Update()
    {
        OnUpdate();
        if (dialogue.ableToGo && dialogue.ableToNavigate)
        {
            if (Input.GetButtonDown("Interact") && !dialogue.autonext)
            {
                if (dialogue.isConversation)
                {
                    DialogueManager.Instance.DisplayNextSentence(dialogue, dialogue.interval, player);
                    if (dialogue.sentenceCount == 0)
                    {
                        return;
                    }
                    dialogue.sentenceCount++;
                    if (dialogue.useManualDurations)
                        dialogue.duration = dialogue.durations[dialogue.sentenceCount-1];
                    if (dialogue.titles[dialogue.sentenceCount - 1] != null)
                        DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount-1];
                    dialogue.ableToNavigate = false;
                    if(dialogue.useManualDurations)
                        StartCoroutine(wait());
                }

                else
                {
                    DialogueManager.Instance.DisplayNextSentence(dialogue, player);
                    if (dialogue.sentenceCount == 0)
                    {
                        return;
                    }
                    dialogue.sentenceCount++;
                    if (dialogue.useManualDurations)
                        dialogue.duration = dialogue.durations[dialogue.sentenceCount-1];
                    if (dialogue.titles[dialogue.sentenceCount - 1] != null)
                        DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount - 1];
                    dialogue.ableToNavigate = false;
                    if (dialogue.useManualDurations)
                        StartCoroutine(wait());
                }
            }

            if (dialogue.autonext && !dialogue.changeBgSpriteEverySentence)
            {
                if (dialogue.isConversation)
                {
                    DialogueManager.Instance.DisplayNextSentence(dialogue, dialogue.interval, player);
                    if (dialogue.sentenceCount == 0)
                    {
                        return;
                    }
                    dialogue.sentenceCount++;
                    if (dialogue.useManualDurations)
                        dialogue.duration = dialogue.durations[dialogue.sentenceCount - 1];
                    if (dialogue.titles[dialogue.sentenceCount - 1] != null)
                        DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount - 1];
                    dialogue.ableToNavigate = false;
                    if (dialogue.useManualDurations)
                        StartCoroutine(wait());
                }

                else
                {
                    DialogueManager.Instance.DisplayNextSentence(dialogue, player);
                    if (dialogue.sentenceCount == 0)
                    {
                        return;
                    }
                    dialogue.sentenceCount++;
                    if (dialogue.useManualDurations)
                        dialogue.duration = dialogue.durations[dialogue.sentenceCount - 1];
                    if (dialogue.titles[dialogue.sentenceCount - 1] != null)
                        DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount - 1];
                    dialogue.ableToNavigate = false;
                    if (dialogue.useManualDurations)
                        StartCoroutine(wait());
                }
            }
        }
    }
    public void Looked()
    {
        forcedUninteractive = true;
        player.GetComponent<Controller>().enabled = false;
        if (dialogue.isConversation)
        {
            DialogueManager.Instance.StartDialogue(dialogue, dialogue.interval, player, this);
            dialogue.sentenceCount++;
            if(dialogue.useManualDurations)
                dialogue.duration = dialogue.durations[dialogue.sentenceCount-1];
            Debug.Log(LanguageManager.language);
            if (dialogue.titles[dialogue.sentenceCount - 1] != null)
                DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount - 1];
        }

        else
        {
            DialogueManager.Instance.StartDialogue(dialogue, player, this);
            dialogue.sentenceCount++;
            if (dialogue.useManualDurations)
                dialogue.duration = dialogue.durations[dialogue.sentenceCount -1];
            if(dialogue.titles[dialogue.sentenceCount -1] != null)
                DialogueManager.Instance.nameText.text = dialogue.titles[dialogue.sentenceCount - 1];
        }
        if (dialogue.useManualDurations)
        {
            StartCoroutine(start());
            StartCoroutine(wait());
        }
            
    }
    IEnumerator start()
    {
        yield return new WaitForSeconds(dialogue.duration);
        dialogue.ableToGo = true;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(dialogue.duration);
        dialogue.ableToNavigate = true;
    }
}

