using Lessons.Architecture.PM;
using UniRx;
using UnityEngine;

public interface IPresenter 
{
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
    IReadOnlyReactiveProperty<int> Level { get; }
    IReadOnlyReactiveProperty<int> CurrentExperience { get; }
    IReadOnlyReactiveProperty<int> RequiredExperience { get; }

    bool CanLevelUp { get; }
    PlayerStat[] GetStats();
    void LevelUp();

}
