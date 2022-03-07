// Murat Sancak

using UnityEngine;

public class Close:MonoBehaviour
{
    [SerializeField]
    private RectTransform uP; // uP: Upper Panel.

    private readonly WaitForSeconds wFS = Menu.wFS; // wFS: Wait For Seconds.

    // Murat Sancak

    private void Awake() => StartCoroutine(Enumerator());

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

    public void Load(int s) => Scene.Load(s); // s: Scene.

    public void Quit() =>
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif

    public void Reload() => PlayerPrefs.DeleteAll();
}


// Murat Sancak