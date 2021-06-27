using System;

namespace Anubis.System.Attributes
{
    [AttributeUsage( AttributeTargets.All, AllowMultiple = true )]
    public class RequiredBehaviourAttribute : Attribute
    {
        public Type RequiredObjectType { get; }

        public RequiredBehaviourAttribute(Type requiredObjectType)
        {
            RequiredObjectType = requiredObjectType;
        }
    }
}
