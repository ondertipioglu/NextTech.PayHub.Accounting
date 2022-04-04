using MediatR;

namespace NextTech.Core.MediatR.Commands
{
    public interface ICommand : IRequest { }
    public interface ICommand<out TResponse> : IRequest<TResponse> { }

}
