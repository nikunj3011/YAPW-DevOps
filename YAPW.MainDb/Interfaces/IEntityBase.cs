namespace YAPW.MainDb.Interfaces

{
    public interface IEntityBase : IEntityKey
    {
        bool Active { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastModificationDate { get; set; }
    }
}