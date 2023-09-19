using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crescent.WinForms.Helpers
{
    public static class SecureStringHelper
    {
        public static string ConvertToUnsecureString(this SecureString secureText)
        {
            if (secureText == null) return null;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureText);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public static SecureString ConvertToSecureString(this string text)
        {
            if (text == null) return null;

            SecureString secureText = new SecureString();
            foreach (var c in text.ToCharArray())
            {
                secureText.AppendChar(c);
            }
            secureText.MakeReadOnly();
            return secureText;
        }

    }
}
