
using System;

namespace ZyGames.Framework.Model
{
    /// <summary>
    /// Entity field allow child extend.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldExtendAttribute : Attribute
    {
    }
}
