// murasanca

using TMPro;
using UnityEngine;

namespace murasanca
{
    public class Inventory : MonoBehaviour
    {
        private float x;

        private Touch touch;

        private static GameObject bagButton, checkmarkButton, closeButton, dicesGameObject, inventoryButton, keyRawImage, menuButton, paintbrushButton, productText, reloadButton, shieldKeyRawImage, shieldRawImage;

        private readonly static Vector2
            inventoryButton0 = new(0, -64), inventoryButton1 = new(0, -154),
            menuButton0 = new(64, -64), menuButton1 = new(64, -154),
            reloadButton0 = new(-64, -64), reloadButton1 = new(-64, -154);

        private static readonly GameObject[] dices = new GameObject[7];

        public static int set = -1;

        private void Awake()
        {
            bagButton = GameObject.Find("Bag Button");
            checkmarkButton = GameObject.Find("Checkmark Button");
            closeButton = GameObject.Find("Close Button");
            dicesGameObject = GameObject.Find("Dices Game Object");
            inventoryButton = GameObject.Find("Inventory Button");
            keyRawImage = GameObject.Find("Key Raw Image");
            menuButton = GameObject.Find("Menu Button");
            paintbrushButton = GameObject.Find("Paintbrush Button");
            productText = GameObject.Find("Product Text (TMP)");
            reloadButton = GameObject.Find("Reload Button (Legacy)");
            shieldKeyRawImage = GameObject.Find("Shield Key Raw Image");
            shieldRawImage = GameObject.Find("Shield Raw Image");

            StartCoroutine(Enumerator());
        }

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                if (!Monetization.IsInitialized || IAP.HasReceipt(0))
                {
                    inventoryButton.GetComponent<RectTransform>().anchoredPosition = inventoryButton0;
                    menuButton.GetComponent<RectTransform>().anchoredPosition = menuButton0;
                    reloadButton.GetComponent<RectTransform>().anchoredPosition = reloadButton0;
                }
                else
                {
                    inventoryButton.GetComponent<RectTransform>().anchoredPosition = inventoryButton1;
                    menuButton.GetComponent<RectTransform>().anchoredPosition = menuButton1;
                    reloadButton.GetComponent<RectTransform>().anchoredPosition = reloadButton1;
                }
                yield return Menu.waitForSeconds;
            }
        }

        private void Start() => Set(set);

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                if (bagButton.activeSelf)
                    Scene.Load(0);
                else
                    Close();

            if(Input.touchCount is not 0)
            {
                touch=Input.GetTouch(0);
                if (touch.phase is TouchPhase.Began)
                    x = touch.position.x;
                else if (256 < Mathf.Abs(x - touch.position.x) && touch.phase is TouchPhase.Ended)
                    Slide(Mathf.Sign(x - touch.position.x));
            }
        }

        private static void Instantiate()
        {
            if(Preferences.Poly is 1)
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

        public void BagB() // B: Button.
        {
            Bag.set = set;
            Scene.Load(1);
        }

        public void Checkmark()
        {
            Preferences.DD = Preferences.D4 = Preferences.D6 = Preferences.D8 = Preferences.D10 = Preferences.D12 = Preferences.D20 = set;
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

        public void Set()=> set = -1;

        public static void Set(int set)
        {
            foreach (GameObject dice in dices)
                Destroy(dice);

            switch (set)
            {
                case -1: // Shield
                    if (IAP.HasReceipt(0))
                    {
                        bagButton.SetActive(false);
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        keyRawImage.SetActive(true);
                        paintbrushButton.SetActive(false);
                        productText.GetComponent<TextMeshProUGUI>().text = "SHIELD";
                        productText.SetActive(true);
                        shieldKeyRawImage.SetActive(true);
                        shieldRawImage.SetActive(true);
                    }
                    else
                    {
                        bagButton.SetActive(true);
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        keyRawImage.SetActive(false);
                        paintbrushButton.SetActive(false);
                        productText.GetComponent<TextMeshProUGUI>().text = "SHIELD";
                        productText.SetActive(true);
                        shieldKeyRawImage.SetActive(false);
                        shieldRawImage.SetActive(true);
                    }
                    break;
                case 0: // 0
                    bagButton.SetActive(false);
                    checkmarkButton.SetActive(false);
                    closeButton.SetActive(false);
                    keyRawImage.SetActive(true);
                    paintbrushButton.SetActive(true);
                    productText.GetComponent<TextMeshProUGUI>().text = Bag.products[0];
                    productText.SetActive(false);
                    shieldKeyRawImage.SetActive(false);
                    shieldRawImage.SetActive(false);

                    Instantiate();
                    break;
                default:
                    if (IAP.HasReceipt(set))
                    {
                        bagButton.SetActive(false);
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        keyRawImage.SetActive(true);
                        paintbrushButton.SetActive(true);
                        productText.GetComponent<TextMeshProUGUI>().text = Bag.products[set];
                        productText.SetActive(false);
                        shieldKeyRawImage.SetActive(false);
                        shieldRawImage.SetActive(false);
                    }
                    else
                    {
                        bagButton.SetActive(true);
                        checkmarkButton.SetActive(false);
                        closeButton.SetActive(false);
                        keyRawImage.SetActive(false);
                        paintbrushButton.SetActive(false);
                        productText.GetComponent<TextMeshProUGUI>().text = Bag.products[set];
                        productText.SetActive(true);
                        shieldKeyRawImage.SetActive(false);
                        shieldRawImage.SetActive(false);
                    }

                    Instantiate();
                    break;
            }
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
    }
}

// murasanca