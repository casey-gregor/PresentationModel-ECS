
using UnityEngine;

public sealed class XPController
{
    private IPresenter currentPresenter;
    private PresenterFactory presenterFactory;
    public XPController(PresenterFactory factory)
    {
        this.presenterFactory = factory;
    }

    public void UpdateXP(int value)
    {
        if (this.presenterFactory.CurrentPresent != null)
        {
            this.currentPresenter = this.presenterFactory.CurrentPresent;
            this.currentPresenter.PlayerLevel.AddExperience(value);
            //Debug.Log($"{value} added to {this.currentPresenter.UserInfo.Name}");
        }
    }
}
