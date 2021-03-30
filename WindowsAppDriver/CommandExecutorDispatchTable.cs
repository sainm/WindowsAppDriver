using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WindowsAppDriver.CommandExecutors;
using CommonLib;
using Winium.Desktop.Driver.CommandExecutors;

namespace WindowsAppDriver
{
    internal class CommandExecutorDispatchTable
    {
        private Dictionary<string, Type> commandExecutorsRepository;
        
        public CommandExecutorDispatchTable()
        {
            this.ConstructDispatcherTable();
        }
        
        public CommandExecutorBase GetExecutor(string commandName)
        {
            Type executorType;
            if (this.commandExecutorsRepository.TryGetValue(commandName, out executorType))
            {
            }
            else
            {
                executorType = typeof(NotImplementedExecutor);
            }

            return (CommandExecutorBase)Activator.CreateInstance(executorType);
        }
        
        private void ConstructDispatcherTable()
        {
            this.commandExecutorsRepository = new Dictionary<string, Type>();

            // TODO: bad const
            const string ExecutorsNamespace = "Winium.Desktop.Driver.CommandExecutors";

            var q =
                (from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == ExecutorsNamespace
                    select t).ToArray();

            var fields = typeof(DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var localField = field;
                var executor = q.FirstOrDefault(x => x.Name.Equals(localField.Name + "Executor"));
                if (executor != null)
                {
                    this.commandExecutorsRepository.Add(localField.GetValue(null).ToString(), executor);
                }
            }
        }

    }
}