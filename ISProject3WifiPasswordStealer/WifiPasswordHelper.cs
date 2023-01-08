using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WifiPassword
{
    public class WifiPasswordHelper
    {
        private static string[] GetProfiles()
        {
            var output = RunCommand("netsh", "wlan show profiles"); //method to be created
            var lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var result = lines.Where(line => line.Contains(" : ")).Select(x => x.Substring(x.IndexOf(":") + 2)).ToArray();
            return result;
        }

    }
}
