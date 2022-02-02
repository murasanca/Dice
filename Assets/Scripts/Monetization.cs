// murasanca

using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace murasanca
{
    public class Monetization : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
#if UNITY_IOS
        private string
            banner = "Banner_iOS",
            game = "4474157",
            interstitial = "Interstitial_iOS",
            rewarded = "Rewarded_iOS";
            
#else // UNITY_ANDROID
        private const string
            banner = "Banner_Android",
            game = "4474156",
            interstitial = "Interstitial_Android",
            rewarded = "Rewarded_Android";
#endif

        private bool
            // iBS = false, // iBS: Is Banner Showing.
            iIS = false, // iIS: Is Interstitial Showing.
            iRS = false; // iRS: Is Rewarded Showing.

        private const bool test = true;

        private readonly WaitForSeconds wFS = Menu.wFS;

        private static bool
            // iBL = false, // iBR: is Banner Loaded.
            iIL = false, // iIR: is Interstitial Loaded.
            iRL = false; // iIR: is Rewarded Loaded.

        private static int
            b = -1, // b: Button.
            s = -1; // s: Scene.

        public static Monetization monetization;

        // murasanca

        public static bool IBL => Advertisement.Banner.isLoaded; // IBR: Is Banner Loaded.
        public static bool II => Advertisement.isInitialized; // II: Is Initialized.
        public static bool IIL => iIL; // IIR: Is Interstitial Loaded.
        public static bool IRL => iRL; // IRR: Is Rewarded Loaded.

        // murasanca

        private void Awake()
        {
            if (monetization is null)
                monetization = this;
            else if (monetization != this)
                Destroy(gameObject);
            DontDestroyOnLoad(monetization);
        }

        private void Start() => StartCoroutine(Initialize());

        // murasanca

        private IEnumerator Initialize()
        {
            while (true)
            {
                if (!IAP.HR(0))
                    if (!II)
                        Advertisement.Initialize(game, test, monetization);
                    else
                    {
                        Advertisement.Banner.Load(banner);
                        Advertisement.Banner.Show(banner);

                        if (!IIL)
                            Advertisement.Load(interstitial, monetization);

                        if (!IRL)
                            Advertisement.Load(rewarded, monetization);
                    }

                yield return wFS;
            }
        }

        // murasanca

        public static void Hide() => Advertisement.Banner.Hide();

        public static void Interstitial(int scene)
        {
            if (!iIL || IAP.HR(0))
                Scene.Reward(s = scene);
            else
            {
                s = scene;

                Advertisement.Show(interstitial, monetization);
            }
        }

        public static void Rewarded(int button)
        {
            if (!iRL || IAP.HR(0))
                Play.Reward(b = button);
            else
            {
                b = button;

                Advertisement.Show(rewarded, monetization);
            }
        }

        // murasanca

        public void OnInitializationComplete()
        {
            // throw new System.NotImplementedException();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Advertisement.Initialize(game, test, monetization);

            // Handheld.Vibrate();

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsAdLoaded(string advertisement)
        {
            if (advertisement is interstitial)
                iIL = true;
            else if (advertisement is rewarded)
                iRL = true;

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsFailedToLoad(string advertisement, UnityAdsLoadError error, string message)
        {
            switch (advertisement)
            {
                case banner:
                    Advertisement.Banner.Load();
                    break;
                case interstitial:
                    iIL = false;

                    Advertisement.Load(interstitial, monetization);
                    break;
                case rewarded:
                    iRL = false;

                    Advertisement.Load(rewarded, monetization);
                    break;
            }

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string advertisement, UnityAdsShowCompletionState showCompletionState)
        {
            if (iIS && advertisement is interstitial)
            {
                iIS = false;

                Scene.Reward(s);
            }
            else if (iRS && advertisement is rewarded)
            {
                if (showCompletionState is UnityAdsShowCompletionState.COMPLETED)
                    Play.Reward(b);
                else // showCompletionState is UnityAdsShowCompletionState.SKIPPED || showCompletionState is UnityAdsShowCompletionState.UNKNOWN.
                    Handheld.Vibrate();

                iRS = false;
            }

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowFailure(string advertisement, UnityAdsShowError error, string message)
        {
            switch (advertisement)
            {
                // case banner:
                //     iBS= false;
                //     break;
                case interstitial:
                    iIS = false;

                    Handheld.Vibrate();
                    break;
                case rewarded:
                    iRS = false;

                    Handheld.Vibrate();
                    break;
                default:
                    break;
            }

            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowStart(string advertisement)
        {
            switch (advertisement)
            {
                // case banner:
                //     iBS = true;
                //     break;
                case interstitial:
                    iIS = true;
                    break;
                case rewarded:
                    iRS = true;
                    break;
                default:
                    break;
            }

            // throw new System.NotImplementedException();
        }
    }
}

// murasanca