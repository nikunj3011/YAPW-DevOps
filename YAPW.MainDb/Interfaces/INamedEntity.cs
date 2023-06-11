namespace YAPW.MainDb.Interfaces
{
    public interface INamedEntity : IEntityBase
    {
        string Description { get; set; }
        string Name { get; set; }
    }
}