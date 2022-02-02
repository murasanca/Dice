// murasanca

using UnityEngine;

namespace murasanca
{
    public class Close : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] gameObjects = new GameObject[3];

        private readonly Vector2 banner = Menu.banner;

        private readonly Vector2[]
                    vector2s0 = new Vector2[3], // Shield.
                    vector2s1 = new Vector2[3]; // Advertisement.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

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
            for (int i = 0; i < gameObjects.Length; i++)
            {
                vector2s0[i] = banner + gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
                vector2s1[i] = gameObjects[i].GetComponent<RectTransform>().anchoredPosition;
            }

            StartCoroutine(Enumerator());
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

        public void Load(int scene) => Scene.Load(scene);

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else // UNITY_ANDROID
            Application.Quit();
#endif
        }

        public void Reload() => PlayerPrefs.DeleteAll();
    }
}

// murasanca