using System;

namespace Demo.Common.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateWrapperAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotRenderAttribute : Attribute
    {
    }
}
