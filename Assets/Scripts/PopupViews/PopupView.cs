using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PopupView : MonoBehaviour
    {
        public IBigPresenter Presenter { get; private set; }
    
        
        [SerializeField] private OutsideButton closeButton;

        private List<IViewable> popupViews = new();


        [Inject]
        public void Contstuct(IViewable[] viewables)
        {
            popupViews = viewables.ToList<IViewable>();
        }
        public void ShowPopup(IBigPresenter presenter)
        {
            this.Presenter = presenter;

            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }

            foreach(IViewable viewable in popupViews)
            {
                viewable.Initiate(presenter);
            }

            this.closeButton.CloseEvent += HidePopup;
  
        }

        public void HidePopup()
        {
            this.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            this.closeButton.CloseEvent -= HidePopup;
        }
    }



}
