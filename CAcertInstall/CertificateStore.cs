using System.Security.Cryptography.X509Certificates;
using System;
using System.Diagnostics;

namespace CAcertInstall
{
    public class CertificateStore
    {
        public static bool IsCertificateInstalled(Certificate certificate)
        {
            bool Found = false;

            X509Store store = new X509Store(certificate.StoreName, StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 Item in store.Certificates)
                    if (Item.Thumbprint == certificate.X509.Thumbprint)
                    {
                        Found = true;
                        break;
                    }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

            store.Close();

            return Found;
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
            X509Store store = new X509Store(certificate.StoreName, StoreLocation.LocalMachine);
            bool Result;

            try
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate.X509);
                Result = true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Result = false;
            }

            store.Close();

            return Result;
        }
    }
}
