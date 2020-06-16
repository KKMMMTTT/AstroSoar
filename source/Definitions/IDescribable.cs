namespace Game.Definitions
{
    public interface IDescribable
    {
        string? Description { get; set; }
        string FullDescription { get; }
    }
}
