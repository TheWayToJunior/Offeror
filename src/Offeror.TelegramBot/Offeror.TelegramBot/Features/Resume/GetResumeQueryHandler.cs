using MediatR;

namespace Offeror.TelegramBot.Features.Resume
{
    public class GetResumeQueryHandler : IRequestHandler<GetResumeQuery, GetResumeResponse>
    {
        public Task<GetResumeResponse> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
