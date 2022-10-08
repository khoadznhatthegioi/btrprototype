using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Dialogue
{
    public bool isConversation;
    [ConditionalHide("isConversation", true, false)]
    [Range(0, 0.1f)]
    public float interval = 0.0226f;
    [ConditionalHide("isConversation", true, false)]
    public float intervalPunc = 0.15f;
    public bool replayable;
    public bool autonext;
    public bool changeBgSpriteEverySentence;
    public bool useImagesReferenceSizesInsteadOfVector2;
    public bool isTextDependsOnBackgroundSize; // Just for non-Conversations
    [ConditionalHide("isTextDependsOnBackgroundSize", true, false)]
    public float tsizeOverBgSize;
    public bool useManualDurations;
    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, true)]
    public Vector2 initialBgSize;

    //[ConditionalHide("useImagesReferenceSizesInsteadOfVector2", false, true)]
    public List<Vector2> followingSizes = new List<Vector2>();

    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", ConditionalSourceField2 = "isTextDependsOnBackgroundSize", HideInInspector = true, InverseCondition1 = true, InverseCondition2 = true, UseOrLogic = false)]
    public Vector2 InitialTextSize;

    //[ConditionalHide("useImagesReferenceSizesInsteadOfVector2", false, true)]
    public List<Vector2> followingTextSizes = new();

    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, false)]
    public Image InitialBgImageRef;

    //[ConditionalHide("useImagesReferenceSizesInsteadOfVector2", false, false)]
    public List<Image> followingImagesRef = new List<Image>();

    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", ConditionalSourceField2 = "isTextDependsOnBackgroundSize", HideInInspector = true, InverseCondition1 = false, InverseCondition2 = true, UseOrLogic = false)]
    public Image InitialTextImageRef;

    //[ConditionalHide("useImagesReferenceSizesInsteadOfVector2", false, false)]
    public List<Image> followingImagesTextRef = new();

    [Header("Only For Conversations, And Only Vector2, Else Use Image Ref")]
    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, true)]
    public Vector2 positionBackground;

    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, true)]
    public Vector2 positionText;

    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, true)]
    public Vector2 positionTitle;


    [Header("Only For Conversations, Title")]
    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, false)]
    public Image IninitialTitleImageRef;

    [ConditionalHide("useImagesReferenceSizesInsteadOfVector2", true, true)]
    public Vector2 initialTitleSize;
    
    [Header("Others")]
    public Transform objectFollow;
    [HideInInspector] public Image nextImageRefBgSize;
    [HideInInspector] public Image nextImageRefTextSize;
    [HideInInspector] public Vector2 nextBgSize;
    [HideInInspector] public Vector2 nextTextSize;
    [HideInInspector] public float intervalSpriteChange;
    [HideInInspector] public bool ableToNavigate;
    [HideInInspector] public bool ableToGo;
    [HideInInspector] public int sentenceCount;
    [HideInInspector] public float duration; 

    public GameObject trigger;

    [HideInInspector] public List<float> durations = new List<float>();
    public List<float> intervalsSpriteChange = new List<float>();
    [HideInInspector] public List<string> titles = new List<string>();
    [TextArea(3, 10)]
    public string[] sentencesVi;
    [TextArea(3, 10)]
    public string[] sentencesEng;
    [TextArea(3, 10)]
    public string[] sentencesFra;
    [TextArea(3, 10)]
    public string[] sentencesJa;
    [TextArea(3, 10)]
    public string[] sentencesKo;
    [TextArea(3, 10)]
    public string[] sentencesTChi;
    [TextArea(3, 10)]
    public string[] sentencesSChi;
}