using System;
using Microsoft.Practices.Unity;
using AuditDemo.Core;

namespace AuditDemo.ConsoleApplication
{
    public sealed class MenuManager
    {
        private readonly IUnityContainer _container;
        private IController _activeController;

        public MenuManager(IUnityContainer container)
        {
            _container = container;
        }

        public bool TerminateApplication { get; set; }
        public void Show(string menu)
        {
            var controller = _container.Resolve<IController>(menu);

            controller.Activate();
            _activeController = controller;
        }

        public void ProcessCommand(string command)
        {
            if (string.Compare(command, "exit", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.TerminateApplication = true;
                return;
            }
            else if (_container.IsRegistered<IController>(command))
            {
                Show(command);
            }

            _activeController.ProcessCommand(command);
        }
    }
}
