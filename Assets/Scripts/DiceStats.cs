// murasanca

using UnityEngine;

namespace murasanca
{
    public class DiceStats : MonoBehaviour
    {
        public int side = 1;

        public Transform[] diceSides;

        private void Update()
        {
            for (int i = 0; i < diceSides.Length; i++)
                if (diceSides[i].position.y > diceSides[side - 1].position.y)
                    side = i + 1;
        }
    }
}

// murasanca