using Lessons.Architecture.PM;
using System.Collections;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class UserInfoPresenter : IUserInfoPresenter
    {
        public Sprite Icon { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private UserInfo userInfo;

        public UserInfoPresenter(PlayerConfig config)
        {
            SetupUserInfo(config);
        }

        private void SetupUserInfo(PlayerConfig config)
        {
            this.userInfo = new UserInfo();

            this.userInfo.ChangeName(config.playerName);
            this.Name = this.userInfo.Name;

            this.userInfo.ChangeDescription(config.playerDescription);
            this.Description = this.userInfo.Description;

            this.userInfo.ChangeIcon(config.playerAvatar);
            this.Icon = this.userInfo.Icon;
        }
    }

}
