using System;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.Attributes
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        public const string DefaultErrorMessage = "The {0} is required";
        public NonEmptyGuidAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            //NotDefault doesn't necessarily mean required
            if (value is null)
            {
                return true;
            }

            var type = value.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return !value.Equals(defaultValue);
            }

            // non-null ref type
            return true;
        }
    }
}
