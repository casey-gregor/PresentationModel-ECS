using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoView : MonoBehaviour, IViewable
{
    [SerializeField] private Image heroAva;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI heroDescription;

    private IUserInfoPresenter presenter;

    public void Initiate(IPresenter presenter)
    {
        foreach (ISmallPresenter smallPresenter in presenter.SmallPresenters)
        {
            if (smallPresenter is IUserInfoPresenter userInfoPresenter)
            {
                this.presenter = userInfoPresenter;
            }
        }

        UpdateUserInfo();
    }

    private void UpdateUserInfo()
    {
       
        this.heroAva.sprite = this.presenter.Icon;
        this.heroName.text = this.presenter.Name;
        this.heroDescription.text = this.presenter.Description;
    }
}
