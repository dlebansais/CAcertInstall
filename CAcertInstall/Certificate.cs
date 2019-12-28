namespace CAcertInstall
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Represents a certificate.
    /// </summary>
    public class Certificate
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="Certificate"/> class.
        /// </summary>
        /// <param name="x509">The certificate data.</param>
        /// <param name="storeName">The store for wich this certificate is intended.</param>
        public Certificate(X509Certificate2 x509, StoreName storeName)
        {
            X509 = x509;
            StoreName = storeName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the certificate data.
        /// </summary>
        public X509Certificate2 X509 { get; }

        /// <summary>
        /// Gets the store for wich this certificate is intended.
        /// </summary>
        public StoreName StoreName { get; }
        #endregion
    }
}
