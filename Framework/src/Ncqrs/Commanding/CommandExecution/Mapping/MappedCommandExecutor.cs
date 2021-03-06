﻿using System;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Ncqrs.Commanding.CommandExecution.Mapping
{
    /// <summary>
    /// A command handler that execute an action based on the mapping of a command.
    /// </summary>
    public class MappedCommandExecutor<TCommand> : ICommandExecutor<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command to execute. This should not be null.</param>
        /// <exception cref="ArgumentNullException">Occurs when <i>command</i> is null.</exception>
        public void Execute(TCommand command)
        {
            var factory = new ActionFactory();
            ICommandExecutor<TCommand> executor = factory.CreateExecutorForCommand(command);

            if (command.GetType().GetCustomAttributes(typeof(TransactionalAttribute), true).Length > 0)
            {
                executor = new TransactionalCommandExecutorWrapper<TCommand>(executor);
            }

            executor.Execute(command);
        }
    }
}
