using Lessons.Architecture.PM;

public interface IPresenter 
{
    UserInfo UserInfo { get; }
    PlayerLevel PlayerLevel { get; }
    StatsInfo Stats { get; }
}
