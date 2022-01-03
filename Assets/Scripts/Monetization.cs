// murasanca

using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace murasanca
{
    public class Monetization : MonoBehaviour, IUnityAdsListener
    {
        private const bool isTest = true;

        private static string b; // Button

        public static Monetization monetization;

#if UNITY_IOS
    private string
        banner = "Banner_iOS",
        // interstitial = "Interstitial_iOS",
        rewarded = "Rewarded_iOS",
        game = "4474157";
#else // UNITY_ANDROID
        private const string
            banner = "Banner_Android",
            // interstitial = "Interstitial_Android",
            rewarded = "Rewarded_Android",
            game = "4474156";
#endif

        public static bool IsInitialized => Advertisement.isInitialized;

        public static bool IsRewardedReady => Advertisement.IsReady(rewarded);

        private void Awake()
        {
            if (monetization is null)
                monetization = this;
            else if (monetization != this)
                Destroy(gameObject);
            DontDestroyOnLoad(monetization);
        }

        private IEnumerator Banner()
        {
            while (!Advertisement.isInitialized)
                yield return new WaitForSeconds(1);

            if (IAP.HasReceipt(0))
                Advertisement.Banner.Hide();
            else
                Advertisement.Banner.Show(banner);
        }

        private void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(game, isTest);
            Advertisement.Initialize(game, isTest);
            Advertisement.Initialize(game, isTest);

            StartCoroutine(Banner());
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        }

        public static void Hide() => Advertisement.Banner.Hide();

        public static void Interstitial()
        {
            if (!IAP.HasReceipt(0) && Advertisement.IsReady())
                Advertisement.Show();
        }

        public void OnDestroy() => Advertisement.RemoveListener(this);

        public void OnUnityAdsDidError(string message)
        {
            Handheld.Vibrate();

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidFinish(string placement, ShowResult showResult)
        {
            if(placement is rewarded)
            {
                if (showResult is ShowResult.Finished)
                    Three.Reward(b);
                else // showResult is ShowResult.Failed or ShowResult.Skipped.
                    Handheld.Vibrate();
            }

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidStart(string placement)
        {
            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsReady(string placement)
        {
            // throw new System.NotImplementedException();
        }

        public static void Rewarded(string button)
        {
            if (!Advertisement.IsReady(rewarded) || IAP.HasReceipt(0))
                Three.Reward(b = button);
            else if (Advertisement.IsReady(rewarded))
            {
                b = button;
                Advertisement.Show(rewarded);
            }
        }
    }
}

// murasanca