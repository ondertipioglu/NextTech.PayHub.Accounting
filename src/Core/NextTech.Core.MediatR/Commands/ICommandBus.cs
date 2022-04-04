using System.Threading.Tasks;

namespace NextTech.Core.MediatR.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
