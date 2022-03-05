// Murat Sancak

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace murasanca
{
    public class Play : MonoBehaviour
    {
        [SerializeField]
        private GameObject dDD, d4D, d6D, d8D, d10D, d12D, d20D; // D: Down.

        [SerializeField]
        private RectTransform
            sT, // sT: Score Text (TMP).
            uP; // uP: Upper Panel.

        [SerializeField]
        private RectTransform[] rTs = new RectTransform[4]; // rTs: Rect Transforms.

        private GameObject gO; // gO: Game Object.

        private int s = 0; // s: Score.

        private readonly Vector2 b = Menu.b; // b: Banner.

        private readonly Vector2[]
            v2s0 = new Vector2[4], // v2s0: Vector2's 0.
            v2s1 = new Vector2[4]; // v2s1: Vector2's 1.

        private readonly WaitForSeconds wFS = new(.1f); // wFS: Wait For Seconds.

        private static GameObject // A: Advertisement, U: Up.
            dDA, dDU,
            d4A, d4U,
            d6A, d6U,
            d8A, d8U,
            d10A, d10U,
            d12A, d12U,
            d20A, d20U,
            dGO; // dGO: Dices Game Object.

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

        private readonly static Vector3 v3 = 1.5f * Vector2.up; // v3: Vector3.

        private readonly static Vector3[]
            v3s = new Vector3[7] // v3s: Vector3's.
            {
                new(0, 1.64f, 0), // dD
                new(8, 1.35f, 0), // d4
                new(15.5f, 1.64f, 0), // d6
                new(11.5f, 1.64f, 0), // d8
                new(8, 1.64f, 2.5f), // d10
                new(8, 1.64f, -2.5f), // d12
                new(4, 1.64f, 0), // d20
            };

        public static IList<GameObject> ds = new List<GameObject>(); // ds: Dices.

        // Murat Sancak

        private Vector2[] Vector2s
        {
            get
            {
                if (!Monetization.IBL || IAP.HR(0))
                    return v2s0;
                else
                    return v2s1;
            }
        }

        // Murat Sancak

        private void Awake()
        {
            dDA = GameObject.Find("DD Ad Button");
            dDU = GameObject.Find("DD Up Button");
            d4A = GameObject.Find("D4 Ad Button");
            d4U = GameObject.Find("D4 Up Button");
            d6A = GameObject.Find("D6 Ad Button");
            d6U = GameObject.Find("D6 Up Button");
            d8A = GameObject.Find("D8 Ad Button");
            d8U = GameObject.Find("D8 Up Button");
            d10A = GameObject.Find("D10 Ad Button");
            d10U = GameObject.Find("D10 Up Button");
            d12A = GameObject.Find("D12 Ad Button");
            d12U = GameObject.Find("D12 Up Button");
            d20A = GameObject.Find("D20 Ad Button");
            d20U = GameObject.Find("D20 Up Button");
            dGO = GameObject.Find("Dices Game Object");

            for (int i = 0; i < rTs.Length; i++)
            {
                v2s0[i] = b + rTs[i].anchoredPosition;
                v2s1[i] = rTs[i].anchoredPosition;
            }
        }

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
            if (Preferences.Poly is 1)
            {
                Add(Create(dDHP[Preferences.DD], v3s[0]), dDs);
                Add(Create(d4HP[Preferences.D4], v3s[1]), d4s);
                Add(Create(d6HP[Preferences.D6], v3s[2]), d6s);
                Add(Create(d8HP[Preferences.D8], v3s[3]), d8s);
                Add(Create(d10HP[Preferences.D10], v3s[4]), d10s);
                Add(Create(d12HP[Preferences.D12], v3s[5]), d12s);
                Add(Create(d20HP[Preferences.D20], v3s[6]), d20s);
            }
            else
            {
                Add(Create(dDLP[Preferences.DD], v3s[0]), dDs);
                Add(Create(d4LP[Preferences.D4], v3s[1]), d4s);
                Add(Create(d6LP[Preferences.D6], v3s[2]), d6s);
                Add(Create(d8LP[Preferences.D8], v3s[3]), d8s);
                Add(Create(d12LP[Preferences.D12], v3s[5]), d12s);
                Add(Create(d20LP[Preferences.D20], v3s[6]), d20s);
                Add(Create(d10LP[Preferences.D10], v3s[4]), d10s);
            }

            Add(dDs.Last(), ds);
            Add(d4s.Last(), ds);
            Add(d6s.Last(), ds);
            Add(d8s.Last(), ds);
            Add(d10s.Last(), ds);
            Add(d12s.Last(), ds);
            Add(d20s.Last(), ds);

            StartCoroutine(Enumerator());
        }

        private void Update() => Button(IAP.HR(0), Monetization.iRL);

        // Murat Sancak

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                for (int i = 0; i < rTs.Length; i++)
                    rTs[i].anchoredPosition = Vector2s[i];

                foreach (GameObject d in ds) // d: Dice.
                    s += d.GetComponent<Dice>().surface;
                sT.GetComponent<TextMeshProUGUI>().text = s.ToString();

                if (!Monetization.IBL || IAP.HR(0))
                    uP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 256);
                else
                    uP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 346);

                if (s is 132)
                {
                    Handheld.Vibrate();
                    PG.Achievement(132);
                }
                PG.Achievement(ds.Count);
                PG.Leaderboard(s);

                s = 0;

                yield return wFS;
            }
        }

        // Murat Sancak

        private void Button(bool a, bool i) // a: Active, i: Interactable.
        {
            dDA.GetComponent<Button>().interactable =
            d4A.GetComponent<Button>().interactable =
            d6A.GetComponent<Button>().interactable =
            d8A.GetComponent<Button>().interactable =
            d10A.GetComponent<Button>().interactable =
            d12A.GetComponent<Button>().interactable =
            d20A.GetComponent<Button>().interactable =
            i;

            dDA.SetActive(!a && dDs.Count is not 2);
            dDU.SetActive(dDs.Count is not 2 && a);
            d4A.SetActive(!a && d4s.Count is not 2);
            d4U.SetActive(d4s.Count is not 2 && a);
            d6A.SetActive(!a && d6s.Count is not 2);
            d6U.SetActive(d6s.Count is not 2 && a);
            d8A.SetActive(!a && d8s.Count is not 2);
            d8U.SetActive(d8s.Count is not 2 && a);
            d10A.SetActive(!a && d10s.Count is not 2);
            d10U.SetActive(d10s.Count is not 2 && a);
            d12A.SetActive(!a && d12s.Count is not 2);
            d12U.SetActive(d12s.Count is not 2 && a);
            d20A.SetActive(!a && d20s.Count is not 2);
            d20U.SetActive(d20s.Count is not 2 && a);

            dDD.SetActive(dDs.Count is not 0);
            d4D.SetActive(d4s.Count is not 0);
            d6D.SetActive(d6s.Count is not 0);
            d8D.SetActive(d8s.Count is not 0);
            d10D.SetActive(d10s.Count is not 0);
            d12D.SetActive(d12s.Count is not 0);
            d20D.SetActive(d20s.Count is not 0);

            dDU.GetComponent<Button>().interactable =
            d4U.GetComponent<Button>().interactable =
            d6U.GetComponent<Button>().interactable =
            d8U.GetComponent<Button>().interactable =
            d10U.GetComponent<Button>().interactable =
            d12U.GetComponent<Button>().interactable =
            d20U.GetComponent<Button>().interactable =
            !i;
        }

        private void Remove(GameObject gO, IList<GameObject> gOs) // gO: Game Object, gOs: Game Objects.
        {
            gOs.Remove(gO);
            Destroy(gO);
        }

        private static GameObject Create(GameObject gO, Vector3 v3) => Instantiate(gO, v3, Quaternion.identity, dGO.transform); // gO: Game Object, v3: Vector3.

        private static void Add(GameObject gO, IList<GameObject> gOs) => gOs.Add(gO); // gO: Game Object, gOs: Game Objects.

        public void DDA() => Monetization.Rewarded(0);
        public void DDD()
        {
            if (dDs.Count is 1)
            {
                gO = dDs[0];
                Remove(gO, dDs);
                Remove(gO, ds);
            }
            else // if (dDs.Count is 2)
            {
                gO = dDs[1];
                Remove(gO, dDs);
                Remove(gO, ds);
            }
        }
        public void DDU()
        {
            if (Preferences.Poly is 1)
                Add(Create(dDHP[Preferences.DD], v3s[0] + dDs.Count * v3), dDs);
            else
                Add(Create(dDLP[Preferences.DD], v3s[0] + dDs.Count * v3), dDs);
            Add(dDs.Last(), ds);
        }

        public void D4A() => Monetization.Rewarded(4);
        public void D4D()
        {
            if (d4s.Count is 1)
            {
                gO = d4s[0];
                Remove(gO, d4s);
                Remove(gO, ds);
            }
            else // if (d4s.Count is 2)
            {
                gO = d4s[1];
                Remove(gO, d4s);
                Remove(gO, ds);
            }
        }
        public void D4U()
        {
            if (Preferences.Poly is 1)
                Add(Create(d4HP[Preferences.D4], v3s[1] + d4s.Count * v3), d4s);
            else
                Add(Create(d4LP[Preferences.D4], v3s[1] + d4s.Count * v3), d4s);
            Add(d4s.Last(), ds);
        }

        public void D6A() => Monetization.Rewarded(6);
        public void D6D()
        {
            if (d6s.Count is 1)
            {
                gO = d6s[0];
                Remove(gO, d6s);
                Remove(gO, ds);
            }
            else // if (d6s.Count is 2)
            {
                gO = d6s[1];
                Remove(gO, d6s);
                Remove(gO, ds);
            }
        }
        public void D6U()
        {
            if (Preferences.Poly is 1)
                Add(Create(d6HP[Preferences.D6], v3s[2] + d6s.Count * v3), d6s);
            else
                Add(Create(d6LP[Preferences.D6], v3s[2] + d6s.Count * v3), d6s);
            Add(d6s.Last(), ds);
        }

        public void D8A() => Monetization.Rewarded(8);
        public void D8D()
        {
            if (d8s.Count is 1)
            {
                gO = d8s[0];
                Remove(gO, d8s);
                Remove(gO, ds);
            }
            else // if (d8s.Count is 2)
            {
                gO = d8s[1];
                Remove(gO, d8s);
                Remove(gO, ds);
            }
        }
        public void D8U()
        {
            if (Preferences.Poly is 1)
                Add(Create(d8HP[Preferences.D8], v3s[3] + d8s.Count * v3), d8s);
            else
                Add(Create(d8LP[Preferences.D8], v3s[3] + d8s.Count * v3), d8s);
            Add(d8s.Last(), ds);
        }

        public void D10A() => Monetization.Rewarded(10);
        public void D10D()
        {
            if (d10s.Count is 1)
            {
                gO = d10s[0];
                Remove(gO, d10s);
                Remove(gO, ds);
            }
            else // if (d10s.Count is 2)
            {
                gO = d10s[1];
                Remove(gO, d10s);
                Remove(gO, ds);
            }
        }
        public void D10U()
        {
            if (Preferences.Poly is 1)
                Add(Create(d10HP[Preferences.D10], v3s[4] + d10s.Count * v3), d10s);
            else
                Add(Create(d10LP[Preferences.D10], v3s[4] + d10s.Count * v3), d10s);
            Add(d10s.Last(), ds);
        }

        public void D12A() => Monetization.Rewarded(12);
        public void D12D()
        {
            if (d12s.Count is 1)
            {
                gO = d12s[0];
                Remove(gO, d12s);
                Remove(gO, ds);
            }
            else // if (d12s.Count is 2)
            {
                gO = d12s[1];
                Remove(gO, d12s);
                Remove(gO, ds);
            }
        }
        public void D12U()
        {
            if (Preferences.Poly is 1)
                Add(Create(d12HP[Preferences.D12], v3s[5] + d12s.Count * v3), d12s);
            else
                Add(Create(d12LP[Preferences.D12], v3s[5] + d12s.Count * v3), d12s);
            Add(d12s.Last(), ds);
        }

        public void D20A() => Monetization.Rewarded(20);
        public void D20D()
        {
            if (d20s.Count is 1)
            {
                gO = d20s[0];
                Remove(gO, d20s);
                Remove(gO, ds);
            }
            else // if (d20s.Count is 2)
            {
                gO = d20s[1];
                Remove(gO, d20s);
                Remove(gO, ds);
            }
        }
        public void D20U()
        {
            if (Preferences.Poly is 1)
                Add(Create(d20HP[Preferences.D20], v3s[6] + d20s.Count * v3), d20s);
            else
                Add(Create(d20LP[Preferences.D20], v3s[6] + d20s.Count * v3), d20s);
            Add(d20s.Last(), ds);
        }

        public void Load(int s) => Scene.Load(s); // s: Scene.

        public void Pause(bool p) => Time.timeScale = p ? 0 : 1; // p: Pause.

        public static void Reward(int b) // b: Button.
        {
            switch (b)
            {
                case 0:
                    if (Preferences.Poly is 1)
                        Add(Create(dDHP[Preferences.DD], v3s[0] + dDs.Count * v3), dDs);
                    else
                        Add(Create(dDLP[Preferences.DD], v3s[0] + dDs.Count * v3), dDs);
                    Add(dDs.Last(), ds);
                    break;
                case 4:
                    if (Preferences.Poly is 1)
                        Add(Create(d4HP[Preferences.D4], v3s[1] + d4s.Count * v3), d4s);
                    else
                        Add(Create(d4LP[Preferences.D4], v3s[1] + d4s.Count * v3), d4s);
                    Add(d4s.Last(), ds);
                    break;
                case 6:
                    if (Preferences.Poly is 1)
                        Add(Create(d6HP[Preferences.D6], v3s[2] + d6s.Count * v3), d6s);
                    else
                        Add(Create(d6LP[Preferences.D6], v3s[2] + d6s.Count * v3), d6s);
                    Add(d6s.Last(), ds);
                    break;
                case 8:
                    if (Preferences.Poly is 1)
                        Add(Create(d8HP[Preferences.D8], v3s[3] + d8s.Count * v3), d8s);
                    else
                        Add(Create(d8LP[Preferences.D8], v3s[3] + d8s.Count * v3), d8s);
                    Add(d8s.Last(), ds);
                    break;
                case 10:
                    if (Preferences.Poly is 1)
                        Add(Create(d10HP[Preferences.D10], v3s[4] + d10s.Count * v3), d10s);
                    else
                        Add(Create(d10LP[Preferences.D10], v3s[4] + d10s.Count * v3), d10s);
                    Add(d10s.Last(), ds);
                    break;
                case 12:
                    if (Preferences.Poly is 1)
                        Add(Create(d12HP[Preferences.D12], v3s[5] + d12s.Count * v3), d12s);
                    else
                        Add(Create(d12LP[Preferences.D12], v3s[5] + d12s.Count * v3), d12s);
                    Add(d12s.Last(), ds);
                    break;
                case 20:
                    if (Preferences.Poly is 1)
                        Add(Create(d20HP[Preferences.D20], v3s[6] + d20s.Count * v3), d20s);
                    else
                        Add(Create(d20LP[Preferences.D20], v3s[6] + d20s.Count * v3), d20s);
                    Add(d20s.Last(), ds);
                    break;
            }
        }
    }
}

// Murat Sancak