// murasanca

using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace murasanca
{
    public class IAP : MonoBehaviour, IStoreListener
    {
        private static IExtensionProvider extensionProvider;

        private static IStoreController storeController;

        public static IAP iap;

        public readonly static string[] nonConsumables = new string[23]
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

        private void Awake()
        {
            if (iap is null)
                iap = this;
            else if (iap != this)
                Destroy(gameObject);
            DontDestroyOnLoad(iap);

            if (storeController is null)
                InitializePurchasing();
        }

        public static bool HasReceipt(int p) // p: Product.
        {
            if (IsInitialized())
                return storeController.products.WithID(nonConsumables[p]).hasReceipt;
            else
                return false;
        }

        public void InitializePurchasing()
        {
            if (IsInitialized())
                return;

            ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (string nonConsumable in nonConsumables)
                configurationBuilder.AddProduct(nonConsumable, ProductType.Consumable);

            UnityPurchasing.Initialize(this, configurationBuilder);
        }

        private static bool IsInitialized() => extensionProvider is not null && storeController is not null;

        public static void BuyNonConsumable(int p) // p: Product.
        {
            if (p is not -1)
                BuyProductID(nonConsumables[p]);
            else
                BuyProductID(nonConsumables[0]);
        }

        private static void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = storeController.products.WithID(productId);

                if (product is not null && product.availableToPurchase)
                    storeController.InitiatePurchase(product);
                else
                    Handheld.Vibrate();
            }
            else
               Handheld.Vibrate();
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            extensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error) => Handheld.Vibrate();

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (String.Equals(args.purchasedProduct.definition.id, nonConsumables[0], StringComparison.Ordinal))
            {
                Bag.Set(-1);
                Monetization.Hide();

                return PurchaseProcessingResult.Complete;
            }

            for (int i = 1; i < nonConsumables.Length; i++)
                if (String.Equals(args.purchasedProduct.definition.id, nonConsumables[i], StringComparison.Ordinal))
                {
                    Bag.Set();
                    return PurchaseProcessingResult.Complete;
                }

            Handheld.Vibrate();
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}

// murasanca