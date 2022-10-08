using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    //public static LanguageManager Instance { get; private set; }
    public enum Language
    {
        Vietnamese,
        English,
        Francais,
        Nihon,
        Korean,
        TraditionalChinese,
        SimplifiedChinese
    }
    public static Language language;
}
