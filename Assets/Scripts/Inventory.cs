// murasanca

using TMPro;
using UnityEngine;

namespace murasanca
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] gameObjects = new GameObject[3];

        private float x;

        private Touch touch;

        private readonly Vector2 banner = Menu.banner;

        private readonly Vector2[]
            vector2s0 = new Vector2[3], // Shield.
            vector2s1 = new Vector2[3]; // Advertisement.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

        private static GameObject bagButton, checkmarkButton, closeButton, dicesGameObject, keyRawImage, paintbrushButton, productText, shieldKeyRawImage, shieldRawImage;

        private static readonly GameObject[] dices = new GameObject[7];

        public static int set = -1;

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
            bagButton = GameObject.Find("Bag Button");
            checkmarkButton = GameObject.Find("Checkmark Button");
            closeButton = GameObject.Find("Close Button");
            dicesGameObject = GameObject.Find("Dices Game Object");
            keyRawImage = GameObject.Find("Key Raw Image");
            paintbrushButton = GameObject.Find("Paintbrush Button");
            productText = GameObject.Find("Product Text (TMP)");
            shieldKeyRawImage = GameObject.Find("Shield Key Raw Image");
            shieldRawImage = GameObject.Find("Shield Raw Image");

            for (int i = 0; i < gameObjects.Length; i++)
            {
                vector2s0[i] = banner + gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
                vector2s1[i] = gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
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
                for (int i = 0; i < gameObjects.Length; i++)
                    gameObjects[i].GetComponent<RectTransform>().anchoredPosition = Vector2s[i];

                yield return wFS;
            }
        }

        // murasanca

        public void B() // B: Bag.
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

        public void Reload() => PlayerPrefs.DeleteAll();

        public void Set() => set = -1;

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

        public static void Set(int set)
        {
            checkmarkButton.SetActive(false);
            closeButton.SetActive(false);

            foreach (GameObject dice in dices)
                Destroy(dice);

            switch (set)
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

                    if (IAP.HR(set))
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
                        productText.GetComponent<TextMeshProUGUI>().text = Bag.products[set];
                        productText.SetActive(true);
                    }

                    Instantiate();
                    break;
            }
        }
    }
}

// murasanca