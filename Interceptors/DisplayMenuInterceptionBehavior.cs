using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AuditDemo.ConsoleApplication.Interceptors
{
    public class DisplayMenuInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.Name == "Activate")
            {
                string namedController = input.Target.GetType().Name;
                string name = namedController.Replace("Controller", "");
                this.WriteMenuToConsole(string.Format(@"Views\{0}View.txt", name));
            }

            IMethodReturn result = getNext()(input, getNext);
            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private void WriteMenuToConsole(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine("\n" + line);
            }
            Console.WriteLine("\n");
        } 
    }
}
