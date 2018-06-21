namespace CheckTLSSupport
{
    using System;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            //Based on code from https://stackoverflow.com/a/43534406
            Console.WriteLine(".NET Runtime: " + System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(int).Assembly.Location).ProductVersion);
            Console.WriteLine("Enabled protocols:   " + ServicePointManager.SecurityProtocol);

            Console.WriteLine("Available protocols: ");
            bool platformSupportsTls13 = false;

            foreach (SecurityProtocolType protocol in Enum.GetValues(typeof(SecurityProtocolType)))
            {
                Console.WriteLine("{0} ({1})", protocol.ToString(), protocol.GetHashCode());
                if (protocol.GetHashCode() == 48 && protocol.GetHashCode() == 192 && protocol.GetHashCode() == 768 && protocol.GetHashCode() == 3072)
                {
                    platformSupportsTls13 = true;
                }
            }

            var isTls13Enabled = ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)48) &&
                                 ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)192) &&
                                 ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)768) &&
                                 ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)3072);
            Console.WriteLine("Is Tls13 enabled: " + isTls13Enabled);

            if (!isTls13Enabled)
            {
                if (platformSupportsTls13)
                {
                    Console.WriteLine("Platform supports Tls13, but it is not enabled.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Platform does not support Tls13.");
                }
            }
        }
    }
}
