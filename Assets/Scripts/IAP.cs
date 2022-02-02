// murasanca

using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace murasanca
{
    public class IAP : MonoBehaviour, IStoreListener
    {
        private static IExtensionProvider eP; // eP: Extension Provider.

        private static IStoreController sC; // sC: Store Controller.

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

        public static IAP iap;

        // murasanca

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

        // murasanca

        public static bool HR(int p) // HR: Has Receipt, p: Product.
        {
            if (Initialized())
                return sC.products.WithID(products[p]).hasReceipt;
            else
                return false;
        }

        public void Initialize()
        {
            if (Initialized())
                return;

            ConfigurationBuilder cB = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance()); // cB: Configuration Builder.
            foreach (string product in products)
                cB.AddProduct(product, ProductType.Consumable);
            UnityPurchasing.Initialize(this, cB);
        }

        private static bool Initialized() => eP is not null && sC is not null;

        public static void Checkmark(int product)
        {
            if (product is not -1)
                Buy(products[product]);
            else
                Buy(products[0]);
        }

        private static void Buy(string p) // p: Product.
        {
            if (Initialized())
            {
                Product product = sC.products.WithID(p);

                if (product is not null && product.availableToPurchase)
                    sC.InitiatePurchase(product);
                else
                    Handheld.Vibrate();
            }
            else
                Handheld.Vibrate();
        }

        // murasanca

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

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => Handheld.Vibrate();
    }
}

// murasanca