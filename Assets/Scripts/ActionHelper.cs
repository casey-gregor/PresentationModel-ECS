using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class ActionHelper : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private PopupView popupView;
        [SerializeField] private int xpToAdd = 100;
        [SerializeField] private int statToAdd = 1;

        public int XPToAdd { get => xpToAdd; }
        public int StatToAdd {  get => statToAdd; }

        private PresenterFactory presenterFactory;
        private XPController xpController;

        [Inject]
        public void Construct(PresenterFactory presenterFactory, XPController xpController)
        {
            this.presenterFactory = presenterFactory;
            this.xpController = xpController;
        }
        public void ShowPopup()
        {
            IPresenter presenter = this.presenterFactory.CreatePresenter(this.playerConfig);
            this.popupView.ShowPopup(presenter);
        }

        public void AddXP(int value)
        {
            this.xpController.UpdateXP(value);
        }
    }

}
