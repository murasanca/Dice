// murasanca

using UnityEngine;

namespace murasanca
{
    public class Music : MonoBehaviour
    {
        public static Music music;

        // murasanca

        private void Awake()
        {
            if (music is null)
                music = this;
            else if (music != this)
                Destroy(gameObject);
            DontDestroyOnLoad(music);
        }

        private void Start()
        {
            GetComponent<AudioSource>().volume = Preferences.V;
            Time.timeScale = Mathf.Abs(GetComponent<AudioSource>().pitch = Preferences.P);
        }
    }
}

// murasanca