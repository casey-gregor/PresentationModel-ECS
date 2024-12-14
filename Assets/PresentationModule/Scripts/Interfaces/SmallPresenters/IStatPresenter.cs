
namespace Lessons.Architecture.PM
{
    public interface IStatPresenter : ISmallPresenter
    {
        string Name { get; }
        string GetStatText();
    }

}
