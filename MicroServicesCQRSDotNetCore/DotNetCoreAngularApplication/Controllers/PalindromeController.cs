using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetCoreAngularApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/PalindromeData")]
    public class PalindromeController : Controller
    {
        IManageApi _managerApi;

        public PalindromeController(IManageApi managerApi)
        {
            _managerApi = managerApi;
        }

        [HttpGet]
        [Route("GetPalindrome")]
        public IEnumerable<PalindromeDTO> GetPalindrome()
        {
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            var result = _managerApi.GetSynch<List<PalindromeDTO>>("Palindrome", "GetPalindrome", apiParams);
            return result;
        }

        [HttpPost]
        [Route("SavePalindrome")]
        public bool SavePalindrome([FromBody] PalindromeDTO palindrome)
        {
            char[] charArray = palindrome.Name.ToCharArray();
            Array.Reverse(charArray);
            string result = new string(charArray);
            if (result.ToUpper() == palindrome.Name.ToUpper())
            {
                return _managerApi.PostSynch<bool>("Palindrome", "SavePalindrome", palindrome);
            }
            else
            {
                return false;
            }

        }

    }
}