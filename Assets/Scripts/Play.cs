// murasanca

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace murasanca
{
    public class Play : MonoBehaviour
    {
        [SerializeField]
        private GameObject // D: Down.
            dDD, d4D, d6D, d8D, d10D, d12D, d20D, scoreText, upperPanel;

        [SerializeField]
        private GameObject[] gameObjects = new GameObject[4];

        private int score = 0;

        private readonly Vector2 banner = Menu.banner;

        private readonly Vector2[]
            vector2s0 = new Vector2[4], // Shield.
            vector2s1 = new Vector2[4]; // Advertisement.

        private readonly WaitForSeconds wFS = new(.1f); // wFS: Wait For Seconds.

        private static GameObject // A: Ad, U: Up.
            dDA, dDU,
            d4A, d4U,
            d6A, d6U,
            d8A, d8U,
            d10A, d10U,
            d12A, d12U,
            d20A, d20U,
            dicesGameObject;

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

        private readonly static Vector3 vector3 = 1.5f * Vector2.up;

        private readonly static Vector3[]
            vector3s = new Vector3[7]
            {
                new(0, 1.64f, 0), // dD
                new(8, 1.35f, 0), // d4
                new(15.5f, 1.64f, 0), // d6
                new(11.5f, 1.64f, 0), // d8
                new(8, 1.64f, 2.5f), // d10
                new(8, 1.64f, -2.5f), // d12
                new(4, 1.64f, 0), // d20
            };

        public static IList<GameObject> dices = new List<GameObject>();

        // murasanca

        private Vector2[] Vector2s
        {
            get
            {
                if (!Monetization.IBL || IAP.HR(0))
                    return vector2s0;
                else
                    return vector2s1;
            }
        }

        // murasanca

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
            dicesGameObject = GameObject.Find("Dices Game Object");

            for (int i = 0; i < gameObjects.Length; i++)
            {
                vector2s0[i] = banner + gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
                vector2s1[i] = gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
            }

            if (!Monetization.IRL || IAP.HR(0))
            {
                dDA.SetActive(false);
                dDU.SetActive(true);
                d4A.SetActive(false);
                d4U.SetActive(true);
                d6A.SetActive(false);
                d6U.SetActive(true);
                d8A.SetActive(false);
                d8U.SetActive(true);
                d10A.SetActive(false);
                d10U.SetActive(true);
                d12A.SetActive(false);
                d12U.SetActive(true);
                d20A.SetActive(false);
                d20U.SetActive(true);
            }
            else if (Monetization.IRL)
            {
                dDA.SetActive(true);
                dDU.SetActive(false);
                d4A.SetActive(true);
                d4U.SetActive(false);
                d6A.SetActive(true);
                d6U.SetActive(false);
                d8A.SetActive(true);
                d8U.SetActive(false);
                d10A.SetActive(true);
                d10U.SetActive(false);
                d12A.SetActive(true);
                d12U.SetActive(false);
                d20A.SetActive(true);
                d20U.SetActive(false);
            }

            StartCoroutine(Enumerator());

            Time.timeScale = 1;
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
            dices.Clear();

            Time.timeScale = Mathf.Abs(Preferences.Pitch);
        }

        private void Start()
        {
            if (Preferences.Poly is 1)
            {
                Add(Create(dDHP[Preferences.DD], vector3s[0]), dDs);
                Add(Create(d4HP[Preferences.D4], vector3s[1]), d4s);
                Add(Create(d6HP[Preferences.D6], vector3s[2]), d6s);
                Add(Create(d8HP[Preferences.D8], vector3s[3]), d8s);
                Add(Create(d10HP[Preferences.D10], vector3s[4]), d10s);
                Add(Create(d12HP[Preferences.D12], vector3s[5]), d12s);
                Add(Create(d20HP[Preferences.D20], vector3s[6]), d20s);
            }
            else
            {
                Add(Create(dDLP[Preferences.DD], vector3s[0]), dDs);
                Add(Create(d4LP[Preferences.D4], vector3s[1]), d4s);
                Add(Create(d6LP[Preferences.D6], vector3s[2]), d6s);
                Add(Create(d8LP[Preferences.D8], vector3s[3]), d8s);
                Add(Create(d12LP[Preferences.D12], vector3s[5]), d12s);
                Add(Create(d20LP[Preferences.D20], vector3s[6]), d20s);
                Add(Create(d10LP[Preferences.D10], vector3s[4]), d10s);
            }

            Add(dDs.Last(), dices);
            Add(d4s.Last(), dices);
            Add(d6s.Last(), dices);
            Add(d8s.Last(), dices);
            Add(d10s.Last(), dices);
            Add(d12s.Last(), dices);
            Add(d20s.Last(), dices);
        }

        // murasanca

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                    gameObjects[i].GetComponent<RectTransform>().anchoredPosition = Vector2s[i];

                foreach (GameObject dice in dices)
                    score += dice.GetComponent<Dice>().surface;
                scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();

                if (!Monetization.IBL || IAP.HR(0))
                    upperPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 256);
                else
                    upperPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 346);

                if (!Monetization.IRL || IAP.HR(0))
                {
                    if (dDs.Count is 1)
                    {
                        dDA.SetActive(false);
                        dDU.SetActive(true);
                    }
                    if (d4s.Count is 1)
                    {
                        d4A.SetActive(false);
                        d4U.SetActive(true);
                    }
                    if (d6s.Count is 1)
                    {
                        d6A.SetActive(false);
                        d6U.SetActive(true);
                    }
                    if (d8s.Count is 1)
                    {
                        d8A.SetActive(false);
                        d8U.SetActive(true);
                    }
                    if (d10s.Count is 1)
                    {
                        d10A.SetActive(false);
                        d10U.SetActive(true);
                    }
                    if (d12s.Count is 1)
                    {
                        d12A.SetActive(false);
                        d12U.SetActive(true);
                    }
                    if (d20s.Count is 1)
                    {
                        d20A.SetActive(false);
                        d20U.SetActive(true);
                    }
                }
                else
                {
                    if (dDs.Count is 1)
                    {
                        dDA.SetActive(true);
                        dDU.SetActive(false);
                    }
                    if (d4s.Count is 1)
                    {
                        d4A.SetActive(true);
                        d4U.SetActive(false);
                    }
                    if (d6s.Count is 1)
                    {
                        d6A.SetActive(true);
                        d6U.SetActive(false);
                    }
                    if (d8s.Count is 1)
                    {
                        d8A.SetActive(true);
                        d8U.SetActive(false);
                    }
                    if (d10s.Count is 1)
                    {
                        d10A.SetActive(true);
                        d10U.SetActive(false);
                    }
                    if (d12s.Count is 1)
                    {
                        d12A.SetActive(true);
                        d12U.SetActive(false);
                    }
                    if (d20s.Count is 1)
                    {
                        d20A.SetActive(true);
                        d20U.SetActive(false);
                    }
                }

                if (score is 132)
                {
                    Handheld.Vibrate();
                    PG.Progress(132);
                }
                PG.Progress(dices.Count);
                PG.Score(score);

                score = 0;

                yield return wFS;
            }
        }

        // murasanca

        private void Remove(GameObject gameObject, IList<GameObject> gameObjects)
        {
            gameObjects.Remove(gameObject);
            Destroy(gameObject);
        }

        private static GameObject Create(GameObject original, Vector3 position) => Instantiate(original, position, Quaternion.identity, dicesGameObject.transform);

        private static void Add(GameObject gameObject, IList<GameObject> gameObjects) => gameObjects.Add(gameObject);

        public void DDA() => Monetization.Rewarded(0);
        public void DDD()
        {
            GameObject gameObject;
            if (dDs.Count is 1)
            {
                dDA.SetActive(false);
                dDD.SetActive(false);
                dDU.SetActive(true);

                gameObject = dDs[0];
                Remove(gameObject, dDs);
                Remove(gameObject, dices);
            }
            else // if (dDs.Count is 2)
            {
                gameObject = dDs[1];
                Remove(gameObject, dDs);
                Remove(gameObject, dices);
            }
        }
        public void DDU()
        {
            if (dDs.Count is 0)
                dDD.SetActive(true);
            else // if (dDs.Count is 1)
            {
                // dDA.SetActive(false);
                dDU.SetActive(false);
                // dDD.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(dDHP[Preferences.DD], vector3s[0] + dDs.Count * vector3), dDs);
            else
                Add(Create(dDLP[Preferences.DD], vector3s[0] + dDs.Count * vector3), dDs);
            Add(dDs.Last(), dices);
        }

        public void D4A() => Monetization.Rewarded(4);
        public void D4D()
        {
            GameObject gameObject;
            if (d4s.Count is 1)
            {
                d4A.SetActive(false);
                d4D.SetActive(false);
                d4U.SetActive(true);

                gameObject = d4s[0];
                Remove(gameObject, d4s);
                Remove(gameObject, dices);
            }
            else // if (d4s.Count is 2)
            {
                gameObject = d4s[1];
                Remove(gameObject, d4s);
                Remove(gameObject, dices);
            }
        }
        public void D4U()
        {
            if (d4s.Count is 0)
                d4D.SetActive(true);
            else // if (d4s.Count is 1)
            {
                // d4A.SetActive(false);
                d4U.SetActive(false);
                // d4D.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(d4HP[Preferences.D4], vector3s[1] + d4s.Count * vector3), d4s);
            else
                Add(Create(d4LP[Preferences.D4], vector3s[1] + d4s.Count * vector3), d4s);
            Add(d4s.Last(), dices);
        }

        public void D6A() => Monetization.Rewarded(6);
        public void D6D()
        {
            GameObject gameObject;
            if (d6s.Count is 1)
            {
                d6A.SetActive(false);
                d6D.SetActive(false);
                d6U.SetActive(true);

                gameObject = d6s[0];
                Remove(gameObject, d6s);
                Remove(gameObject, dices);
            }
            else // if (d6s.Count is 2)
            {
                gameObject = d6s[1];
                Remove(gameObject, d6s);
                Remove(gameObject, dices);
            }
        }
        public void D6U()
        {
            if (d6s.Count is 0)
                d6D.SetActive(true);
            else // if (d6s.Count is 1)
            {
                // d6A.SetActive(false);
                d6U.SetActive(false);
                // d6D.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(d6HP[Preferences.D6], vector3s[2] + d6s.Count * vector3), d6s);
            else
                Add(Create(d6LP[Preferences.D6], vector3s[2] + d6s.Count * vector3), d6s);
            Add(d6s.Last(), dices);
        }

        public void D8A() => Monetization.Rewarded(8);
        public void D8D()
        {
            GameObject gameObject;
            if (d8s.Count is 1)
            {
                d8A.SetActive(false);
                d8D.SetActive(false);
                d8U.SetActive(true);

                gameObject = d8s[0];
                Remove(gameObject, d8s);
                Remove(gameObject, dices);
            }
            else // if (d8s.Count is 2)
            {
                gameObject = d8s[1];
                Remove(gameObject, d8s);
                Remove(gameObject, dices);
            }
        }
        public void D8U()
        {
            if (d8s.Count is 0)
                d8D.SetActive(true);
            else // if (d8s.Count is 1)
            {
                // d8A.SetActive(false);
                d8U.SetActive(false);
                // d8D.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(d8HP[Preferences.D8], vector3s[3] + d8s.Count * vector3), d8s);
            else
                Add(Create(d8LP[Preferences.D8], vector3s[3] + d8s.Count * vector3), d8s);
            Add(d8s.Last(), dices);
        }

        public void D10A() => Monetization.Rewarded(10);
        public void D10D()
        {
            GameObject gameObject;
            if (d10s.Count is 1)
            {
                d10A.SetActive(false);
                d10D.SetActive(false);
                d10U.SetActive(true);

                gameObject = d10s[0];
                Remove(gameObject, d10s);
                Remove(gameObject, dices);
            }
            else // if (d10s.Count is 2)
            {
                gameObject = d10s[1];
                Remove(gameObject, d10s);
                Remove(gameObject, dices);
            }
        }
        public void D10U()
        {
            if (d10s.Count is 0)
                d10D.SetActive(true);
            else // if (d10s.Count is 1)
            {
                // d10A.SetActive(false);
                d10U.SetActive(false);
                // d10D.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(d10HP[Preferences.D10], vector3s[4] + d10s.Count * vector3), d10s);
            else
                Add(Create(d10LP[Preferences.D10], vector3s[4] + d10s.Count * vector3), d10s);
            Add(d10s.Last(), dices);
        }

        public void D12A() => Monetization.Rewarded(12);
        public void D12D()
        {
            GameObject gameObject;
            if (d12s.Count is 1)
            {
                d12A.SetActive(false);
                d12D.SetActive(false);
                d12U.SetActive(true);

                gameObject = d12s[0];
                Remove(gameObject, d12s);
                Remove(gameObject, dices);
            }
            else // if (d12s.Count is 2)
            {
                gameObject = d12s[1];
                Remove(gameObject, d12s);
                Remove(gameObject, dices);
            }
        }
        public void D12U()
        {
            if (d12s.Count is 0)
                d12D.SetActive(true);
            else // if (d12s.Count is 1)
            {
                // d12A.SetActive(false);
                d12U.SetActive(false);
                // d12D.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(d12HP[Preferences.D12], vector3s[5] + d12s.Count * vector3), d12s);
            else
                Add(Create(d12LP[Preferences.D12], vector3s[5] + d12s.Count * vector3), d12s);
            Add(d12s.Last(), dices);
        }

        public void D20A() => Monetization.Rewarded(20);
        public void D20D()
        {
            GameObject gameObject;
            if (d20s.Count is 1)
            {
                d20A.SetActive(false);
                d20D.SetActive(false);
                d20U.SetActive(true);

                gameObject = d20s[0];
                Remove(gameObject, d20s);
                Remove(gameObject, dices);
            }
            else // if (d20s.Count is 2)
            {
                gameObject = d20s[1];
                Remove(gameObject, d20s);
                Remove(gameObject, dices);
            }
        }
        public void D20U()
        {
            if (d20s.Count is 0)
                d20D.SetActive(true);
            else // if (d20s.Count is 1)
            {
                // d20A.SetActive(false);
                d20U.SetActive(false);
                // d20D.SetActive(true);
            }

            if (Preferences.Poly is 1)
                Add(Create(d20HP[Preferences.D20], vector3s[6] + d20s.Count * vector3), d20s);
            else
                Add(Create(d20LP[Preferences.D20], vector3s[6] + d20s.Count * vector3), d20s);
            Add(d20s.Last(), dices);
        }

        public void Load(int scene) => Scene.Load(scene);

        public void Pause(bool pause) => Time.timeScale = pause ? 0 : 1;

        public static void Reward(int button)
        {
            switch (button)
            {
                case 0:
                    if (Preferences.Poly is 1)
                        Add(Create(dDHP[Preferences.DD], vector3s[0] + dDs.Count * vector3), dDs);
                    else
                        Add(Create(dDLP[Preferences.DD], vector3s[0] + dDs.Count * vector3), dDs);
                    Add(dDs.Last(), dices);

                    dDA.SetActive(false);
                    dDU.SetActive(false);
                    break;
                case 4:
                    if (Preferences.Poly is 1)
                        Add(Create(d4HP[Preferences.D4], vector3s[1] + d4s.Count * vector3), d4s);
                    else
                        Add(Create(d4LP[Preferences.D4], vector3s[1] + d4s.Count * vector3), d4s);
                    Add(d4s.Last(), dices);

                    d4A.SetActive(false);
                    d4U.SetActive(false);
                    break;
                case 6:
                    if (Preferences.Poly is 1)
                        Add(Create(d6HP[Preferences.D6], vector3s[2] + d6s.Count * vector3), d6s);
                    else
                        Add(Create(d6LP[Preferences.D6], vector3s[2] + d6s.Count * vector3), d6s);
                    Add(d6s.Last(), dices);

                    d6A.SetActive(false);
                    d6U.SetActive(false);
                    break;
                case 8:
                    if (Preferences.Poly is 1)
                        Add(Create(d8HP[Preferences.D8], vector3s[3] + d8s.Count * vector3), d8s);
                    else
                        Add(Create(d8LP[Preferences.D8], vector3s[3] + d8s.Count * vector3), d8s);
                    Add(d8s.Last(), dices);

                    d8A.SetActive(false);
                    d8U.SetActive(false);
                    break;
                case 10:
                    if (Preferences.Poly is 1)
                        Add(Create(d10HP[Preferences.D10], vector3s[4] + d10s.Count * vector3), d10s);
                    else
                        Add(Create(d10LP[Preferences.D10], vector3s[4] + d10s.Count * vector3), d10s);
                    Add(d10s.Last(), dices);

                    d10A.SetActive(false);
                    d10U.SetActive(false);
                    break;
                case 12:
                    if (Preferences.Poly is 1)
                        Add(Create(d12HP[Preferences.D12], vector3s[5] + d12s.Count * vector3), d12s);
                    else
                        Add(Create(d12LP[Preferences.D12], vector3s[5] + d12s.Count * vector3), d12s);
                    Add(d12s.Last(), dices);

                    d12A.SetActive(false);
                    d12U.SetActive(false);
                    break;
                case 20:
                    if (Preferences.Poly is 1)
                        Add(Create(d20HP[Preferences.D20], vector3s[6] + d20s.Count * vector3), d20s);
                    else
                        Add(Create(d20LP[Preferences.D20], vector3s[6] + d20s.Count * vector3), d20s);
                    Add(d20s.Last(), dices);

                    d20A.SetActive(false);
                    d20U.SetActive(false);
                    break;
            }
        }
    }
}

// murasanca