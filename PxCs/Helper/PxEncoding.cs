using System.Text;

namespace PxCs.Helper
{
    public class PxEncoding
    {
        internal static Encoding Encoding;
        internal static bool isEncodingSet;

        public enum SupportedEncodings
        {
            CentralEurope = 1250,
            EastEurope = 1251,
            WestEurope = 1252
        };
        
        public static void SetEncoding(SupportedEncodings encodingId)
        {
            // As PxCs is with .netstandard2.1 we need to register the coding provider once.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Encoding = Encoding.GetEncoding((int)encodingId);

            isEncodingSet = true;
        }

    }
}