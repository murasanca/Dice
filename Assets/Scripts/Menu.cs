// Murat Sancak

using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] rTs = new RectTransform[6]; // rTs: Rect Transforms.

    private int f; // f: For.

    private readonly Vector2[]
        v2s0 = new Vector2[6], // v2s0: Vector2's 0.
        v2s1 = new Vector2[6]; // v2s1: Vector2's 1.

    public static Button
        g, // g: Goblet.
        s; // s: Scroll.

    public static GameObject[] // HP: High Poly, LP: Low Poly.
        dDHP = new GameObject[23], dDLP = new GameObject[23],
        d4HP = new GameObject[23], d4LP = new GameObject[23],
        d6HP = new GameObject[23], d6LP = new GameObject[23],
        d8HP = new GameObject[23], d8LP = new GameObject[23],
        d10HP = new GameObject[23], d10LP = new GameObject[23],
        d12HP = new GameObject[23], d12LP = new GameObject[23],
        d20HP = new GameObject[23], d20LP = new GameObject[23];

    public readonly static Vector2 b = 90 * Vector2.up; // b: Banner.

    public readonly static WaitForSeconds wFS = new(1); // wFS: Wait For Seconds.

    // Murat Sancak

    private Vector2[] V2s => B ? v2s0 : v2s1; // V2s: Vector2's.

    private static bool B => !Monetization.IBL || IAP.HR(0); // B: Banner.

    public static Vector3[] V3s => B ? // V3s: Vector3's.
        new Vector3[7]
        {
            new(2, 1.64f, 0), // dD
            new(9.5f, 1.35f, 0), // d4
            new(16.5f, 1.64f, 0), // d6
            new(12.5f, 1.64f, 0), // d8
            new(9.5f, 1.64f, 2.5f), // d10
            new(9.5f, 1.64f, -2.5f), // d12
            new(5.5f, 1.64f, 0) // d20
        }
        :
        new Vector3[7]
        {
            new(1.5f, 1.64f, 0), // dD
            new(9, 1.35f, 0), // d4
            new(16, 1.64f, 0), // d6
            new(12, 1.64f, 0), // d8
            new(9, 1.64f, 2.5f), // d10
            new(9, 1.64f, -2.5f), // d12
            new(5, 1.64f, 0), // d20
        };

    // Murat Sancak

    private void Awake()
    {
        for (f = 0; f < rTs.Length; f++)
        {
            v2s0[f] = b + rTs[f].anchoredPosition;
            v2s1[f] = rTs[f].anchoredPosition;
        }

        g = GameObject.Find("Goblet Button").GetComponent<Button>();
        s = GameObject.Find("Scroll Button").GetComponent<Button>();

        if (dDHP is not null) for (f = 0; f < dDHP.Length; f++) dDHP[f] = Resources.Load<GameObject>(String.Concat("DD High Poly ", f));
        if (dDLP is not null) for (f = 0; f < dDLP.Length; f++) dDLP[f] = Resources.Load<GameObject>(String.Concat("DD Low Poly ", f));

        if (d4HP is not null) for (f = 0; f < d4HP.Length; f++) d4HP[f] = Resources.Load<GameObject>(String.Concat("D4 High Poly ", f));
        if (d4LP is not null) for (f = 0; f < d4LP.Length; f++) d4LP[f] = Resources.Load<GameObject>(String.Concat("D4 Low Poly ", f));

        if (d6HP is not null) for (f = 0; f < d6HP.Length; f++) d6HP[f] = Resources.Load<GameObject>(String.Concat("D6 High Poly ", f));
        if (d6LP is not null) for (f = 0; f < d6LP.Length; f++) d6LP[f] = Resources.Load<GameObject>(String.Concat("D6 Low Poly ", f));

        if (d8HP is not null) for (f = 0; f < d8HP.Length; f++) d8HP[f] = Resources.Load<GameObject>(String.Concat("D8 High Poly ", f));
        if (d8LP is not null) for (f = 0; f < d8LP.Length; f++) d8LP[f] = Resources.Load<GameObject>(String.Concat("D8 Low Poly ", f));

        if (d10HP is not null) for (f = 0; f < d10HP.Length; f++) d10HP[f] = Resources.Load<GameObject>(String.Concat("D10 High Poly ", f));
        if (d10LP is not null) for (f = 0; f < d10LP.Length; f++) d10LP[f] = Resources.Load<GameObject>(String.Concat("D10 Low Poly ", f));

        if (d12HP is not null) for (f = 0; f < d12HP.Length; f++) d12HP[f] = Resources.Load<GameObject>(String.Concat("D12 High Poly ", f));
        if (d12LP is not null) for (f = 0; f < d12LP.Length; f++) d12LP[f] = Resources.Load<GameObject>(String.Concat("D12 Low Poly ", f));

        if (d20HP is not null) for (f = 0; f < d20HP.Length; f++) d20HP[f] = Resources.Load<GameObject>(String.Concat("D20 High Poly ", f));
        if (d20LP is not null) for (f = 0; f < d20LP.Length; f++) d20LP[f] = Resources.Load<GameObject>(String.Concat("D20 Low Poly ", f));
    }

    private void Start() => StartCoroutine(Enumerator());

    // Murat Sancak

    private System.Collections.IEnumerator Enumerator()
    {
        while(true)
        {
            for (f = 0; f < rTs.Length; f++)
                rTs[f].anchoredPosition = V2s[f];

            yield return wFS;
        }
    }

    // Murat Sancak

    public void Bag()
    {
        murasanca.Bag.s = -1;
        Scene.Load(1);
    }

    public void Goblet() => PG.Leaderboards();

    public void Inventory()
    {
        murasanca.Inventory.s = -1;
        Scene.Load(3);
    }

    public void Load(int s) => Scene.Load(s); // s: Scene.

    public void Scroll() => PG.Achievements();

    public void Star() => Application.OpenURL("market://details?id=com.murasanca.Dice");
}

// Murat Sancak