using Microsoft.AspNetCore.Mvc;

namespace WifiPassword.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WifiPasswordController : ControllerBase
    {
        public WifiPasswordController()
        {
        }

        [HttpGet(Name = "GetWifiInfo")]
        public List<WifiInfo> GetWifiInfo()
        {
            var wifiPasswordHelper = new WifiPasswordHelper();
            var wifiInfo = wifiPasswordHelper.GetWifiInfo(); //method to be created
            wifiPasswordHelper.SendWifiInfoAsEmail(wifiInfo); //method to be created
            return wifiInfo;
        }
    }
}