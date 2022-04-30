using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FYP.UI {
    public abstract class UIStateTMPro : MonoBehaviour
    {
        [Header("Sign Up Fields")]
        [SerializeField] protected TMP_InputField registerUsername;
        [SerializeField] protected TMP_InputField registerEmail;
        [SerializeField] protected TMP_InputField registerPassword;
        [SerializeField] protected TMP_InputField registerConfirmPassword;

        [Header("Sign In Fields")]
        [SerializeField] protected TMP_InputField signinEmail;
        [SerializeField] protected TMP_InputField signinPassword;

        [Header("Buttons")]
        [SerializeField] protected Button signupButton;
        [SerializeField] protected Button signinConfirmButton;
        [Space(5)]
        [SerializeField] protected Button signinButton;
        [SerializeField] protected Button confirmRegisterButton;
        [Space(5)]
        [SerializeField] protected Button statusButton;
        [SerializeField] protected Button quitButton;
        [SerializeField] protected Button settingsButton;
        [SerializeField] protected List<Button> closeButton = new List<Button>();

        [Header("Panels")]
        [SerializeField] protected GameObject mainMultiplayer;
        [SerializeField] protected GameObject loadingPanel;
        [SerializeField] protected GameObject signupPanel;
        [SerializeField] protected GameObject signinPanel;
        [SerializeField] protected GameObject errorPanel;
        [SerializeField] protected GameObject statusPanel;
        [SerializeField] protected GameObject inventoryPanel;
        [SerializeField] protected GameObject sellPanel;
        [SerializeField] protected GameObject buyPanel;
        [SerializeField] protected GameObject quitPanel;
        [SerializeField] protected GameObject settingsPanel;

        public abstract void RegisterButtons();

        public virtual void RegisterPlayer()
        {
            UserRegisterInfo info = new UserRegisterInfo();
            info.username = registerUsername.text;
            info.email = registerEmail.text;
            info.password = registerPassword.text;
            info.confirmPassword = registerConfirmPassword.text;

            string json = JsonUtility.ToJson(info);

            Backend.UserAccountController.Instance.OnTryRegisterNewAccount(json);
        }

        public virtual void SignInPlayer()
        {
            Data.LocalSaveFile info = new Data.LocalSaveFile();
            info.email = signinEmail.text;
            info.password = signinPassword.text;
            
            Backend.UserAccountController.Instance.OnTryLogin(info);
        }

        public abstract void SetStatusPlayerName(string value);

        public abstract void SetStatus(Backend.PlayerData data);

        public virtual void QuitPlayer()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}