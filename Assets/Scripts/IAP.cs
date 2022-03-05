// Murat Sancak

using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

namespace murasanca
{
    public class IAP : MonoBehaviour, IStoreListener // IAP: In-App Purchase.
    {
        private ConfigurationBuilder cB; // cB: Configuration Builder.

        private static IExtensionProvider eP; // eP: Extension Provider.

        private static IStoreController sC; // sC: Store Controller.

        private static Product p; // p: Product.

        public static IAP iap; // iap: In-App Purchase.

        public readonly static string[] ps = new string[23] // ps: Products.
        {
            "com.murasanca.dice.0",
            "com.murasanca.dice.1",
            "com.murasanca.dice.2",
            "com.murasanca.dice.3",
            "com.murasanca.dice.4",
            "com.murasanca.dice.5",
            "com.murasanca.dice.6",
            "com.murasanca.dice.7",
            "com.murasanca.dice.8",
            "com.murasanca.dice.9",
            "com.murasanca.dice.10",
            "com.murasanca.dice.11",
            "com.murasanca.dice.12",
            "com.murasanca.dice.13",
            "com.murasanca.dice.14",
            "com.murasanca.dice.15",
            "com.murasanca.dice.16",
            "com.murasanca.dice.17",
            "com.murasanca.dice.18",
            "com.murasanca.dice.19",
            "com.murasanca.dice.20",
            "com.murasanca.dice.21",
            "com.murasanca.dice.22"
        };

        // Murat Sancak

        public static bool II => eP is not null && sC is not null; // II: Is Initialized.

        // Murat Sancak

        private void Awake()
        {
            if (iap is null)
                iap = this;
            else if (iap != this)
                Destroy(gameObject);
            DontDestroyOnLoad(iap);
        }

        private void Start() => Initialize();

        // Murat Sancak

        private static void Checkmark(string p) // p: Product.
        {
            if (II)
            {
                IAP.p = sC.products.WithID(p);
                if (IAP.p is not null && IAP.p.availableToPurchase)
                    sC.InitiatePurchase(IAP.p);
                else
                    Handheld.Vibrate();
            }
            else
                Handheld.Vibrate();
        }

        public void Initialize()
        {
            cB = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (string p in ps) // p: Product.
                cB.AddProduct(p, ProductType.NonConsumable);
            UnityPurchasing.Initialize(iap, cB);
        }

        public static bool HR(int p) // HR: Has Receipt, p: Product.
        {
            if (II)
                return sC.products.WithID(ps[p]).hasReceipt;
            else
                return false;
        }

        public static void Checkmark(int p) // p: Product.
        {
            if (p is not -1)
                Checkmark(ps[p]);
            else
                Checkmark(ps[0]);
        }

        // Murat Sancak

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs pEA) // pEA: Purchase Event Args.
        {
            if (string.Equals(pEA.purchasedProduct.definition.id, ps[0], StringComparison.Ordinal))
            {
                if (SceneManager.GetActiveScene().buildIndex is 1) // Bag.
                    Bag.Set(-1);

                Monetization.Hide();

                return PurchaseProcessingResult.Complete;
            }

            for (int i = 1; i < ps.Length; i++)
                if (string.Equals(pEA.purchasedProduct.definition.id, ps[i], StringComparison.Ordinal))
                {
                    if (SceneManager.GetActiveScene().buildIndex is 1) // Bag.
                        Bag.Set();

                    return PurchaseProcessingResult.Complete;
                }

            Handheld.Vibrate();

            return PurchaseProcessingResult.Complete;
        }

        public void OnInitialized(IStoreController sC, IExtensionProvider eP) // sC: Store Controller, eP: Extension Provider.
        {
            IAP.eP = eP;
            IAP.sC = sC;
        }

        public void OnInitializeFailed(InitializationFailureReason iFR) => Initialize(); // iFR: Purchase Failure Reason.

        public void OnPurchaseFailed(Product p, PurchaseFailureReason pFR) // p: Product, pFR: Purchase Failure Reason.
        {
            Handheld.Vibrate();

            if (SceneManager.GetActiveScene().buildIndex is 1) // Bag.
                Bag.Close();
        }
    }
}

// Murat Sancak