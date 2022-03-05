// murasanca

using UnityEngine;
using UnityEngine.SceneManagement;

namespace murasanca
{
    public class Scene : MonoBehaviour
    {
        private readonly static WaitForSeconds // wFS: Wait For Seconds.
            wFS64 = new(16 / 15), // wFS64 = new(64 / 60).
            wFS128 = new(32 / 15); // wFS128 = new(128 / 60).

        private static Animator a; // a: Animator.

        public static Scene s; // s: Scene.

        // murasanca

        private void Awake()
        {
            if (s is null)
                s = this;
            else if (s != this)
                Destroy(gameObject);
            DontDestroyOnLoad(s);

            a = s.gameObject.GetComponent<Animator>();
        }

        private void Start() => StartCoroutine(Single());

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                if (SceneManager.GetActiveScene().buildIndex is 0)
                    Load(2);
                else
                    Load(0);
        }

        // murasanca

        private static System.Collections.IEnumerator Double(int scene)
        {
            if (SceneManager.GetActiveScene().buildIndex is 4) // Play.
            {
                Camera.main.GetComponent<Jump>().enabled = false;
                Camera.main.GetComponent<Roll>().enabled = false;
            }

            a.SetTrigger("Scene Canvas 0");
            a.Play("Scene Canvas 0");
            
            yield return wFS128;

            a.ResetTrigger("Scene Canvas 0");

            SceneManager.LoadScene(scene);

            a.SetTrigger("Scene Canvas 1");
            a.Play("Scene Canvas 1");

            yield return wFS64;

            a.ResetTrigger("Scene Canvas 1");
        }

        private static System.Collections.IEnumerator Single()
        {
            a.SetTrigger("Scene Canvas 1");
            a.Play("Scene Canvas 1");

            yield return wFS64;

            a.ResetTrigger("Scene Canvas 1");
        }

        // murasanca

        public static void Load(int s) => Monetization.Interstitial(s); // s: Scene.

        public static void Reward(int s) => Scene.s.StartCoroutine(Double(s)); // s: Scene.
    }
}

// murasanca