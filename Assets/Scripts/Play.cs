// Murat Sancak

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Play:MonoBehaviour
{
    [SerializeField]
    private Button // A: Advertisement, D: Down, U: Up.
        dDAB, dDDB, dDUB,
        d4AB, d4DB, d4UB,
        d6AB, d6DB, d6UB,
        d8AB, d8DB, d8UB,
        d10AB, d10DB, d10UB,
        d12AB, d12DB, d12UB,
        d20AB, d20DB, d20UB;

    [SerializeField]
    private GameObject
        A, // A: Advertisement.
        U; // U: Up.

    [SerializeField]
    private RectTransform
        sT, // sT: Score Text (TMP).
        uP; // uP: Upper Panel.

    private GameObject gO; // gO: Game Object.

    private int s = 0; // s: Score.

    //private readonly Vector2[]
    //    v2s0 = new Vector2[4], // v2s0: Vector2's 0.
    //    v2s1 = new Vector2[4]; // v2s1: Vector2's 1.

    private readonly WaitForSeconds wFS = new(.1f); // wFS: Wait For Seconds.

    private static GameObject dGO; // dGO: Dices Game Object.

    private readonly static GameObject[] // HP: High Poly, LP: Low Poly.
        dDHP = Menu.dDHP, dDLP = Menu.dDLP,
        d4HP = Menu.d4HP, d4LP = Menu.d4LP,
        d6HP = Menu.d6HP, d6LP = Menu.d6LP,
        d8HP = Menu.d8HP, d8LP = Menu.d8LP,
        d10HP = Menu.d10HP, d10LP = Menu.d10LP,
        d12HP = Menu.d12HP, d12LP = Menu.d12LP,
        d20HP = Menu.d20HP, d20LP = Menu.d20LP;

    private readonly static IList<GameObject>
        dDs = new List<GameObject>(),
        d4s = new List<GameObject>(),
        d6s = new List<GameObject>(),
        d8s = new List<GameObject>(),
        d10s = new List<GameObject>(),
        d12s = new List<GameObject>(),
        d20s = new List<GameObject>();

    private readonly static Vector3 v3 = 1.5f*Vector2.up; // v3: Vector3.

    private readonly static Vector3[]
        v3s = new Vector3[7] // v3s: Vector3's.
            {
                new(0, 1.64f, 0), // dD.
                new(8, 1.35f, 0), // d4.
                new(15.5f, 1.64f, 0), // d6.
                new(11.5f, 1.64f, 0), // d8.
                new(8, 1.64f, 2.5f), // d10.
                new(8, 1.64f, -2.5f), // d12.
                new(4, 1.64f, 0), // d20.
            };

    public static IList<GameObject> ds = new List<GameObject>(); // ds: Dices.

    // Murat Sancak

    private void Awake() => dGO=GameObject.Find("Dices Game Object");

    private void OnDestroy()
    {
        dDs.Clear();
        d4s.Clear();
        d6s.Clear();
        d8s.Clear();
        d10s.Clear();
        d12s.Clear();
        d20s.Clear();
        ds.Clear();
    }

    private void Start()
    {
        if(Preferences.Poly is 1)
        {
            Add(Create(dDHP[Preferences.DD],v3s[0]),dDs);
            Add(Create(d4HP[Preferences.D4],v3s[1]),d4s);
            Add(Create(d6HP[Preferences.D6],v3s[2]),d6s);
            Add(Create(d8HP[Preferences.D8],v3s[3]),d8s);
            Add(Create(d10HP[Preferences.D10],v3s[4]),d10s);
            Add(Create(d12HP[Preferences.D12],v3s[5]),d12s);
            Add(Create(d20HP[Preferences.D20],v3s[6]),d20s);
        }
        else
        {
            Add(Create(dDLP[Preferences.DD],v3s[0]),dDs);
            Add(Create(d4LP[Preferences.D4],v3s[1]),d4s);
            Add(Create(d6LP[Preferences.D6],v3s[2]),d6s);
            Add(Create(d8LP[Preferences.D8],v3s[3]),d8s);
            Add(Create(d12LP[Preferences.D12],v3s[5]),d12s);
            Add(Create(d20LP[Preferences.D20],v3s[6]),d20s);
            Add(Create(d10LP[Preferences.D10],v3s[4]),d10s);
        }

        Add(dDs.Last(),ds);
        Add(d4s.Last(),ds);
        Add(d6s.Last(),ds);
        Add(d8s.Last(),ds);
        Add(d10s.Last(),ds);
        Add(d12s.Last(),ds);
        Add(d20s.Last(),ds);

        StartCoroutine(Enumerator());
    }

    private void Update() => Button(IAP.HR(0),Monetization.iRL);

    // Murat Sancak

    private System.Collections.IEnumerator Enumerator()
    {
        while(true)
        {
            foreach(GameObject d in ds) // d: Dice.
                s+=d.GetComponent<Dice>().s;
            sT.GetComponent<TextMeshProUGUI>().text=s.ToString();

            if(!Monetization.IBL||IAP.HR(0))
                uP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,256);
            else
                uP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,346);

            if(s is 132)
            {
                Handheld.Vibrate();
                PG.Achievement(132);
            }
            PG.Achievement(ds.Count);
            PG.Leaderboard(s);

            s=0;

            yield return wFS;
        }
    }

    // Murat Sancak

    private void Button(bool a,bool i) // a: Active, i: Interactable.
    {
        A.SetActive(!a);
        U.SetActive(a);

        dDAB.interactable=
        d4AB.interactable=
        d6AB.interactable=
        d8AB.interactable=
        d10AB.interactable=
        d12AB.interactable=
        d20AB.interactable=
        i;

        dDDB.interactable=dDs.Count is not 0;
        d4DB.interactable=d4s.Count is not 0;
        d6DB.interactable=d6s.Count is not 0;
        d8DB.interactable=d8s.Count is not 0;
        d10DB.interactable=d10s.Count is not 0;
        d12DB.interactable=d12s.Count is not 0;
        d20DB.interactable=d20s.Count is not 0;

        dDUB.interactable=dDs.Count is not 2;
        d4UB.interactable=d4s.Count is not 2;
        d6UB.interactable=d6s.Count is not 2;
        d8UB.interactable=d8s.Count is not 2;
        d10UB.interactable=d10s.Count is not 2;
        d12UB.interactable=d12s.Count is not 2;
        d20UB.interactable=d20s.Count is not 2;
    }

    private void Remove(GameObject gO,IList<GameObject> gOs) // gO: Game Object, gOs: Game Objects.
    {
        gOs.Remove(gO);
        Destroy(gO);
    }

    private static GameObject Create(GameObject gO,Vector3 v3) => Instantiate(gO,v3,Quaternion.identity,dGO.transform); // gO: Game Object, v3: Vector3.

    private static void Add(GameObject gO,IList<GameObject> gOs) => gOs.Add(gO); // gO: Game Object, gOs: Game Objects.

    public void DDA() => Monetization.Rewarded(0); // A: Advertisement.
    public void DDD() // D: Down.
    {
        if(dDs.Count is 1)
        {
            gO=dDs[0];
            Remove(gO,dDs);
            Remove(gO,ds);
        }
        else // if(dDs.Count is 2)
        {
            gO=dDs[1];
            Remove(gO,dDs);
            Remove(gO,ds);
        }
    }
    public void DDU() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(dDHP[Preferences.DD],v3s[0]+dDs.Count*v3),dDs);
        else
            Add(Create(dDLP[Preferences.DD],v3s[0]+dDs.Count*v3),dDs);
        Add(dDs.Last(),ds);
    }

    public void D4A() => Monetization.Rewarded(4); // A: Advertisement.
    public void D4D() // D: Down.
    {
        if(d4s.Count is 1)
        {
            gO=d4s[0];
            Remove(gO,d4s);
            Remove(gO,ds);
        }
        else // if(d4s.Count is 2)
        {
            gO=d4s[1];
            Remove(gO,d4s);
            Remove(gO,ds);
        }
    }
    public void D4U() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(d4HP[Preferences.D4],v3s[1]+d4s.Count*v3),d4s);
        else
            Add(Create(d4LP[Preferences.D4],v3s[1]+d4s.Count*v3),d4s);
        Add(d4s.Last(),ds);
    }

    public void D6A() => Monetization.Rewarded(6); // A: Advertisement.
    public void D6D() // D: Down.
    {
        if(d6s.Count is 1)
        {
            gO=d6s[0];
            Remove(gO,d6s);
            Remove(gO,ds);
        }
        else // if(d6s.Count is 2)
        {
            gO=d6s[1];
            Remove(gO,d6s);
            Remove(gO,ds);
        }
    }
    public void D6U() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(d6HP[Preferences.D6],v3s[2]+d6s.Count*v3),d6s);
        else
            Add(Create(d6LP[Preferences.D6],v3s[2]+d6s.Count*v3),d6s);
        Add(d6s.Last(),ds);
    }

    public void D8A() => Monetization.Rewarded(8); // A: Advertisement.
    public void D8D() // D: Down.
    {
        if(d8s.Count is 1)
        {
            gO=d8s[0];
            Remove(gO,d8s);
            Remove(gO,ds);
        }
        else // if(d8s.Count is 2)
        {
            gO=d8s[1];
            Remove(gO,d8s);
            Remove(gO,ds);
        }
    }
    public void D8U() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(d8HP[Preferences.D8],v3s[3]+d8s.Count*v3),d8s);
        else
            Add(Create(d8LP[Preferences.D8],v3s[3]+d8s.Count*v3),d8s);
        Add(d8s.Last(),ds);
    }

    public void D10A() => Monetization.Rewarded(10); // A: Advertisement.
    public void D10D() // D: Down.
    {
        if(d10s.Count is 1)
        {
            gO=d10s[0];
            Remove(gO,d10s);
            Remove(gO,ds);
        }
        else // if(d10s.Count is 2)
        {
            gO=d10s[1];
            Remove(gO,d10s);
            Remove(gO,ds);
        }
    }
    public void D10U() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(d10HP[Preferences.D10],v3s[4]+d10s.Count*v3),d10s);
        else
            Add(Create(d10LP[Preferences.D10],v3s[4]+d10s.Count*v3),d10s);
        Add(d10s.Last(),ds);
    }

    public void D12A() => Monetization.Rewarded(12); // A: Advertisement.
    public void D12D() // D: Down.
    {
        if(d12s.Count is 1)
        {
            gO=d12s[0];
            Remove(gO,d12s);
            Remove(gO,ds);
        }
        else // if(d12s.Count is 2)
        {
            gO=d12s[1];
            Remove(gO,d12s);
            Remove(gO,ds);
        }
    }
    public void D12U() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(d12HP[Preferences.D12],v3s[5]+d12s.Count*v3),d12s);
        else
            Add(Create(d12LP[Preferences.D12],v3s[5]+d12s.Count*v3),d12s);
        Add(d12s.Last(),ds);
    }

    public void D20A() => Monetization.Rewarded(20); // A: Advertisement.
    public void D20D() // D: Down.
    {
        if(d20s.Count is 1)
        {
            gO=d20s[0];
            Remove(gO,d20s);
            Remove(gO,ds);
        }
        else // if(d20s.Count is 2)
        {
            gO=d20s[1];
            Remove(gO,d20s);
            Remove(gO,ds);
        }
    }
    public void D20U() // U: Up.
    {
        if(Preferences.Poly is 1)
            Add(Create(d20HP[Preferences.D20],v3s[6]+d20s.Count*v3),d20s);
        else
            Add(Create(d20LP[Preferences.D20],v3s[6]+d20s.Count*v3),d20s);
        Add(d20s.Last(),ds);
    }

    public void Load(int s) => Scene.Load(s); // s: Scene.

    public void Pause(bool p) => Time.timeScale=p ? 0 : Preferences.P; // p: Pause.

    public static void Reward(int b) // b: Button.
    {
        switch(b)
        {
            case 0:
                if(Preferences.Poly is 1)
                    Add(Create(dDHP[Preferences.DD],v3s[0]+dDs.Count*v3),dDs);
                else
                    Add(Create(dDLP[Preferences.DD],v3s[0]+dDs.Count*v3),dDs);
                Add(dDs.Last(),ds);
                break;
            case 4:
                if(Preferences.Poly is 1)
                    Add(Create(d4HP[Preferences.D4],v3s[1]+d4s.Count*v3),d4s);
                else
                    Add(Create(d4LP[Preferences.D4],v3s[1]+d4s.Count*v3),d4s);
                Add(d4s.Last(),ds);
                break;
            case 6:
                if(Preferences.Poly is 1)
                    Add(Create(d6HP[Preferences.D6],v3s[2]+d6s.Count*v3),d6s);
                else
                    Add(Create(d6LP[Preferences.D6],v3s[2]+d6s.Count*v3),d6s);
                Add(d6s.Last(),ds);
                break;
            case 8:
                if(Preferences.Poly is 1)
                    Add(Create(d8HP[Preferences.D8],v3s[3]+d8s.Count*v3),d8s);
                else
                    Add(Create(d8LP[Preferences.D8],v3s[3]+d8s.Count*v3),d8s);
                Add(d8s.Last(),ds);
                break;
            case 10:
                if(Preferences.Poly is 1)
                    Add(Create(d10HP[Preferences.D10],v3s[4]+d10s.Count*v3),d10s);
                else
                    Add(Create(d10LP[Preferences.D10],v3s[4]+d10s.Count*v3),d10s);
                Add(d10s.Last(),ds);
                break;
            case 12:
                if(Preferences.Poly is 1)
                    Add(Create(d12HP[Preferences.D12],v3s[5]+d12s.Count*v3),d12s);
                else
                    Add(Create(d12LP[Preferences.D12],v3s[5]+d12s.Count*v3),d12s);
                Add(d12s.Last(),ds);
                break;
            case 20:
                if(Preferences.Poly is 1)
                    Add(Create(d20HP[Preferences.D20],v3s[6]+d20s.Count*v3),d20s);
                else
                    Add(Create(d20LP[Preferences.D20],v3s[6]+d20s.Count*v3),d20s);
                Add(d20s.Last(),ds);
                break;
        }
    }
}

// Murat Sancak