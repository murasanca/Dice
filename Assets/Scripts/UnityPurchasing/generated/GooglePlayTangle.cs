// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("IqIjO6YU/ZNJrKOs0bjCfvTX3I/uKrLUcfCthQeJw+1aS4fanL/0u3dV4VXlRUsCbmQpbZoB1a4wWB+QyJuDInr0S4vszn0yiIKK4zxI+DBvySfm1Chn8X9QuXF7KTbtfCE+I6zmGxUTcXWLQzOz1QS+Tmom1acwAtgKex1K+P9mTdow3G8hdNzOdRJ7UreGhoJ0IDfkwsW8tUODjPjRipYjWDuVcyWd4o6MtWIaA+7/2r6jzi8z2BAH/CH2/WAJW/CnJDcqc054LJnhkJQwr1s02mpYgW8NlUozfzK4TTPoVO6YyCoY9WWYXPlsAdhWXe9sT11ga2RH6yXrmmBsbGxobW7vbGJtXe9sZ2/vbGxt7GSMB6IJFqcq4Az2Rw7zcG9ubG1s");
        private static int[] order = new int[] { 9,8,4,5,4,13,10,10,11,13,12,12,13,13,14 };
        private static int key = 109;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
