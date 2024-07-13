


namespace Lessons.Architecture.PM
{
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
            if (this.presenterFactory.CurrentPresenter != null)
            {
                this.currentPresenter = this.presenterFactory.CurrentPresenter;
                this.currentPresenter.PlayerLevel.AddExperience(value);
            }
        }
    }

}
