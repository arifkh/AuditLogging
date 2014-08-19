using System;
using AuditDemo.Core;

namespace AuditDemo.ConsoleApplication.Controllers
{
    public class HomeController : IController
    {
        public HomeController()
        {
        }

        public void Activate()
        {
        }

        public void ProcessCommand(string command)
        {
            Console.WriteLine("{0} command was sent", command);

            switch (command.ToLower())
            {
                default:
                    break;
            }
        }
    }
}
