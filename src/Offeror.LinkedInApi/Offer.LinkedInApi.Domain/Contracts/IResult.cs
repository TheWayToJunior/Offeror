using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offeror.LinkedInApi.Domain.Contracts
{
    public interface IResult
    {
        public IEnumerable<string> Errors { get; }
        public bool IsSuccess { get; }
    }

    public interface IResult<out T> : IResult
    {
        T Value { get; }
    }
}
