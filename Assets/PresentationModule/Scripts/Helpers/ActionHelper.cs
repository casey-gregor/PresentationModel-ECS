using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class ActionHelper : MonoBehaviour
    {
        public int XPToAdd { get => xpToAdd; }
        public int StatToAdd {  get => statToAdd; }

        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private PopupView popupView;
        [SerializeField] private int xpToAdd = 100;
        [SerializeField] private int statToAdd = 1;


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
            IBigPresenter presenter = this.presenterFactory.CreatePresenter(this.playerConfig);
            this.popupView.ShowPopup(presenter);
        }
        
        public void AddXP(int value)
        {
            this.xpController.UpdateXP(value);
        }
    }

}
