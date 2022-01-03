// murasanca

using UnityEngine;

namespace murasanca
{
    public class DiceHighlight : MonoBehaviour
    {
        private DiceStats diceStats;
        public GameObject[] sides;

        private void Awake() => diceStats = gameObject.GetComponent<DiceStats>();

        private void Update()
        {
            for (int i = 0; i < sides.Length; i++)
                sides[i].SetActive(false);
            sides[diceStats.side - 1].SetActive(true);
        }
    }
}

// murasanca