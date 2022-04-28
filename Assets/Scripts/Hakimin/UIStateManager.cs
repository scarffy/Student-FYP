using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FYP.UI
{
    public class UIStateManager : UIStateTMPro
    {
        #region public variable
        public static UIStateManager Instance { get; private set; }
        [Space(30)]
        public State state;
        public enum State
        {
            none,
            single,
            multiplayer,
            signup,
            signin,
            error,
            status,
            inventory,
            sell,
            buy,
            quit,
        }
        #endregion

        #region Initialized
        private void Awake()
        {
            if (Instance != this  && Instance != null) Destroy(this);
            else Instance = this;
        }

        void Start()
        {
            Backend.UserAccountController.Instance.OnLoggedIn += LoggedIn;
            Backend.UserAccountController.Instance.OnFoundInfo += FoundInfo;
            RegisterButtons();

            SetState(0);
        }

        /// <summary>
        /// Get player's status during first time login
        /// </summary>
        void LoggedIn()
        {
            SetState(State.none);
            SetStatus(Backend.PlayerStats.Instance.playerData);
            Backend.UserAccountController.Instance.OnLoggedIn -= LoggedIn;
        }

        /// <summary>
        /// Updating the player name in status windows
        /// </summary>
        void FoundInfo()
        {
            SetStatusPlayerName(Data.PlayfabAccountInfo.Instance.accountInfo.TitleInfo.DisplayName);
            Backend.UserAccountController.Instance.OnFoundInfo -= FoundInfo;
        }

        public override void RegisterButtons()
        {
            signupButton.onClick.AddListener(() => { SetStates(1); });
            signinButton.onClick.AddListener(() => { SetStates(2); });
            confirmRegisterButton.onClick.AddListener(() => { RegisterPlayer(); });
            signinConfirmButton.onClick.AddListener(() => { SignInPlayer(); });

            for (int i = 0; i < closeButton.Count; i++)
            {
                closeButton[i].onClick.AddListener(() => { SetStates(0); });
            }
            quitButton.onClick.AddListener(() => { QuitPlayer(); });
        }
        #endregion

        public void SetStates(int value)
        {
            state = (State)value;
            SetState(state);
        }

        public void SetState(State curState)
        {
            state = curState;

            mainMultiplayer.SetActive(state == State.multiplayer);
            signupPanel.SetActive(state == State.signup);
            signinPanel.SetActive(state == State.signin);
            //errorPanel.SetActive(state == State.error);
            statusPanel.SetActive(state == State.status);
            inventoryPanel.SetActive(state == State.inventory);
            sellPanel.SetActive(state == State.sell);
            buyPanel.SetActive(state == State.buy);
            quitPanel.SetActive(state == State.quit);

        }

        /// <summary>
        /// Any error related panel
        /// </summary>
        /// <param name="errorMsg"></param>
        public void SetErrorState(string errorMsg)
        {
            SetState(State.error);
            //TODO: Setup error panel msg
        }

        public override void RegisterPlayer() => base.RegisterPlayer();
        public override void SignInPlayer() => base.SignInPlayer();

        public override void SetStatus(Backend.PlayerData data)
        {
            UIStatusController.Instance.SetStatus(data);
        }

        public override void SetStatusPlayerName(string value)
        {
            UIStatusController.Instance.SetStatusPlayerName(value);
        }

        public override void QuitPlayer() => base.QuitPlayer();

        public string GetEmailSignIn => signinEmail.text;
        public string GetPass => signinPassword.text;

    }
}