using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class CommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public CommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }
        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList().OrderBy(s => s.Id);
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            // Update changes are tracked by the DbContext automatically.
            // No update operations are explicitly called. But this empty method
            // is kept here for consistancy.
        }
    }

}