// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Xr+jSICXbLFmbfCZy2A3tKe6496SSJrrjdpob/bdSqBM/7HkTF7lgufFccV11duS/vS5/QqRRT6gyI8AWAsTsupk2xt8Xu2iGBIac6zYaKA8douFg+HlG9OjI0WULt76tkU3oLIys6s2hG0D2TwzPEEoUu5kR0wf68InFhYS5LCndFJVLCXTExxoQRrovAlxAASgP8ukSvrIEf+dBdqj73/88v3Nf/z3/3/8/P189ByXMpmGBrPIqwXjtQ1yHhwl8oqTfm9KLjN+uiJE4WA9FZcZU33K2xdKDC9kK/9Zt3ZEuPdh78Ap4eu5pn3ssa6zzX/8383w+/TXe7V7CvD8/Pz4/f6iKN2jeMR+CFi6iGX1CMxp/JFIxje6cJxm155j4P/+/P38");
        private static int[] order = new int[] { 3,10,4,5,13,9,6,12,10,11,10,13,13,13,14 };
        private static int key = 253;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
