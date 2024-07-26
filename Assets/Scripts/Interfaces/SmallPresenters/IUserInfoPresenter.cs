using UnityEngine;

public interface IUserInfoPresenter : ISmallPresenter
{
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
}
