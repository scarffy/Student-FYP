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
        public Action OnFoundInfo;

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
                    localSaveFile.email = info.email;
                    localSaveFile.password = info.password;

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
                UIStateManager.Instance.SetState(UIStateManager.State.loading);
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
               GetUserInfo(saveData.email, res.PlayFabId, saveData.password);
               PlayerStats.Instance.GetUserData(res.PlayFabId, "PlayerGender", null,res => 
               {
                   if (res.Contains("True"))
                       GenderSelection.Instance.SelectGender(true);
                   else
                       GenderSelection.Instance.SelectGender(false);
               });
               PlayFabManager.Instance.isSignIn = true;
               PlayFabManager.Instance.KC = res.InfoResultPayload.UserVirtualCurrency["KC"]; // to get the user virtual currency from playfab portal

               InventorySystem.Instance.GetInventory();

               PlayerStats.Instance.SetUserData();
               PlayerStats.Instance.GetUserData(res.PlayFabId);
               MonsterStats.Instance.GetTitleData();

               OnLoggedIn?.Invoke();
               UIStateManager.Instance.SetState(UIStateManager.State.single);

           },
           err =>
           {
               Debug.Log("Error: " + err.ErrorMessage + " | Parameter: " + saveData.email + "," + saveData.password);
           });
        }

        #endregion

        void GetUserInfo(string email, string playfabId, string pass)
        {
            GetAccountInfoRequest req = new GetAccountInfoRequest
            {
                Email = email,
                PlayFabId = playfabId,
            };

            PlayFabClientAPI.GetAccountInfo(req,
                res =>
                {
                    OnFoundInfo?.Invoke();

                    if (!string.IsNullOrEmpty(pass))
                    {
                        Data.PlayfabAccountInfo.FillData(res.AccountInfo, pass);
                    }
                    else Debug.Log("Can't save data. Password field is empty");
                },
                err => {
                    Debug.LogError("Get User Info error : " + err.GenerateErrorReport());
                });
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