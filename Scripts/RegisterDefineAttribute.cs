using System;

namespace Hibzz.DefineManager
{
    /// <summary>
    /// Attribute used to register a define with the define manager
    /// </summary>
    /// <remarks><i>
    /// The function must return <b>DefineRegistrationData</b>
    /// </i></remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RegisterDefineAttribute : Attribute
    {
    }
}
