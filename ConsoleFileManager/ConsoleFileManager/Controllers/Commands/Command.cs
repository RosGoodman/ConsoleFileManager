
namespace ConsoleFileManager.Controllers.Commands
{
    public abstract class Command
    {
        public abstract void Execute(string b);
        public abstract void Undo(string b);
    }
}
