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

        private static Product product;

        public static IAP iap; // iap: In-App Purchase.

        public readonly static string[] products = new string[23]
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

        private static Product[] Product
        {
            get
            {
                if (II)
                    return sC.products.all;
                else
                    return null;
            }
        }

        // Murat Sancak

        private void Awake()
        {
            if (iap is null)
                iap = this;
            else if (iap != this)
                Destroy(gameObject);
            DontDestroyOnLoad(iap);

            if (sC is null)
                Initialize();
        }

        // Murat Sancak

        private static void Checkmark(string p) // p: Product.
        {
            if (II)
            {
                product = sC.products.WithID(p);

                if (product is not null && product.availableToPurchase)
                    sC.InitiatePurchase(product);
                else
                    Handheld.Vibrate();
            }
            else
                Handheld.Vibrate();
        }

        public void Initialize()
        {
            if (II)
                return;

            cB = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (string product in products)
                cB.AddProduct(product, ProductType.NonConsumable);
            UnityPurchasing.Initialize(iap, cB);
        }

        public static bool HR(int p) // HR: Has Receipt, p: Product.
        {
            if (II)
                return sC.products.WithID(products[p]).hasReceipt;
            else
                return false;
        }

        public static void Checkmark(int product)
        {
            if (product is not -1)
                Checkmark(products[product]);
            else
                Checkmark(products[0]);
        }

        // Murat Sancak

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (string.Equals(args.purchasedProduct.definition.id, products[0], StringComparison.Ordinal))
            {
                Bag.Set(-1);
                Monetization.Hide();

                return PurchaseProcessingResult.Complete;
            }

            for (int i = 1; i < products.Length; i++)
                if (string.Equals(args.purchasedProduct.definition.id, products[i], StringComparison.Ordinal))
                {
                    Bag.Set();
                    return PurchaseProcessingResult.Complete;
                }

            Handheld.Vibrate();

            return PurchaseProcessingResult.Complete;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            eP = extensions;
            sC = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error) => Handheld.Vibrate();

        public void OnPurchaseFailed(Product p, PurchaseFailureReason pFR) // p: Product, pFR: Purchase Failure Reason.
        {
            Handheld.Vibrate();

            if (SceneManager.GetActiveScene().buildIndex is 2) // Settings.
                Bag.Close();
        }
    }
}

// Murat Sancak