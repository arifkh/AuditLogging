using System;
using AuditDemo.ConsoleApplication.Controllers;
using AuditDemo.ConsoleApplication.Interceptors;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using AuditDemo.Core;
using AuditDemo.ConsoleApplication.DAL;

namespace AuditDemo.ConsoleApplication
{
    class Program
    {
        private static readonly IUnityContainer UnityContainer;

        static Program()
        {
            UnityContainer = new UnityContainer();
            UnityContainer.AddNewExtension<Interception>();

            RegisterTypes();
        }

        static void Main(string[] args)
        {
            var menuManager = UnityContainer.Resolve<MenuManager>();
            menuManager.Show("home");

            var commandText = String.Empty;

            do
            {
                commandText = (Console.ReadLine() ?? "").ToLower();
                
                menuManager.ProcessCommand(commandText);
            } 
            while (!menuManager.TerminateApplication);
        }

        public static void RegisterTypes()
        {
            UnityContainer.RegisterType(typeof (IController), typeof (HomeController), "home",
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<DisplayMenuInterceptionBehavior>());

            UnityContainer.RegisterType(typeof(IController), typeof(AuditController), "audit",
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<DisplayMenuInterceptionBehavior>());

            EmploymentContext contextForEmployeeUOW = UnityContainer.Resolve<EmploymentContext>();
            UnityContainer.RegisterType<IEmployeeAuditUnitOfWork, EmployeeAuditEnabledUnitOfWork>(
                new InjectionConstructor(
                        new EmployeeRepository(contextForEmployeeUOW),
                        new AuditRepository(contextForEmployeeUOW),
                        contextForEmployeeUOW
                    )
            );

            // Need a singleton instance of MenuManager
            UnityContainer.RegisterType<MenuManager>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new object[] { UnityContainer }));
        }
    }
}
