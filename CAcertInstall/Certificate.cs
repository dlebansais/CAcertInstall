#nullable enable

using System.Security.Cryptography.X509Certificates;

namespace CAcertInstall
{
    public class Certificate
    {
        public Certificate(X509Certificate2 x509, StoreName storeName)
        {
            X509 = x509;
            StoreName = storeName;
        }

        public X509Certificate2 X509 { get; private set; }
        public StoreName StoreName { get; private set; }
    }
}
