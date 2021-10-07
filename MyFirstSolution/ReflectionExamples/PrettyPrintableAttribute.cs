using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionExamples
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class PrettyPrintableAttribute : Attribute
    {
    }
}
