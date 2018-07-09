using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotNetCoreAPIServices.Controllers
{
    [Produces("application/json")]
    [Route("api/Palindrome")]
    public class PalindromeController : Controller
    {
        IPalidromeRepository _palindromeReporitory;

        public PalindromeController(IPalidromeRepository palindromeReporitory)
        {
            _palindromeReporitory = palindromeReporitory;
        }

        [HttpGet]
        [Route("GetPalindrome")]
        public IEnumerable<PalindromeDTO> Get()
        {
            return _palindromeReporitory.GetAll();
        }

        // POST api/values
        [HttpPost]
        [Route("SavePalindrome")]
        public bool Post([FromBody] PalindromeDTO palindrome)
        {
            return _palindromeReporitory.Save(palindrome);
        }

    }
}