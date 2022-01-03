// murasanca

using System;
using UnityEngine;

namespace murasanca
{
    public class Menu : MonoBehaviour
    {
        public static GameObject[] // HP: High Poly, LP: Low Poly.
            dDHP = new GameObject[23], dDLP = new GameObject[23],
            d4HP = new GameObject[23], d4LP = new GameObject[23],
            d6HP = new GameObject[23], d6LP = new GameObject[23],
            d8HP = new GameObject[23], d8LP = new GameObject[23],
            d10HP = new GameObject[23], d10LP = new GameObject[23],
            d12HP = new GameObject[23], d12LP = new GameObject[23],
            d20HP = new GameObject[23], d20LP = new GameObject[23];

        public static Vector3[] Vector3s
        {
            get
            {
                if (!Monetization.IsInitialized||IAP.HasReceipt(0))
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

        public readonly static WaitForSeconds waitForSeconds = new(1);

        private void Awake()
        {
            if (!Preferences.HasKey("dD")) Preferences.DD = 0;
            if (!Preferences.HasKey("d4")) Preferences.D4 = 0;
            if (!Preferences.HasKey("d6")) Preferences.D6 = 0;
            if (!Preferences.HasKey("d8")) Preferences.D8 = 0;
            if (!Preferences.HasKey("d10")) Preferences.D10 = 0;
            if (!Preferences.HasKey("d12")) Preferences.D12 = 0;
            if (!Preferences.HasKey("d20")) Preferences.D20 = 0;
            if (!Preferences.HasKey("mPV")) Preferences.MPV = 1;
            if (!Preferences.HasKey("mVV")) Preferences.MVV = .64f;
            if (!Preferences.HasKey("poly")) Preferences.Poly = 1;
            if (!Preferences.HasKey("score")) Preferences.Score = 26;

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
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Scene.Load(2);
        }

        public void Goblet() => PlayServices.ShowLeaderboard();

        public void Scroll() => PlayServices.ShowAchievements();

        public void BagB() // B: Button.
        {
            Bag.set = -1;
            Scene.Load(1);
        }

        public void InventoryB() // B: Button.
        {
            Inventory.set = -1;
            Scene.Load(3);
        }

        public void Load(int scene) => Scene.Load(scene);

        public void Star() => Application.OpenURL("market://details?id=com.murasanca.Dice");
    }
}

// murasanca