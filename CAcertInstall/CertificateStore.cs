using System.Security.Cryptography.X509Certificates;
using System;
using System.Diagnostics;

namespace CAcertInstall
{
    public class CertificateStore
    {
        private const StoreName RootStoreName = StoreName.Root;

        public static bool IsCertificateInstalled(X509Certificate2 certificate)
        {
            bool Found = false;

            X509Store store = new X509Store(RootStoreName, StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 Item in store.Certificates)
                    if (Item.Thumbprint == certificate.Thumbprint)
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

        public static bool InstallCertificate(X509Certificate2 certificate)
        {
            X509Store store = new X509Store(RootStoreName, StoreLocation.LocalMachine);
            bool Result;

            try
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
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
