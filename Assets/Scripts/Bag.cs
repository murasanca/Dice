// Murat Sancak

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

namespace murasanca
{
    public class Bag : MonoBehaviour
    {
        [SerializeField]
        private RectTransform[] rTs = new RectTransform[3]; // rTs: Rect Transforms.

        private float x;

        private int f; // f: For.

        private Touch t; // t: Touch.

        private readonly Vector2 b = Menu.b; // b: Banner.

        private readonly Vector2[]
            v2s0 = new Vector2[3], // v2s0: Vector2's 0.
            v2s1 = new Vector2[3]; // v2s1: Vector2's 1.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

        private static GameObject
            cB, // cB: Close Button.
            cBL, // cBL: Checkmark Button (Legacy).
            dsGO, // dsGO: Dices Game Object.
            iB, // iB: Inventory Button.
            kB, // kB: Key Button.
            kRI, // kRI: Key Raw Image.
            lB, // lB: Lock Button.
            pTTMP, // pTTMP: Product Text (TMP).
            sRI; // sRI: Shield Raw Image.

        private readonly static GameObject[] ds = new GameObject[7]; // ds: Dices.

        public static int s = -1; // s: Set.

        public readonly static string[] ps = new string[23] // ps: Products.
        {
            "0","1","2","3","4","5","6","7","8","9", // Data: 1.
            "10","11","12","13","14","15","16","17","18","19", // Data: 2.
            "20","21","22" // Data: 3.
        };

        // Murat Sancak

        private Vector2[] V2s => !Monetization.IBL||IAP.HR(0) ? v2s0 : v2s1; // V2s: Vector2's.

        // Murat Sancak

        private void Awake()
        {
            cB=GameObject.Find("Close Button");
            cBL = GameObject.Find("Checkmark Button (Legacy)");
            dsGO = GameObject.Find("Dices Game Object");
            iB = GameObject.Find("Inventory Button");
            kB = GameObject.Find("Key Button");
            kRI = GameObject.Find("Key Raw Image");
            lB = GameObject.Find("Lock Button");
            pTTMP = GameObject.Find("Product Text (TMP)");
            sRI = GameObject.Find("Shield Raw Image");

            for (f = 0; f < rTs.Length; f++)
            {
                v2s0[f] = b + rTs[f].anchoredPosition;
                v2s1[f] = rTs[f].anchoredPosition;
            }

            StartCoroutine(Enumerator());
        }

        private void Start() => Set(s);

        private void Update()
        {
            if (Input.touchCount is not 0)
            {
                if ((t=Input.GetTouch(0)).phase is TouchPhase.Began)
                    x = t.position.x;
                else if (256 < Mathf.Abs(x - t.position.x) && t.phase is TouchPhase.Ended)
                    Slide(Mathf.Sign(x - t.position.x));
            }
        }

        // Murat Sancak

        private System.Collections.IEnumerator Enumerator()
        {
            while(true)
            {
                for(f = 0; f < rTs.Length; f++)
                    rTs[f].anchoredPosition = V2s[f];

                yield return wFS;
            }
        }

        // Murat Sancak

        private static void Instantiate()
        {
            if (Preferences.Poly is 1)
            {
                ds[0] = Instantiate(Menu.dDHP[s], Menu.V3s[0], Quaternion.identity, dsGO.transform);
                ds[1] = Instantiate(Menu.d4HP[s], Menu.V3s[1], Quaternion.identity, dsGO.transform);
                ds[2] = Instantiate(Menu.d6HP[s], Menu.V3s[2], Quaternion.identity, dsGO.transform);
                ds[3] = Instantiate(Menu.d8HP[s], Menu.V3s[3], Quaternion.identity, dsGO.transform);
                ds[4] = Instantiate(Menu.d10HP[s], Menu.V3s[4], Quaternion.identity, dsGO.transform);
                ds[5] = Instantiate(Menu.d12HP[s], Menu.V3s[5], Quaternion.identity, dsGO.transform);
                ds[6] = Instantiate(Menu.d20HP[s], Menu.V3s[6], Quaternion.identity, dsGO.transform);
            }
            else
            {
                ds[0] = Instantiate(Menu.dDLP[s], Menu.V3s[0], Quaternion.identity, dsGO.transform);
                ds[1] = Instantiate(Menu.d4LP[s], Menu.V3s[1], Quaternion.identity, dsGO.transform);
                ds[2] = Instantiate(Menu.d6LP[s], Menu.V3s[2], Quaternion.identity, dsGO.transform);
                ds[3] = Instantiate(Menu.d8LP[s], Menu.V3s[3], Quaternion.identity, dsGO.transform);
                ds[4] = Instantiate(Menu.d10LP[s], Menu.V3s[4], Quaternion.identity, dsGO.transform);
                ds[5] = Instantiate(Menu.d12LP[s], Menu.V3s[5], Quaternion.identity, dsGO.transform);
                ds[6] = Instantiate(Menu.d20LP[s], Menu.V3s[6], Quaternion.identity, dsGO.transform);
            }
        }

        public void B() // B: Bag.
        {
            s = -1;
            Scene.Load(1);
        }

        public void Checkmark() => IAP.Checkmark(s);

        public void Inventory()
        {
            murasanca.Inventory.s =s;
            Scene.Load(3);
        }

        public void Key() => Set(s);

        public void Load(int s) => Scene.Load(s); // s: Scene.

        public void Lock()
        {
            cB.SetActive(true);
            cBL.GetComponent<IAPButton>().productId=s is not -1 ? IAP.ps[s] : IAP.ps[0];
            cBL.SetActive(true);
            kB.GetComponent<Button>().interactable = true;
            kB.SetActive(true);
            lB.SetActive(false);
        }
         
        public void Reload() => PlayerPrefs.DeleteAll();

        public void Slide(float s) // s: Sign.
        {
            if (s is 1)
                if (Bag.s is not 22)
                    ++Bag.s;
                else
                    Bag.s = -1;
            else if (Bag.s is not -1)
                --Bag.s;
            else
                Bag.s = 22;

            Set(Bag.s);
        }

        public static void Close()
        {
            cB.SetActive(false);
            cBL.SetActive(false);
            kB.SetActive(false);
            lB.SetActive(true);
        }

        public static void Set() => Set(Preferences.DD = Preferences.D4 = Preferences.D6 = Preferences.D8 = Preferences.D10 = Preferences.D12 = Preferences.D20 = s);

        public static void Set(int s) // s: Set.
        {
            cB.SetActive(false);
            cBL.SetActive(false);
            
            foreach (GameObject d in ds) // d: Dice.
                Destroy(d);

            switch (s)
            {
                case -1: // Shield
                    iB.SetActive(false);
                    kRI.SetActive(false);
                    pTTMP.GetComponent<TextMeshProUGUI>().text = "0";
                    pTTMP.SetActive(true);
                    sRI.SetActive(true);

                    if (IAP.HR(0))
                    {
                        kB.GetComponent<Button>().interactable = false;
                        kB.SetActive(true);
                        lB.SetActive(false);
                    }
                    else
                    {
                        kB.SetActive(false);
                        lB.SetActive(true);
                    }
                    break;
                case 0:
                    iB.SetActive(true);
                    kB.SetActive(false);
                    kRI.SetActive(true);
                    lB.SetActive(false);
                    pTTMP.SetActive(false);
                    sRI.SetActive(false);

                    Instantiate();
                    break;
                default:
                    kB.SetActive(false);
                    sRI.SetActive(false);

                    if (IAP.HR(s))
                    {
                        iB.SetActive(true);
                        kRI.SetActive(true);
                        lB.SetActive(false);
                        pTTMP.SetActive(false);
                    }
                    else
                    {
                        iB.SetActive(false);
                        kRI.SetActive(false);
                        lB.SetActive(true);
                        pTTMP.GetComponent<TextMeshProUGUI>().text = ps[s];
                        pTTMP.SetActive(true);
                    }

                    Instantiate();
                    break;
            }
        }
    }
}

// Murat Sancak