// Murat Sancak

using UnityEngine;

namespace murasanca
{
    public class Sound : MonoBehaviour
    {
        public static AudioSource aS; // aS: Audio Source.

        public static Sound s; // s: Sound.

        // Murat Sancak

        public static bool IP => aS.isPlaying; // IP: Is Playing.

        public static float P // P: Pitch.
        {
            get => aS.pitch;
            set => aS.pitch = value;
        }

        public static float V // V: Volume.
        {
            get => aS.volume;
            set => aS.volume = value;
        }

        // Murat Sancak

        private void Awake()
        {
            if (s is null)
                s = this;
            else if (s != this)
                Destroy(gameObject);
            DontDestroyOnLoad(s);

            aS = s.GetComponent<AudioSource>();
        }

        private void Start()
        {
            aS.pitch = Preferences.P;
            aS.volume = Preferences.V;
        }
    }
}

// Murat Sancak