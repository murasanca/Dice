// murasanca

using TMPro;
using UnityEngine;

namespace murasanca
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private RectTransform[] rTs = new RectTransform[3]; // rTs: Rect Transforms.

        private float x;

        private Touch touch;

        private readonly Vector2 b = Menu.b;

        private readonly Vector2[]
            v2s0 = new Vector2[3], // v2s0: Vector2's 0.
            v2s1 = new Vector2[3]; // v2s1: Vector2's 1.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

        private static GameObject bagButton, checkmarkButton, closeButton, dicesGameObject, keyRawImage, paintbrushButton, productText, shieldKeyRawImage, shieldRawImage;

        private static readonly GameObject[] ds = new GameObject[7]; // ds: Dices.

        public static int s = -1; // s: Set.

        // murasanca

        private Vector2[] V2s => !Monetization.IBL||IAP.HR(0) ? v2s0 : v2s1; // V2s: Vector2's.

        // murasanca

        private void Awake()
        {
            bagButton = GameObject.Find("Bag Button");
            checkmarkButton = GameObject.Find("Checkmark Button");
            closeButton = GameObject.Find("Close Button");
            dicesGameObject = GameObject.Find("Dices Game Object");
            keyRawImage = GameObject.Find("Key Raw Image");
            paintbrushButton = GameObject.Find("Paintbrush Button");
            productText = GameObject.Find("Product Text (TMP)");
            shieldKeyRawImage = GameObject.Find("Shield Key Raw Image");
            shieldRawImage = GameObject.Find("Shield Raw Image");

            for (int i = 0; i < rTs.Length; i++)
            {
                v2s0[i] = b + rTs[i].anchoredPosition;
                v2s1[i] = rTs[i].anchoredPosition;
            }

            StartCoroutine(Enumerator());
        }

        private void Start() => Set(s);

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
                    rTs[i].anchoredPosition = V2s[i];

                yield return wFS;
            }
        }

        // murasanca

        public void B() // B: Bag.
        {
            Bag.set = s;
            Scene.Load(1);
        }

        public void Checkmark()
        {
            Preferences.DD = Preferences.D4 = Preferences.D6 = Preferences.D8 = Preferences.D10 = Preferences.D12 = Preferences.D20 = s;
            Close();
        }

        public void Close()
        {
            checkmarkButton.SetActive(false);
            closeButton.SetActive(false);
        }

        public void Load(int scene) => Scene.Load(scene);

        public void Paintbrush()
        {
            checkmarkButton.SetActive(!checkmarkButton.activeSelf);
            closeButton.SetActive(!closeButton.activeSelf);
        }

        public void Reload() => PlayerPrefs.DeleteAll();

        public void Set() => s = -1;

        public void Slide(float s) // s: Sign.
        {
            if (s is 1)
                if (Inventory.s is not 22)
                    ++Inventory.s;
                else
                    Inventory.s = -1;
            else // sign is -1.
                if (Inventory.s is not -1)
                --Inventory.s;
            else
                Inventory.s = 22;

            Set(Inventory.s);
        }

        private static void Instantiate()
        {
            if (Preferences.Poly is 1)
            {
                ds[0] = Instantiate(Menu.dDHP[s], Menu.V3s[0], Quaternion.identity, dicesGameObject.transform);
                ds[1] = Instantiate(Menu.d4HP[s], Menu.V3s[1], Quaternion.identity, dicesGameObject.transform);
                ds[2] = Instantiate(Menu.d6HP[s], Menu.V3s[2], Quaternion.identity, dicesGameObject.transform);
                ds[3] = Instantiate(Menu.d8HP[s], Menu.V3s[3], Quaternion.identity, dicesGameObject.transform);
                ds[4] = Instantiate(Menu.d10HP[s], Menu.V3s[4], Quaternion.identity, dicesGameObject.transform);
                ds[5] = Instantiate(Menu.d12HP[s], Menu.V3s[5], Quaternion.identity, dicesGameObject.transform);
                ds[6] = Instantiate(Menu.d20HP[s], Menu.V3s[6], Quaternion.identity, dicesGameObject.transform);
            }
            else
            {
                ds[0] = Instantiate(Menu.dDLP[s], Menu.V3s[0], Quaternion.identity, dicesGameObject.transform);
                ds[1] = Instantiate(Menu.d4LP[s], Menu.V3s[1], Quaternion.identity, dicesGameObject.transform);
                ds[2] = Instantiate(Menu.d6LP[s], Menu.V3s[2], Quaternion.identity, dicesGameObject.transform);
                ds[3] = Instantiate(Menu.d8LP[s], Menu.V3s[3], Quaternion.identity, dicesGameObject.transform);
                ds[4] = Instantiate(Menu.d10LP[s], Menu.V3s[4], Quaternion.identity, dicesGameObject.transform);
                ds[5] = Instantiate(Menu.d12LP[s], Menu.V3s[5], Quaternion.identity, dicesGameObject.transform);
                ds[6] = Instantiate(Menu.d20LP[s], Menu.V3s[6], Quaternion.identity, dicesGameObject.transform);
            }
        }

        public static void Set(int s) // s: Set.
        {
            checkmarkButton.SetActive(false);
            closeButton.SetActive(false);

            foreach (GameObject dice in ds)
                Destroy(dice);

            switch (s)
            {
                case -1: // Shield
                    paintbrushButton.SetActive(false);
                    productText.GetComponent<TextMeshProUGUI>().text = "0";
                    productText.SetActive(true);
                    shieldRawImage.SetActive(true);

                    if (IAP.HR(0))
                    {
                        bagButton.SetActive(false);
                        keyRawImage.SetActive(true);
                        shieldKeyRawImage.SetActive(true);
                    }
                    else
                    {
                        bagButton.SetActive(true);
                        keyRawImage.SetActive(false);
                        shieldKeyRawImage.SetActive(false);
                    }
                    break;
                case 0:
                    bagButton.SetActive(false);
                    keyRawImage.SetActive(true);
                    paintbrushButton.SetActive(true);
                    productText.GetComponent<TextMeshProUGUI>().text = Bag.products[0];
                    productText.SetActive(false);
                    shieldKeyRawImage.SetActive(false);
                    shieldRawImage.SetActive(false);

                    Instantiate();
                    break;
                default:
                    shieldKeyRawImage.SetActive(false);
                    shieldRawImage.SetActive(false);

                    if (IAP.HR(s))
                    {
                        bagButton.SetActive(false);
                        keyRawImage.SetActive(true);
                        paintbrushButton.SetActive(true);
                        productText.SetActive(false);
                    }
                    else
                    {
                        bagButton.SetActive(true);
                        keyRawImage.SetActive(false);
                        paintbrushButton.SetActive(false);
                        productText.GetComponent<TextMeshProUGUI>().text = Bag.products[s];
                        productText.SetActive(true);
                    }

                    Instantiate();
                    break;
            }
        }
    }
}

// murasanca