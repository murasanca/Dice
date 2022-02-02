// murasanca

using UnityEngine;

namespace murasanca
{
    public class Preferences : PlayerPrefs
    {
        private readonly static float pitch, volume;

        private readonly static int dD, d4, d6, d8, d10, d12, d20, poly;

        // murasanca

        public static int DD
        {
            get
            {
                if (!IAP.HR(dD) && dD is not 0 || !HasKey("dD"))
                    DD = 0;
                return GetInt("dD", dD);
            }
            set
            {
                SetInt("dD", value);
                Save();
            }
        }
        public static int D4
        {
            get
            {
                if (!IAP.HR(d4) && d4 is not 0 || !HasKey("d4"))
                    D4 = 0;
                return GetInt("d4", d4);
            }
            set
            {
                SetInt("d4", value);
                Save();
            }
        }
        public static int D6
        {
            get
            {
                if (!IAP.HR(d6) && d6 is not 0 || !HasKey("d6"))
                    D6 = 0;
                return GetInt("d6", d6);
            }
            set
            {
                SetInt("d6", value);
                Save();
            }
        }
        public static int D8
        {
            get
            {
                if (!IAP.HR(d8) && d8 is not 0 || !HasKey("d8"))
                    D8 = 0;
                return GetInt("d8", d8);
            }
            set
            {
                SetInt("d8", value);
                Save();
            }
        }
        public static int D10
        {
            get
            {
                if (!IAP.HR(d10) && d10 is not 0 || !HasKey("d10"))
                    D10 = 0;
                return GetInt("d10", d10);
            }
            set
            {
                SetInt("d10", value);
                Save();
            }
        }
        public static int D12
        {
            get
            {
                if (!IAP.HR(d12) && d12 is not 0 || !HasKey("d12"))
                    D12 = 0;
                return GetInt("d12", d12);
            }
            set
            {
                SetInt("d12", value);
                Save();
            }
        }
        public static int D20
        {
            get
            {
                if (!IAP.HR(d20) && d20 is not 0 || !HasKey("d20"))
                    D20 = 0;
                return GetInt("d20", d20);
            }
            set
            {
                SetInt("d20", value);
                Save();
            }
        }

        public static float Pitch
        {
            get
            {
                if (!HasKey("pitch"))
                    Pitch = 1;
                return GetFloat("pitch", pitch);
            }
            set
            {
                SetFloat("pitch", value);
                Save();
            }
        }
        public static float Volume
        {
            get
            {
                if (!HasKey("volume"))
                    Volume = .64f;
                return GetFloat("volume", volume);
            }
            set
            {
                SetFloat("volume", value);
                Save();
            }
        }

        public static int Poly
        {
            get
            {
                if (!HasKey("poly"))
                    Poly = 1;
                return GetInt("poly", poly);
            }
            set
            {
                SetInt("poly", value);
                Save();
            }
        }
    }
}

// murasanca