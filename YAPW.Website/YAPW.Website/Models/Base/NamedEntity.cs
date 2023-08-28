namespace Ditech.Portal.NET.Models.Base
{
    public abstract class NamedEntity : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}