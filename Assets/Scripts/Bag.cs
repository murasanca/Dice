// murasanca

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

namespace murasanca
{
    public class Bag : MonoBehaviour
    {
        private float x;

        private Touch touch;

        private static GameObject bagButton, checkmarkButton, closeButton, dicesGameObject, inventoryButton, keyButton, keyRawImage, lockButton, menuButton, productText, reloadButton, shieldRawImage;

        private readonly static GameObject[] dices = new GameObject[7];

        public readonly static string[] products = new string[23]
        {
            "0","1","2","3","4","5","6","7","8","9", // Data: 1.
            "10","11","12","13","14","15","16","17","18","19", // Data: 2.
            "20","21","22" // Data: 3.
        };

        private readonly static Vector2
            bagButton0 = new Vector2(0, -64), bagButton1 = new Vector2(0, -154),
            menuButton0 = new Vector2(64, -64), menuButton1 = new Vector2(64, -154),
            reloadButton0 = new Vector2(-64, -64), reloadButton1 = new Vector2(-64, -154);

        public static int set = -1;

        private void Awake()
        {
            bagButton = GameObject.Find("Bag Button");
            checkmarkButton = GameObject.Find("Checkmark Button (Legacy)");
            closeButton = GameObject.Find("Close Button");
            dicesGameObject = GameObject.Find("Dices Game Object");
            inventoryButton = GameObject.Find("Inventory Button");
            keyButton = GameObject.Find("Key Button");
            keyRawImage = GameObject.Find("Key Raw Image");
            lockButton = GameObject.Find("Lock Button");
            menuButton = GameObject.Find("Menu Button");
            productText = GameObject.Find("Product Text (TMP)");
            reloadButton = GameObject.Find("Reload Button (Legacy)");
            shieldRawImage = GameObject.Find("Shield Raw Image");

            StartCoroutine(Enumerator());
        }

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                if (!Monetization.IsInitialized || IAP.HasReceipt(0))
                {
                    bagButton.GetComponent<RectTransform>().anchoredPosition = bagButton0;
                    menuButton.GetComponent<RectTransform>().anchoredPosition = menuButton0;
                    reloadButton.GetComponent<RectTransform>().anchoredPosition = reloadButton0;
                }
                else
                {
                    bagButton.GetComponent<RectTransform>().anchoredPosition = bagButton1;
                    menuButton.GetComponent<RectTransform>().anchoredPosition = menuButton1;
                    reloadButton.GetComponent<RectTransform>().anchoredPosition = reloadButton1;
                }
                yield return Menu.waitForSeconds;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                if (lockButton.activeSelf)
                    Scene.Load(0);
                else
                    Close();

            if (Input.touchCount is not 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase is TouchPhase.Began)
                    x = touch.position.x;
                else if (256 < Mathf.Abs(x - touch.position.x) && touch.phase is TouchPhase.Ended)
                    Slide(Mathf.Sign(x - touch.position.x));
            }
        }

        private static void Instantiate()
        {
            if (Preferences.Poly is 1)
            {
                dices[0] = Instantiate(Menu.dDHP[set], Menu.Vector3s[0], Quaternion.identity, dicesGameObject.transform);
                dices[1] = Instantiate(Menu.d4HP[set], Menu.Vector3s[1], Quaternion.identity, dicesGameObject.transform);
                dices[2] = Instantiate(Menu.d6HP[set], Menu.Vector3s[2], Quaternion.identity, dicesGameObject.transform);
                dices[3] = Instantiate(Menu.d8HP[set], Menu.Vector3s[3], Quaternion.identity, dicesGameObject.transform);
                dices[4] = Instantiate(Menu.d10HP[set], Menu.Vector3s[4], Quaternion.identity, dicesGameObject.transform);
                dices[5] = Instantiate(Menu.d12HP[set], Menu.Vector3s[5], Quaternion.identity, dicesGameObject.transform);
                dices[6] = Instantiate(Menu.d20HP[set], Menu.Vector3s[6], Quaternion.identity, dicesGameObject.transform);
            }
            else
            {
                dices[0] = Instantiate(Menu.dDLP[set], Menu.Vector3s[0], Quaternion.identity, dicesGameObject.transform);
                dices[1] = Instantiate(Menu.d4LP[set], Menu.Vector3s[1], Quaternion.identity, dicesGameObject.transform);
                dices[2] = Instantiate(Menu.d6LP[set], Menu.Vector3s[2], Quaternion.identity, dicesGameObject.transform);
                dices[3] = Instantiate(Menu.d8LP[set], Menu.Vector3s[3], Quaternion.identity, dicesGameObject.transform);
                dices[4] = Instantiate(Menu.d10LP[set], Menu.Vector3s[4], Quaternion.identity, dicesGameObject.transform);
                dices[5] = Instantiate(Menu.d12LP[set], Menu.Vector3s[5], Quaternion.identity, dicesGameObject.transform);
                dices[6] = Instantiate(Menu.d20LP[set], Menu.Vector3s[6], Quaternion.identity, dicesGameObject.transform);
            }
        }

        private void Start() => Set(set);

        public void Checkmark() => IAP.BuyNonConsumable(set);

        public void Close()
        {
            checkmarkButton.SetActive(false);
            closeButton.SetActive(false);
            keyButton.SetActive(false);
            lockButton.SetActive(true);
        }

        public void Key() => Set(set);

        public void Lock()
        {
            if (set is not -1)
                checkmarkButton.GetComponent<IAPButton>().productId = IAP.nonConsumables[set];
            else
                checkmarkButton.GetComponent<IAPButton>().productId = IAP.nonConsumables[0];

            checkmarkButton.SetActive(true);
            closeButton.SetActive(true);
            keyButton.GetComponent<Button>().interactable = true;
            keyButton.SetActive(true);
            lockButton.SetActive(false);
        }

        public void Slide(float sign)
        {
            if (sign is 1)
                if (set is not 22)
                    ++set;
                else
                    set = -1;
            else // sign is -1.
                if (set is not -1)
                --set;
            else
                set = 22;

            Set(set);
        }

        public static void Set()
        {
            Preferences.DD = Preferences.D4 = Preferences.D6 = Preferences.D8 = Preferences.D10 = Preferences.D12 = Preferences.D20 = set;
            Set(set);
        }

        public static void Set(int set)
        {
            foreach (GameObject dice in dices)
                Destroy(dice);

            switch (set)
            {
                case -1: // Shield
                    if (IAP.HasReceipt(0))
                    {
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        inventoryButton.SetActive(false);
                        keyButton.GetComponent<Button>().interactable = false;
                        keyButton.SetActive(true);
                        keyRawImage.SetActive(false);
                        lockButton.SetActive(false);
                        productText.GetComponent<TextMeshProUGUI>().text = "SHIELD";
                        productText.SetActive(true);
                        shieldRawImage.SetActive(true);
                    }
                    else
                    {
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        inventoryButton.SetActive(false);
                        keyButton.SetActive(false);
                        keyRawImage.SetActive(false);
                        lockButton.SetActive(true);
                        productText.GetComponent<TextMeshProUGUI>().text = "SHIELD";
                        productText.SetActive(true);
                        shieldRawImage.SetActive(true);
                    }
                    break;
                case 0: // 0
                    checkmarkButton.SetActive(false);
                    closeButton.SetActive(false);
                    inventoryButton.SetActive(true);
                    keyButton.SetActive(false);
                    keyRawImage.SetActive(true);
                    lockButton.SetActive(false);
                    productText.SetActive(false);
                    shieldRawImage.SetActive(false);

                    Instantiate();
                    break;
                default:
                    if (IAP.HasReceipt(set))
                    {
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        inventoryButton.SetActive(true);
                        keyButton.SetActive(false);
                        keyRawImage.SetActive(true);
                        lockButton.SetActive(false);
                        productText.SetActive(false);
                        shieldRawImage.SetActive(false);
                    }
                    else
                    {
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        inventoryButton.SetActive(false);
                        keyButton.SetActive(false);
                        keyRawImage.SetActive(false);
                        lockButton.SetActive(true);
                        productText.GetComponent<TextMeshProUGUI>().text = products[set];
                        productText.SetActive(true);
                        shieldRawImage.SetActive(false);
                    }

                    Instantiate();
                    break;
            }
        }

        public void BagB() // B: Button.
        {
            set = -1;
            Scene.Load(1);
        }

        public void InventoryB() // B: Button.
        {
            Inventory.set = set;
            Scene.Load(3);
        }

        public void Load(int scene) => Scene.Load(scene);
    }
}

// murasanca