using Lessons.Architecture.PM;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IPresenter
{
    IReadOnlyList<ISmallPresenter> SmallPresenters { get; }
   
}

public interface ISmallPresenter
{

}

public interface IUserInfoPresenter : ISmallPresenter
{
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
}

public interface ILevelPresenter : ISmallPresenter
{
    IReadOnlyReactiveProperty<int> Level { get; }
    IReadOnlyReactiveProperty<int> CurrentExperience { get; }
    IReadOnlyReactiveProperty<int> RequiredExperience { get; }

    bool CanLevelUp { get; }
    void LevelUp();
}

public interface IStatsPresenter : ISmallPresenter
{
    event Action StatsUpdateEvent;
    PlayerStat[] GetStats();

}

public interface IViewable
{
    void Initiate(IPresenter presenter);
}
