﻿using System.Reflection;

namespace Shezzy.Shared.Commands
{
    public class CommandDescriptor
    {
        public string[] Names { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public string HelpText { get; set; }
    }
}
