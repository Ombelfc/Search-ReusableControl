﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByRegex.Commands
{
    public class OpenCommand : BaseCommand
    {
        public OpenCommand(Action methodToExecute) : base(methodToExecute) { }
    }
}
