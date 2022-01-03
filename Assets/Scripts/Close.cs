// murasanca

using UnityEngine;

namespace murasanca
{
    public class Close : MonoBehaviour
    {
        [SerializeField]
        private GameObject dmT; // dmT: Dice murasanca Text (TMP).

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Load(0); 
        }

        public void Dice()=> Application.OpenURL("market://details?id=com.murasanca.Dice");

        public void Load(int scene) => Scene.Load(scene);

        public void Quit() => Application.Quit();
    }
}

// murasanca