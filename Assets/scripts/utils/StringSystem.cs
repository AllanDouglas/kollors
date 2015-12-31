using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StringSystem
{

    public const SystemLanguage PADRAO = SystemLanguage.English; //linguagem padrao

    private static SystemLanguage _idioma = PADRAO;  // idioma selecionado
    public static SystemLanguage Idioma
    {
        set
        {
            if (idiomasSuportados.Contains(value))
                _idioma = value;
            else
                _idioma = PADRAO;
        }
    }

    private static ArrayList idiomasSuportados = new ArrayList {
        SystemLanguage.Portuguese,
        SystemLanguage.English
    };

    // texto do nivel
    private static Dictionary<SystemLanguage, string> _NIVEL = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Level"},
        { SystemLanguage.Portuguese, "Nível"}
    };
    public static string NIVEL
    {
        get
        {
            return _NIVEL[_idioma];
        }
    }
    
    // texto do combo
    private static Dictionary<SystemLanguage, string> _combo = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Combo"},
        { SystemLanguage.Portuguese, "Combo"}
    };
    public static string COMBO
    {
        get
        {
            return _combo[_idioma];
        }
    }

    // texto do record
    private static Dictionary<SystemLanguage, string> _record = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "HiScore"},
        { SystemLanguage.Portuguese, "Record"}
    };
    public static string RECORD
    {
        get
        {
            return _record[_idioma];
        }
    }


} 