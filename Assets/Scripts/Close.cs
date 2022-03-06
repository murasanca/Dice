// Murat Sancak

using UnityEngine;

public class Close:MonoBehaviour
{
    [SerializeField]
    private RectTransform[] rTs = new RectTransform[3]; // rTs: Rect Transforms.

    private int f; // f: For.

    private readonly Vector2 b = Menu.b; // b: Banner.

    private readonly Vector2[]
        v2s0 = new Vector2[3], // v2s0: Vector2's 0.
        v2s1 = new Vector2[3]; // v2s1: Vector2's 1.

    private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

    // Murat Sancak

    private Vector2[] V2s => !Monetization.IBL||IAP.HR(0) ? v2s0 : v2s1; // V2s: Vector2's.

    // Murat Sancak

    private void Awake()
    {
        for(f=0;f<rTs.Length;f++)
        {
            v2s0[f]=b+rTs[f].anchoredPosition;
            v2s1[f]=rTs[f].anchoredPosition;
        }

        StartCoroutine(Enumerator());
    }

    // Murat Sancak

    private System.Collections.IEnumerator Enumerator()
    {
        while(true)
        {
            for(f=0;f<rTs.Length;f++)
                rTs[f].anchoredPosition=V2s[f];

            yield return wFS;
        }
    }

    // Murat Sancak

    public void Load(int s) => Scene.Load(s); // s: Scene.

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#else // UNITY_ANDROID
            Application.Quit();
#endif
    }

    public void Reload() => PlayerPrefs.DeleteAll();
}


// Murat Sancak