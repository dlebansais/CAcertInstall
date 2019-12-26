#nullable enable

using System.Security.Cryptography.X509Certificates;
using System;
using System.Diagnostics;

namespace CAcertInstall
{
    public static class CertificateStore
    {
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

        public static bool InstallCertificates(Certificate[] certificates)
        {
            foreach (Certificate certificate in certificates)
                if (!InstallCertificate(certificate))
                    return false;

            return true;
        }
           
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
    }
}
