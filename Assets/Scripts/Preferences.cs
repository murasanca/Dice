// murasanca

using UnityEngine;

namespace murasanca
{
    public class Preferences : PlayerPrefs
    {
        private readonly static float
            mPV, // mPV: Music Pitch Value.
            mVV; // mVV: Music Volume Value.

        private readonly static int d4, d6, d8, d10, d12, d20, dD, poly, score;

        public static float MPV
        {
            get => GetFloat("mPV", mPV);
            set
            {
                SetFloat("mPV", value);
                Save();
            }
        }
        public static float MVV
        {
            get => GetFloat("mVV", mVV);
            set
            {
                SetFloat("mVV", value);
                Save();
            }
        }
        
        public static int D4
        {
            get => GetInt("d4", d4);
            set
            {
                SetInt("d4", value);
                Save();
            }
        }
        public static int D6
        {
            get => GetInt("d6", d6);
            set
            {
                SetInt("d6", value);
                Save();
            }
        }
        public static int DD
        {
            get => GetInt("dD", dD);
            set
            {
                SetInt("dD", value);
                Save();
            }
        }
        public static int D8
        {
            get => GetInt("d8", d8);
            set
            {
                SetInt("d8", value);
                Save();
            }
        }
        public static int D10
        {
            get => GetInt("d10", d10);
            set
            {
                SetInt("d10", value);
                Save();
            }
        }
        public static int D12
        {
            get => GetInt("d12", d12);
            set
            {
                SetInt("d12", value);
                Save();
            }
        }
        public static int D20
        {
            get => GetInt("d20", d20);
            set
            {
                SetInt("d20", value);
                Save();
            }
        }

        public static int Poly
        {
            get => GetInt("poly", poly);
            set
            {
                SetInt("poly", value);
                Save();
            }
        }

        public static int Score
        {
            get => GetInt("score", score);
            set
            {
                SetInt("score", value);
                Save();
            }
        }
    }
}

// murasanca