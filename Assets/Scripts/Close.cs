// murasanca

using UnityEngine;

namespace murasanca
{
    public class Close : MonoBehaviour
    {
        [SerializeField]
        private RectTransform[] rTs = new RectTransform[3]; // rTs: Rect Transforms.

        private readonly Vector2 b = Menu.b;

        private readonly Vector2[]
            v2s0 = new Vector2[3], // v2s0: Vector2's 0.
            v2s1 = new Vector2[3]; // v2s1: Vector2's 1.

        private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

        // murasanca

        private Vector2[] Vector2s
        {
            get
            {
                if (!Monetization.IBL || IAP.HR(0))
                    return v2s0;
                else
                    return v2s1;
            }
        }

        // murasanca

        private void Awake()
        {
            for (int i = 0; i < rTs.Length; i++)
            {
                v2s0[i] = b + rTs[i].anchoredPosition;
                v2s1[i] = rTs[i].anchoredPosition;
            }

            StartCoroutine(Enumerator());
        }

        // murasanca

        private System.Collections.IEnumerator Enumerator()
        {
            while (true)
            {
                for (int i = 0; i < rTs.Length; i++)
                    rTs[i].anchoredPosition = Vector2s[i];

                yield return wFS;
            }
        }

        // murasanca

        public void Load(int s) => Scene.Load(s); // s: Scene.

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