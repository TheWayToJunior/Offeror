using MediatR;

namespace Offeror.TelegramBot.Features.Resume
{
    public class GetResumeQuery : IRequest<GetResumeResponse>
    {
        public string Region { get; set; }

        public IEnumerable<string> Keywords { get; set; }
    }
}
