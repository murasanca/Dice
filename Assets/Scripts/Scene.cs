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

        private static Animator animator;

        public static Scene scene;

        // murasanca

        private void Awake()
        {
            if (scene is null)
                scene = this;
            else if (scene != this)
                Destroy(gameObject);
            DontDestroyOnLoad(scene);

            animator = scene.gameObject.GetComponent<Animator>();
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
            if (SceneManager.GetActiveScene().buildIndex is 4)
            {
                Camera.main.GetComponent<Jump>().enabled = false;
                Camera.main.GetComponent<Roll>().enabled = false;

                if (Time.timeScale is 0)
                    Time.timeScale = 1;
            }

            animator.Play("Scene Canvas 0");
            animator.SetTrigger("Scene Canvas");

            yield return wFS128;

            animator.Play("Scene Canvas 1");
            animator.ResetTrigger("Scene Canvas");

            SceneManager.LoadScene(scene);
        }

        private static System.Collections.IEnumerator Single()
        {
            animator.SetTrigger("Scene Canvas 1");
            animator.Play("Scene Canvas 1");

            yield return wFS64;

            animator.ResetTrigger("Scene Canvas 1");
        }

        // murasanca

        public static void Load(int scene) => Monetization.Interstitial(scene);

        public static void Reward(int s) => scene.StartCoroutine(Double(s));
    }
}

// murasanca