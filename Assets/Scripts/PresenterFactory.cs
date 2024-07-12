
using System;

public class PresenterFactory
{
    private IPresenter currentPresenter;
    public IPresenter CurrentPresent {  get { return currentPresenter; } }

    public event Action<IPresenter> presenterCreatedEvent;

    public IPresenter CreatePresenter(PlayerConfig playerConfig)
    {
        IPresenter presenter = new Player(playerConfig);
        this.currentPresenter = presenter;
        presenterCreatedEvent?.Invoke(this.currentPresenter);
        return presenter;
    }
   
}
