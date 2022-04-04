using MediatR;

namespace NextTech.Core.MediatR.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
