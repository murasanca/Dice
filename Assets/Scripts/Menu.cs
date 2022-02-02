// murasanca

using System;
using UnityEngine;

namespace murasanca
{
    public class Menu : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] gameObjects = new GameObject[6];

        private readonly Vector2[]
                    vector2s0 = new Vector2[6], // Shield.
                    vector2s1 = new Vector2[6]; // Advertisement.

        public static GameObject[] // HP: High Poly, LP: Low Poly.
            dDHP = new GameObject[23], dDLP = new GameObject[23],
            d4HP = new GameObject[23], d4LP = new GameObject[23],
            d6HP = new GameObject[23], d6LP = new GameObject[23],
            d8HP = new GameObject[23], d8LP = new GameObject[23],
            d10HP = new GameObject[23], d10LP = new GameObject[23],
            d12HP = new GameObject[23], d12LP = new GameObject[23],
            d20HP = new GameObject[23], d20LP = new GameObject[23];

        public readonly static Vector2 banner = 90 * Vector2.up;

        public readonly static WaitForSeconds wFS = new(1); // wFS: Wait For Seconds.

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

        public static Vector3[] Vector3s
        {
            get
            {
                if (!Monetization.IBL || IAP.HR(0))
                    return new Vector3[7]
                    {
                        new(2, 1.64f, 0), // dD
                        new(9.5f, 1.35f, 0), // d4
                        new(16.5f, 1.64f, 0), // d6
                        new(12.5f, 1.64f, 0), // d8
                        new(9.5f, 1.64f, 2.5f), // d10
                        new(9.5f, 1.64f, -2.5f), // d12
                        new(5.5f, 1.64f, 0) // d20
                    };
                else
                    return new Vector3[7]
                    {
                        new(1.5f, 1.64f, 0), // dD
                        new(9, 1.35f, 0), // d4
                        new(16, 1.64f, 0), // d6
                        new(12, 1.64f, 0), // d8
                        new(9, 1.64f, 2.5f), // d10
                        new(9, 1.64f, -2.5f), // d12
                        new(5, 1.64f, 0), // d20
                    };
            }
        }

        // murasanca

        private void Awake()
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                vector2s0[i] = banner + gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
                vector2s1[i] = gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
            }

            if (dDHP is not null) for (int i = 0; i < dDHP.Length; i++) dDHP[i] = Resources.Load<GameObject>(String.Concat("DD High Poly ", i));
            if (dDLP is not null) for (int i = 0; i < dDLP.Length; i++) dDLP[i] = Resources.Load<GameObject>(String.Concat("DD Low Poly ", i));

            if (d4HP is not null) for (int i = 0; i < d4HP.Length; i++) d4HP[i] = Resources.Load<GameObject>(String.Concat("D4 High Poly ", i));
            if (d4LP is not null) for (int i = 0; i < d4LP.Length; i++) d4LP[i] = Resources.Load<GameObject>(String.Concat("D4 Low Poly ", i));

            if (d6HP is not null) for (int i = 0; i < d6HP.Length; i++) d6HP[i] = Resources.Load<GameObject>(String.Concat("D6 High Poly ", i));
            if (d6LP is not null) for (int i = 0; i < d6LP.Length; i++) d6LP[i] = Resources.Load<GameObject>(String.Concat("D6 Low Poly ", i));

            if (d8HP is not null) for (int i = 0; i < d8HP.Length; i++) d8HP[i] = Resources.Load<GameObject>(String.Concat("D8 High Poly ", i));
            if (d8LP is not null) for (int i = 0; i < d8LP.Length; i++) d8LP[i] = Resources.Load<GameObject>(String.Concat("D8 Low Poly ", i));

            if (d10HP is not null) for (int i = 0; i < d10HP.Length; i++) d10HP[i] = Resources.Load<GameObject>(String.Concat("D10 High Poly ", i));
            if (d10LP is not null) for (int i = 0; i < d10LP.Length; i++) d10LP[i] = Resources.Load<GameObject>(String.Concat("D10 Low Poly ", i));

            if (d12HP is not null) for (int i = 0; i < d12HP.Length; i++) d12HP[i] = Resources.Load<GameObject>(String.Concat("D12 High Poly ", i));
            if (d12LP is not null) for (int i = 0; i < d12LP.Length; i++) d12LP[i] = Resources.Load<GameObject>(String.Concat("D12 Low Poly ", i));

            if (d20HP is not null) for (int i = 0; i < d20HP.Length; i++) d20HP[i] = Resources.Load<GameObject>(String.Concat("D20 High Poly ", i));
            if (d20LP is not null) for (int i = 0; i < d20LP.Length; i++) d20LP[i] = Resources.Load<GameObject>(String.Concat("D20 Low Poly ", i));

            StartCoroutine(Enumerator());
        }

        // murasanca

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                    gameObjects[i].GetComponent<RectTransform>().anchoredPosition = Vector2s[i];

                yield return wFS;
            }
        }

        // murasanca

        public void B() // B: Bag.
        {
            Bag.set = -1;
            Scene.Load(1);
        }

        public void Goblet() => PG.Leaderboard();

        public void I() // I: Inventory.
        {
            Inventory.set = -1;
            Scene.Load(3);
        }

        public void Load(int scene) => Scene.Load(scene);

        public void Scroll() => PG.Achievements();

        public void Star() => Application.OpenURL("market://details?id=com.murasanca.Dice");
    }
}

// murasanca