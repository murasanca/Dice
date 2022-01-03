// murasanca

using UnityEngine;
using UnityEngine.SceneManagement;

namespace murasanca
{
    public class Scene : MonoBehaviour
    {
        public static Scene scene;

        private void Awake()
        {
            if (scene is null)
                scene = this;
            else if (scene != this)
                Destroy(gameObject);
            DontDestroyOnLoad(scene);
        }

        public static void Load(int scene)
        {
            // Animation ..
            SceneManager.LoadScene(scene);
        }
    }
}

// murasanca