// murasanca

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

        private Touch touch;

        private readonly Vector2 b = Menu.b; // b: Banner.

        private readonly Vector2[]
            v2s0 = new Vector2[3], // v2s0: Vector2's 0.
            v2s1 = new Vector2[3]; // v2s1: Vector2's 1.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

        private static GameObject checkmarkButton, closeButton, dicesGameObject, inventoryButton, keyButton, keyRawImage, lockButton, productText, shieldRawImage;

        private readonly static GameObject[] dices = new GameObject[7];

        public readonly static string[] products = new string[23]
        {
            "0","1","2","3","4","5","6","7","8","9", // Data: 1.
            "10","11","12","13","14","15","16","17","18","19", // Data: 2.
            "20","21","22" // Data: 3.
        };

        public static int set = -1;

        // murasanca

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

        // murasanca

        private void Awake()
        {
            checkmarkButton = GameObject.Find("Checkmark Button (Legacy)");
            closeButton = GameObject.Find("Close Button");
            dicesGameObject = GameObject.Find("Dices Game Object");
            inventoryButton = GameObject.Find("Inventory Button");
            keyButton = GameObject.Find("Key Button");
            keyRawImage = GameObject.Find("Key Raw Image");
            lockButton = GameObject.Find("Lock Button");
            productText = GameObject.Find("Product Text (TMP)");
            shieldRawImage = GameObject.Find("Shield Raw Image");

            for (int i = 0; i < rTs.Length; i++)
            {
                v2s0[i] = b + rTs[i].anchoredPosition;
                v2s1[i] = rTs[i].anchoredPosition;
            }

            StartCoroutine(Enumerator());
        }

        private void Start() => Set(set);

        private void Update()
        {
            if (Input.touchCount is not 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase is TouchPhase.Began)
                    x = touch.position.x;
                else if (256 < Mathf.Abs(x - touch.position.x) && touch.phase is TouchPhase.Ended)
                    Slide(Mathf.Sign(x - touch.position.x));
            }
        }

        // murasanca

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                for (int i = 0; i < rTs.Length; i++)
                    rTs[i].anchoredPosition = Vector2s[i];

                yield return wFS;
            }
        }

        // murasanca
        
        private static void Instantiate()
        {
            if (Preferences.Poly is 1)
            {
                dices[0] = Instantiate(Menu.dDHP[set], Menu.V3s[0], Quaternion.identity, dicesGameObject.transform);
                dices[1] = Instantiate(Menu.d4HP[set], Menu.V3s[1], Quaternion.identity, dicesGameObject.transform);
                dices[2] = Instantiate(Menu.d6HP[set], Menu.V3s[2], Quaternion.identity, dicesGameObject.transform);
                dices[3] = Instantiate(Menu.d8HP[set], Menu.V3s[3], Quaternion.identity, dicesGameObject.transform);
                dices[4] = Instantiate(Menu.d10HP[set], Menu.V3s[4], Quaternion.identity, dicesGameObject.transform);
                dices[5] = Instantiate(Menu.d12HP[set], Menu.V3s[5], Quaternion.identity, dicesGameObject.transform);
                dices[6] = Instantiate(Menu.d20HP[set], Menu.V3s[6], Quaternion.identity, dicesGameObject.transform);
            }
            else
            {
                dices[0] = Instantiate(Menu.dDLP[set], Menu.V3s[0], Quaternion.identity, dicesGameObject.transform);
                dices[1] = Instantiate(Menu.d4LP[set], Menu.V3s[1], Quaternion.identity, dicesGameObject.transform);
                dices[2] = Instantiate(Menu.d6LP[set], Menu.V3s[2], Quaternion.identity, dicesGameObject.transform);
                dices[3] = Instantiate(Menu.d8LP[set], Menu.V3s[3], Quaternion.identity, dicesGameObject.transform);
                dices[4] = Instantiate(Menu.d10LP[set], Menu.V3s[4], Quaternion.identity, dicesGameObject.transform);
                dices[5] = Instantiate(Menu.d12LP[set], Menu.V3s[5], Quaternion.identity, dicesGameObject.transform);
                dices[6] = Instantiate(Menu.d20LP[set], Menu.V3s[6], Quaternion.identity, dicesGameObject.transform);
            }
        }

        public void B() // B: Bag.
        {
            set = -1;
            Scene.Load(1);
        }

        public void Checkmark() => IAP.Checkmark(set);

        public void I() // I: Inventory.
        {
            Inventory.s = set;
            Scene.Load(3);
        }

        public void Key() => Set(set);

        public void Load(int s) => Scene.Load(s); // s: Scene.

        public void Lock()
        {
            if (set is not -1)
                checkmarkButton.GetComponent<IAPButton>().productId = IAP.ps[set];
            else
                checkmarkButton.GetComponent<IAPButton>().productId = IAP.ps[0];

            checkmarkButton.SetActive(true);
            closeButton.SetActive(true);
            keyButton.GetComponent<Button>().interactable = true;
            keyButton.SetActive(true);
            lockButton.SetActive(false);
        }
         
        public void Reload() => PlayerPrefs.DeleteAll();

        public void Slide(float s) // s: Sign.
        {
            if (s is 1)
                if (set is not 22)
                    ++set;
                else
                    set = -1;
            else if (set is not -1)
                --set;
            else
                set = 22;

            Set(set);
        }

        public static void Close()
        {
            checkmarkButton.SetActive(false);
            closeButton.SetActive(false);
            keyButton.SetActive(false);
            lockButton.SetActive(true);
        }

        public static void Set() => Set(Preferences.DD = Preferences.D4 = Preferences.D6 = Preferences.D8 = Preferences.D10 = Preferences.D12 = Preferences.D20 = set);

        public static void Set(int s) // s: Set.
        {
            checkmarkButton.SetActive(false);
            closeButton.SetActive(false);

            foreach (GameObject d in dices) // d: Dice.
                Destroy(d);

            switch (s)
            {
                case -1: // Shield
                    inventoryButton.SetActive(false);
                    keyRawImage.SetActive(false);
                    productText.GetComponent<TextMeshProUGUI>().text = "0";
                    productText.SetActive(true);
                    shieldRawImage.SetActive(true);

                    if (IAP.HR(0))
                    {
                        keyButton.GetComponent<Button>().interactable = false;
                        keyButton.SetActive(true);
                        lockButton.SetActive(false);
                    }
                    else
                    {
                        keyButton.SetActive(false);
                        lockButton.SetActive(true);
                    }
                    break;
                case 0:
                    inventoryButton.SetActive(true);
                    keyButton.SetActive(false);
                    keyRawImage.SetActive(true);
                    lockButton.SetActive(false);
                    productText.SetActive(false);
                    shieldRawImage.SetActive(false);

                    Instantiate();
                    break;
                default:
                    keyButton.SetActive(false);
                    shieldRawImage.SetActive(false);

                    if (IAP.HR(s))
                    {
                        inventoryButton.SetActive(true);
                        keyRawImage.SetActive(true);
                        lockButton.SetActive(false);
                        productText.SetActive(false);
                    }
                    else
                    {
                        inventoryButton.SetActive(false);
                        keyRawImage.SetActive(false);
                        lockButton.SetActive(true);
                        productText.GetComponent<TextMeshProUGUI>().text = products[s];
                        productText.SetActive(true);
                    }

                    Instantiate();
                    break;
            }
        }
    }
}

// murasanca