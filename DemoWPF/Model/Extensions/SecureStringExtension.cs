using System.Runtime.InteropServices;
using System.Security;

namespace DemoWPF.Model.Extensions
{
    public static class SecureStringExtensions
    {
        public static void Dispose(this SecureString secureString)
        {
            if (secureString != null)
            {
                secureString.Clear();
                secureString.Dispose();
            }
        }

        public static string ConvertToUnsecureString(this SecureString secureString)
        {
            if (secureString == null)
            {
                throw new ArgumentNullException(nameof(secureString));
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
