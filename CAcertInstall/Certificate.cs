#nullable enable

using System.Security.Cryptography.X509Certificates;

namespace CAcertInstall
{
    public class Certificate
    {
        #region Init
        public Certificate(X509Certificate2 x509, StoreName storeName)
        {
            X509 = x509;
            StoreName = storeName;
        }
        #endregion

        #region Properties
        public X509Certificate2 X509 { get; }
        public StoreName StoreName { get; }
        #endregion
    }
}
