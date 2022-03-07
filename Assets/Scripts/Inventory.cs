// Murat Sancak

using TMPro;
using UnityEngine;

namespace murasanca
{
    public class Inventory:MonoBehaviour
    {
        [SerializeField]
        private RectTransform uP; // uP: Upper Panel.

        private float x;

        private Touch t; // t: Touch.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

        private static GameObject
            bB, // bB: Bag Button.
            cB, // cB: Close Button.
            cBL, // cBL: Checkmark Button (Legacy).
            dsGO, // dsGO: Dices Game Object.
            kRI, // kRI: Key Raw Image.
            pB, // pB: Paintbrush Button.
            pTTMP, // pTTMP: Product Text (TMP).
            sKRI, // sKRI: Shield Key Raw Image.
            sRI; // sRI: Shield Raw Image.

        private static readonly GameObject[] ds = new GameObject[7]; // ds: Dices.

        public static int s = -1; // s: Set.

        // Murat Sancak

        private void Awake()
        {
            bB=GameObject.Find("Bag Button");
            cB=GameObject.Find("Close Button");
            cBL=GameObject.Find("Checkmark Button (Legacy)");
            dsGO=GameObject.Find("Dices Game Object");
            kRI=GameObject.Find("Key Raw Image");
            pB=GameObject.Find("Paintbrush Button");
            pTTMP=GameObject.Find("Product Text (TMP)");
            sKRI=GameObject.Find("Shield Key Raw Image");
            sRI=GameObject.Find("Shield Raw Image");

            StartCoroutine(Enumerator());
        }

        private void Start() => Set(s);

        private void Update()
        {
            if(Input.touchCount is not 0)
            {
                t=Input.GetTouch(0);
                if(t.phase is TouchPhase.Began)
                    x=t.position.x;
                else if(256<Mathf.Abs(x-t.position.x)&&t.phase is TouchPhase.Ended)
                    Slide(Mathf.Sign(x-t.position.x));
            }
        }

        // Murat Sancak

        private System.Collections.IEnumerator Enumerator()
        {
            while(true)
            {
                if(!Monetization.IBL||IAP.HR(0))
                    uP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,256);
                else
                    uP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,346);

                yield return wFS;
            }
        }

        // Murat Sancak

        public void B() // B: Bag.
        {
            Bag.s=s;
            Scene.Load(1); // Bag.
        }

        public void Checkmark()
        {
            Preferences.DD=Preferences.D4=Preferences.D6=Preferences.D8=Preferences.D10=Preferences.D12=Preferences.D20=s;

            Close();
        }

        public void Close()
        {
            cB.SetActive(false);
            cBL.SetActive(false);
        }

        public void Load(int s) => Scene.Load(s); // s: Scene.

        public void Paintbrush()
        {
            cB.SetActive(!cB.activeSelf);
            cBL.SetActive(!cBL.activeSelf);
        }

        public void Reload()
        {
            Close();

            Preferences.Delete();
        }

        public void Set() => s=-1;

        public void Slide(float s) // s: Sign.
        {
            if(s is 1)
                if(Inventory.s is not 22)
                    ++Inventory.s;
                else
                    Inventory.s=-1;
            else if(Inventory.s is not -1)
                --Inventory.s;
            else
                Inventory.s=22;

            Set(Inventory.s);
        }

        private static void Instantiate()
        {
            if(Preferences.Poly is 1)
            {
                ds[0]=Instantiate(Menu.dDHP[s],Menu.V3s[0],Quaternion.identity,dsGO.transform);
                ds[1]=Instantiate(Menu.d4HP[s],Menu.V3s[1],Quaternion.identity,dsGO.transform);
                ds[2]=Instantiate(Menu.d6HP[s],Menu.V3s[2],Quaternion.identity,dsGO.transform);
                ds[3]=Instantiate(Menu.d8HP[s],Menu.V3s[3],Quaternion.identity,dsGO.transform);
                ds[4]=Instantiate(Menu.d10HP[s],Menu.V3s[4],Quaternion.identity,dsGO.transform);
                ds[5]=Instantiate(Menu.d12HP[s],Menu.V3s[5],Quaternion.identity,dsGO.transform);
                ds[6]=Instantiate(Menu.d20HP[s],Menu.V3s[6],Quaternion.identity,dsGO.transform);
            }
            else
            {
                ds[0]=Instantiate(Menu.dDLP[s],Menu.V3s[0],Quaternion.identity,dsGO.transform);
                ds[1]=Instantiate(Menu.d4LP[s],Menu.V3s[1],Quaternion.identity,dsGO.transform);
                ds[2]=Instantiate(Menu.d6LP[s],Menu.V3s[2],Quaternion.identity,dsGO.transform);
                ds[3]=Instantiate(Menu.d8LP[s],Menu.V3s[3],Quaternion.identity,dsGO.transform);
                ds[4]=Instantiate(Menu.d10LP[s],Menu.V3s[4],Quaternion.identity,dsGO.transform);
                ds[5]=Instantiate(Menu.d12LP[s],Menu.V3s[5],Quaternion.identity,dsGO.transform);
                ds[6]=Instantiate(Menu.d20LP[s],Menu.V3s[6],Quaternion.identity,dsGO.transform);
            }
        }

        public static void Set(int s) // s: Set.
        {
            cB.SetActive(false);
            cBL.SetActive(false);

            foreach(GameObject d in ds) // d: Dice.
                Destroy(d);

            switch(s)
            {
                case -1: // Shield
                    if(IAP.HR(0))
                    {
                        bB.SetActive(false);
                        kRI.SetActive(true);
                        sKRI.SetActive(true);
                    }
                    else
                    {
                        bB.SetActive(true);
                        kRI.SetActive(false);
                        sKRI.SetActive(false);
                    }
                    pB.SetActive(false);
                    pTTMP.GetComponent<TextMeshProUGUI>().text="0";
                    pTTMP.SetActive(true);
                    sRI.SetActive(true);
                    break;
                case 0:
                    bB.SetActive(false);
                    kRI.SetActive(true);
                    pB.SetActive(true);
                    pTTMP.GetComponent<TextMeshProUGUI>().text=Bag.ps[0];
                    pTTMP.SetActive(false);
                    sKRI.SetActive(false);
                    sRI.SetActive(false);

                    Instantiate();
                    break;
                default:
                    if(IAP.HR(s))
                    {
                        bB.SetActive(false);
                        kRI.SetActive(true);
                        pB.SetActive(true);
                        pTTMP.SetActive(false);
                    }
                    else
                    {
                        bB.SetActive(true);
                        kRI.SetActive(false);
                        pB.SetActive(false);
                        pTTMP.GetComponent<TextMeshProUGUI>().text=Bag.ps[s];
                        pTTMP.SetActive(true);
                    }
                    sKRI.SetActive(false);
                    sRI.SetActive(false);

                    Instantiate();
                    break;
            }
        }
    }
}

// Murat Sancak