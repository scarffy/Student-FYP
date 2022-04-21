using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using FYP.UI;

namespace FYP.Backend
{
    /// <summary>
    /// Register and Login player
    /// </summary>
    public class UserAccountController : Singleton<UserAccountController>
    {
        public Action OnLoggedIn;

        private void Start()
        {
            OnCheckLogin();
        }

        public bool samePassword(UserRegisterInfo info)
        {
            if (info.password == info.confirmPassword)
                return true;
            else
                return false;
        }

        #region signup
        public void OnTryRegisterNewAccount(string json)
        {
            UserRegisterInfo info = JsonUtility.FromJson<UserRegisterInfo>(json);

            if (!samePassword(info))
                return;

            RegisterPlayFabUserRequest req = new RegisterPlayFabUserRequest
            {
                Email = info.email,
                DisplayName = info.username,
                Username = info.username,
                Password = info.password,
                RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(req,
                res => {
                    UI.UIStateManager.Instance.SetState(UI.UIStateManager.State.none);
                    PlayFabManager.Instance.isSignIn = true;


                    Data.LocalSaveFile localSaveFile = new Data.LocalSaveFile();
                    localSaveFile.username = info.username;
                    localSaveFile.displayName = info.username;
                    localSaveFile.playfabId = res.PlayFabId;

                    Data.UserLocalSaveFile.Instance.SaveData(localSaveFile);
                },
                err => {
                    Debug.Log(err.GenerateErrorReport());
                    UI.UIStateManager.Instance.SetErrorState("Some error msg here");
                });
        }
        #endregion

        #region auto sign in
        public void OnCheckLogin()
        {
            if (Data.UserLocalSaveFile.Instance.DataExist() && !PlayFabManager.Instance.isSignIn){
                Debug.Log("File Exist. Try to login");
                OnTryLogin(Data.UserLocalSaveFile.Instance.saveData);
            }
        }

        public void OnTryLogin(Data.LocalSaveFile saveData)
        {
            LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
            {
                Email = saveData.email,
                Password = saveData.password,
                InfoRequestParameters = Data.PlayfabAccountInfo.Instance.infoRequest,
            };

            PlayFabClientAPI.LoginWithEmailAddress(req,
           res =>
           {
               GetUserInfo(UI.UIStateManager.Instance.GetEmailSignIn, res.PlayFabId);
               PlayerStats.Instance.GetUserData(res.PlayFabId, "PlayerGender", null,res => 
               {
                   Debug.Log($"{res}");
                   if (res.Contains("True"))
                       GenderSelection.Instance.SelectGender(true);
                   else
                       GenderSelection.Instance.SelectGender(false);
               });
               PlayFabManager.Instance.isSignIn = true;
               PlayFabManager.Instance.KC = res.InfoResultPayload.UserVirtualCurrency["KC"]; // to get the user virtual currency from playfab portal

                //! calling the function from Inventory System script
                //InventorySystem.Instance.BuyItem()
               InventorySystem.Instance.GetItemPrice();
               InventorySystem.Instance.UpdateInventory();
               foreach (GameObject obj in InventorySystem.Instance.enableGameObject)
               {
                   obj.SetActive(true);
               }

               PlayerStats.Instance.SetUserData();
               PlayerStats.Instance.GetUserData(res.PlayFabId);
               MonsterStats.Instance.GetTitleData();

               OnLoggedIn?.Invoke();
           },
           err =>
           {
               Debug.Log("Error: " + err.ErrorMessage);
           });
        }

        #endregion

        #region sign in
        public void OnTryLogin()
        {
            LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
            {
                //Email = email,
                //Password = password,
                InfoRequestParameters = Data.PlayfabAccountInfo.Instance.infoRequest,
               
            };

            PlayFabClientAPI.LoginWithEmailAddress(req,
            res =>
            {
                GetUserInfo(UI.UIStateManager.Instance.GetEmailSignIn, res.PlayFabId);
                PlayFabManager.Instance.isSignIn = true;
                PlayFabManager.Instance.KC = res.InfoResultPayload.UserVirtualCurrency["KC"]; // to get the user virtual currency from playfab portal

                //! calling the function from Inventory System script
                //InventorySystem.Instance.BuyItem()
                InventorySystem.Instance.GetItemPrice();
                InventorySystem.Instance.UpdateInventory();
                foreach(GameObject obj in InventorySystem.Instance.enableGameObject)
                {
                    obj.SetActive(true);
                }

                PlayerStats.Instance.SetUserData();
                PlayerStats.Instance.GetUserData(res.PlayFabId);
                MonsterStats.Instance.GetTitleData();

                OnLoggedIn?.Invoke();
            },
            err =>
            {
                Debug.Log("Error: " + err.ErrorMessage);
            });
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
                    Data.PlayfabAccountInfo.FillData(res.AccountInfo, UI.UIStateManager.Instance.GetPass);
                },
                err => { });
        }
    }
}

[System.Serializable]
public class UserRegisterInfo
{
    public string username;
    public string email;
    public string password;
    public string confirmPassword;
}