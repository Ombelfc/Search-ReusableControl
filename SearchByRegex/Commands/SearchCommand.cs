using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByRegex.Commands
{
    public class SearchCommand : BaseCommand
    {
        public SearchCommand(Action methodToExecute, Func<bool> canExecuteMethod) : base(methodToExecute, canExecuteMethod) { }
    }
}
