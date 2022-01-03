// murasanca

using UnityEngine;

namespace murasanca
{
    public class Music : MonoBehaviour
    {
        public static Music music;

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
            GetComponent<AudioSource>().pitch = Preferences.MPV;
            GetComponent<AudioSource>().volume=Preferences.MVV;
        }
    }
}

// murasanca