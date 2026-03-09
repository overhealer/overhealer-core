using System;

namespace overhealer.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute()
        {

        }
    }
}