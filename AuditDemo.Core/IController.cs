namespace AuditDemo.Core
{
    public interface IController
    {
        void Activate();

        void ProcessCommand(string command);
    }
}
