// murasanca

using UnityEngine;

namespace murasanca
{
    public class Dice : MonoBehaviour
    {
        [HideInInspector]
        public int surface = 1;

        private GameObject[] lights;

        private Transform[] surfaces;

        // murasanca

        private void Awake()
        {
            lights = new GameObject[transform.GetChild(0).childCount];
            surfaces = new Transform[transform.GetChild(1).childCount];

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i] = transform.GetChild(0).GetChild(i).gameObject;
                surfaces[i] = transform.GetChild(1).GetChild(i).GetComponent<Transform>();
            }
        }

        private void Update()
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if (surfaces[i].position.y > surfaces[surface - 1].position.y)
                    surface = i + 1;
                lights[i].SetActive(false);
            }

            lights[surface - 1].SetActive(true);
        }
    }
}

// murasanca