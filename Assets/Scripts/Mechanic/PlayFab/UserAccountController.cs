using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;


namespace FYP.Backend
{
    public class UserAccountController : Singleton<UserAccountController>
    {
        [Header("Screens")]
        public GameObject LoginPanel;
        public GameObject RegisterPanel;

        [Header("Login Screen")]
        public TMP_InputField LoginEmailField;
        public TMP_InputField LoginPasswordField;
        public Button LoginBtn;
        public Button RegisterBtn;

        [Header("Register Screen")]
        public TMP_InputField RegisterEmailField;
        public TMP_InputField RegisterUsernameField;
        public TMP_InputField RegisterPasswordField;
        public Button RegisterAccountBtn;
        public Button BackBtn;

        [Header("Error SignIn/Up")]
        public TMP_Text ErrorLogin;
        public TMP_Text ErrorSignUp;

        
        #region togglebuttonpanel
        public void OpenLoginPanel()
        {
            LoginPanel.SetActive(true);
            RegisterPanel.SetActive(false);
        }

        public void OpenRegistrationPanel()
        {
            LoginPanel.SetActive(false);
            RegisterPanel.SetActive(true);
        }
        #endregion

        #region signup
        public void OnTryRegisterNewAccount()
        {
            BackBtn.interactable = false;
            RegisterAccountBtn.interactable = false;

            string email = RegisterEmailField.text;
            string username = RegisterUsernameField.text;
            string password = RegisterPasswordField.text;


            RegisterPlayFabUserRequest req = new RegisterPlayFabUserRequest
            {
                Email = email,
                DisplayName = username,
                Password = password,
                RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(req,
            res =>
            {
                BackBtn.interactable = true;
                RegisterAccountBtn.interactable = true;
                OpenLoginPanel();
                Debug.Log(res.PlayFabId);
            },
            err =>
            {
                BackBtn.interactable = true;
                RegisterAccountBtn.interactable = true;
                Debug.Log("Error: " + err.ErrorMessage);
                ErrorSignUp.text = err.GenerateErrorReport();
            });
        }



        #endregion

        #region sign in
        public void OnTryLogin()
        {
            string email = LoginEmailField.text;
            string password = LoginPasswordField.text;
            
            LoginBtn.interactable = false;

            LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password,
                InfoRequestParameters = Backend.PlayFabManager.Instance.infoRequest,
               
            };

            PlayFabClientAPI.LoginWithEmailAddress(req,
            res =>
            {
                GetUserInfo(email, res.PlayFabId);
                Debug.Log("login success");
                Backend.PlayFabManager.Instance.KC = res.InfoResultPayload.UserVirtualCurrency["KC"]; // to get the user virtual currency from playfab portal

                //! calling the function from Inventory System script
                //InventorySystem.Instance.BuyItem()
                InventorySystem.Instance.GetItemPrice();
                InventorySystem.Instance.UpdateInventory();
                foreach(GameObject obj in InventorySystem.Instance.enableGameObject)
                {
                    obj.SetActive(true);
                }
            },
            err =>
            {
                Debug.Log("Error: " + err.ErrorMessage);
                LoginBtn.interactable = true;
                ErrorLogin.text = err.GenerateErrorReport();
            });
        }

        public void PlayFabError(PlayFabError error)
        {
            ErrorLogin.text = error.GenerateErrorReport();
        }
        #endregion


        void GetUserInfo(string email, string playfabId)
        {
            GetAccountInfoRequest req = new GetAccountInfoRequest
            {
                Email = email,
                PlayFabId = playfabId,
            };

            PlayFabClientAPI.GetAccountInfo(req,
                res =>
                {
                    //Data.PlayfabAccountInfo.FillData(res.AccountInfo);
                    Data.PlayfabAccountInfo.FillData(res.AccountInfo, () => {
                        LevelManager.Instance.LoadNextLevel();
                    });
                },
                PlayFabError);
        }

        
    }
}