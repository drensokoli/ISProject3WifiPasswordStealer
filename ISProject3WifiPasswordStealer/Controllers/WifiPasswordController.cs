using ISProject3WifiPasswordStealer.Models;
using ISProject3WifiPasswordStealer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WifiPassword.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WifiPasswordController : ControllerBase
    {
        private readonly ILogger<WifiPasswordController> _logger;
        private readonly WifiPasswordHelper _wifiPasswordHelper;

        public WifiPasswordController(ILogger<WifiPasswordController> logger, WifiPasswordHelper wifiPasswordHelper)
        {
            _logger = logger;
            _wifiPasswordHelper = wifiPasswordHelper;
        }
        
        [HttpGet("GetWifiPasswordByName")]
        public string GetWifiPasswordByName(string wifiName)
        {
            var wifiPassword = _wifiPasswordHelper.GetWifiPasswordByName(wifiName);
            return wifiPassword;
        }

        [HttpGet("GetAllWifiInfo")]
        public List<WifiInfo> GetAllWifiInfo()
        {
            var wifiInfo = _wifiPasswordHelper.GetAllWifiInfo();
            return wifiInfo;
        }


    }
}