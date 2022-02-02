using Offeror.LinkedInApi.Domain.Common;

namespace Offeror.LinkedInApi.Domain.Entities
{
    public class Account : EntityBase
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ProfileImage { get; set; }

        public string Url { get; set; }

        public Position Position { get; set; }

        public int PositionId { get; set; }

        public Region Region { get; set; }

        public int RegionId { get; set; }  
    }
}
