// Murat Sancak

using murasanca;
using UnityEngine;

public class Preferences:PlayerPrefs
{
    private readonly static float
        p, // p: Pitch.
        v; // v: Volume.

    private readonly static int dD, d4, d6, d8, d10, d12, d20, poly;

    // Murat Sancak

    public static float P // P: Pitch.
    {
        get
        {
            if(!HasKey("p"))
                P=1;
            return GetFloat("p",p);
        }
        set
        {
            SetFloat("p",value);
            Save();

            Time.timeScale=Mathf.Abs(P);
        }
    }

    public static float V // V: Volume.
    {
        get
        {
            if(!HasKey("v"))
                V=.64f;
            return GetFloat("v",v);
        }
        set
        {
            SetFloat("v",value);
            Save();
        }
    }

    public static int DD
    {
        get
        {
            if(!HasKey("dD")||!IAP.HR(dD)&&dD is not 0)
                DD=0;
            return GetInt("dD",dD);
        }
        set
        {
            SetInt("dD",value);
            Save();
        }
    }
    public static int D4
    {
        get
        {
            if(!HasKey("d4")||!IAP.HR(d4)&&d4 is not 0)
                D4=0;
            return GetInt("d4",d4);
        }
        set
        {
            SetInt("d4",value);
            Save();
        }
    }
    public static int D6
    {
        get
        {
            if(!HasKey("d6")||!IAP.HR(d6)&&d6 is not 0)
                D6=0;
            return GetInt("d6",d6);
        }
        set
        {
            SetInt("d6",value);
            Save();
        }
    }
    public static int D8
    {
        get
        {
            if(!HasKey("d8")||!IAP.HR(d8)&&d8 is not 0)
                D8=0;
            return GetInt("d8",d8);
        }
        set
        {
            SetInt("d8",value);
            Save();
        }
    }
    public static int D10
    {
        get
        {
            if(!HasKey("d10")||!IAP.HR(d10)&&d10 is not 0)
                D10=0;
            return GetInt("d10",d10);
        }
        set
        {
            SetInt("d10",value);
            Save();
        }
    }
    public static int D12
    {
        get
        {
            if(!HasKey("d12")||!IAP.HR(d12)&&d12 is not 0)
                D12=0;
            return GetInt("d12",d12);
        }
        set
        {
            SetInt("d12",value);
            Save();
        }
    }
    public static int D20
    {
        get
        {
            if(!HasKey("d20")||!IAP.HR(d20)&&d20 is not 0)
                D20=0;
            return GetInt("d20",d20);
        }
        set
        {
            SetInt("d20",value);
            Save();
        }
    }

    public static int Poly
    {
        get
        {
            if(!HasKey("poly"))
                Poly=1;
            return GetInt("poly",poly);
        }
        set
        {
            SetInt("poly",value);
            Save();
        }
    }
}

// Murat Sancak