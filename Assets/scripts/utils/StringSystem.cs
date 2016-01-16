using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StringSystem
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
    private static Dictionary<SystemLanguage, string> _ACERTOS = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Hits"},
        { SystemLanguage.Portuguese, "Acertos"}
    };
    public static string ACERTOS
    {
        get
        {
            return _ACERTOS[_idioma];
        }
    }

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
    private static Dictionary<SystemLanguage, string> _sair = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Quit"},
        { SystemLanguage.Portuguese, "Sair"}
    };
    public static string QUIT
    {
        get
        {
            return _sair[_idioma];
        }
    }

    // texto do record
    private static Dictionary<SystemLanguage, string> _record = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "HiScore"},
        { SystemLanguage.Portuguese, "Recorde"}
    };
    public static string RECORD
    {
        get
        {
            return _record[_idioma];
        }
    }

    // texto do start
    private static Dictionary<SystemLanguage, string> _start = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Start"},
        { SystemLanguage.Portuguese, "Iniciar"}
    };
    public static string START
    {
        get
        {
            return _start[_idioma];
        }
    }

    // texto de pontos
    private static Dictionary<SystemLanguage, string> _pontos = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Score"},
        { SystemLanguage.Portuguese, "Pontos"}
    };
    public static string PONTOS
    {
        get
        {
            return _pontos[_idioma];
        }
    }

    // texto da loja
    private static Dictionary<SystemLanguage, string> _loja = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Shop"},
        { SystemLanguage.Portuguese, "Loja"}
    };
    public static string SHOP
    {
        get
        {
            return _loja[_idioma];
        }
    }

    // texto do ranking
    private static Dictionary<SystemLanguage, string> _ranking = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Ranking"},
        { SystemLanguage.Portuguese, "Ranking"}
    };
    public static string RANKING
    {
        get
        {
            return _ranking[_idioma];
        }
    }

    // texto da dica de cor
    private static Dictionary<SystemLanguage, string> _dica_cor = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "Filled shapes , tap on same color."},
        { SystemLanguage.Portuguese, "Formas preenchidas, toque na cor igual."}
    };
    public static string DICA_COR
    {
        get
        {
            return _dica_cor[_idioma];
        }
    }

    // texto da dica de FORMA
    private static Dictionary<SystemLanguage, string> _dica_forma = new Dictionary<SystemLanguage, string>() {
        { SystemLanguage.English, "unfilled , tap on same form"},
        { SystemLanguage.Portuguese, "Sem preenchimento, toque na forma igual."}
    };
    public static string DICA_FORMA
    {
        get
        {
            return _dica_forma[_idioma];
        }
    }


}