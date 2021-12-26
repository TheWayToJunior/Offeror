using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offeror.LinkedInApi.Shared.DTO.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfileImage { get; set; }
        public string Url { get; set; }
        public string Position { get; set; }
        public string Region { get; set; }
    }
}
