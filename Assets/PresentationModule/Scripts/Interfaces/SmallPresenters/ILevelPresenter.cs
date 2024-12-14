using UniRx;

namespace Lessons.Architecture.PM
{
    public interface ILevelPresenter : ISmallPresenter
    {
        IReadOnlyReactiveProperty<int> Level { get; }
        IReadOnlyReactiveProperty<int> CurrentExperience { get; }
        IReadOnlyReactiveProperty<int> RequiredExperience { get; }

        bool CanLevelUp { get; }
        void LevelUp();
        string GetLevelText();
        string GetXPText();

    }

}
