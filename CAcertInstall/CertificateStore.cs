namespace CAcertInstall
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Represents a certificate store.
    /// </summary>
    public static class CertificateStore
    {
        /// <summary>
        /// Checks if a certificate is installed.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns>True if installed; False otherwise.</returns>
        public static bool IsCertificateInstalled(Certificate certificate)
        {
            bool IsFound = false;

            try
            {
                using X509Store store = new X509Store(certificate.StoreName, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);

                foreach (X509Certificate2 Item in store.Certificates)
                    if (Item.Thumbprint == certificate.X509.Thumbprint)
                    {
                        IsFound = true;
                        break;
                    }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

            return IsFound;
        }

        /// <summary>
        /// Installs a collection of certificate.
        /// </summary>
        /// <param name="certificates">The collection of certificates to install.</param>
        /// <returns>True if all certificates have been installed successfully; False otherwise.</returns>
        public static bool InstallCertificates(IEnumerable<Certificate> certificates)
        {
            foreach (Certificate certificate in certificates)
                if (!InstallCertificate(certificate))
                    return false;

            return true;
        }

        /// <summary>
        /// Installs a certificate.
        /// </summary>
        /// <param name="certificate">The certificate to install.</param>
        /// <returns>True if installed successfully; False otherwise.</returns>
        public static bool InstallCertificate(Certificate certificate)
        {
            bool Result = false;

            try
            {
                using X509Store store = new X509Store(certificate.StoreName, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate.X509);

                Result = true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

            return Result;
        }

        /// <summary>
        /// Uninstalls a collection of certificate.
        /// </summary>
        /// <param name="certificates">The collection of certificates to uninstall.</param>
        /// <returns>True if all certificates have been uninstalled successfully; False otherwise.</returns>
        public static bool UninstallCertificates(IEnumerable<Certificate> certificates)
        {
            foreach (Certificate certificate in certificates)
                if (!UninstallCertificate(certificate))
                    return false;

            return true;
        }

        /// <summary>
        /// Uninstalls a certificate.
        /// </summary>
        /// <param name="certificate">The certificate to uninstall.</param>
        /// <returns>True if uninstalled successfully; False otherwise.</returns>
        public static bool UninstallCertificate(Certificate certificate)
        {
            bool Result = false;

            try
            {
                using X509Store store = new X509Store(certificate.StoreName, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                store.Remove(certificate.X509);

                Result = true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

            return Result;
        }
    }
}
