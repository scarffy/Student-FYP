using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;



namespace FYP.Backend
{
    /// <summary>
    /// Register and Login player
    /// </summary>
    public class UserAccountController : Singleton<UserAccountController>
    {
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
                Password = info.password
            };

            PlayFabClientAPI.RegisterPlayFabUser(req,
                res => {
                    UI.UIStateManager.Instance.SetState(UI.UIStateManager.State.none);
                },
                err => {
                    UI.UIStateManager.Instance.SetErrorState("Some error msg here");
                });
        }
        #endregion

        #region sign in
        public void OnTryLogin()
        {
            //string email = LoginEmailField.text;
            //string password = LoginPasswordField.text;
            
            //LoginBtn.interactable = false;

            LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
            {
                //Email = email,
                //Password = password,
                InfoRequestParameters = Data.PlayfabAccountInfo.Instance.infoRequest,
               
            };

            PlayFabClientAPI.LoginWithEmailAddress(req,
            res =>
            {
                //GetUserInfo(email, res.PlayFabId);
                Debug.Log("login success");
                Backend.PlayFabManager.Instance.isSignIn = true;
                Backend.PlayFabManager.Instance.KC = res.InfoResultPayload.UserVirtualCurrency["KC"]; // to get the user virtual currency from playfab portal
                Backend.InventorySystem.Instance.shopBag.SetActive(true);
                Backend.InventorySystem.Instance.inventoryBeg.SetActive(true);
                Backend.InventorySystem.Instance.virtualCoin.SetActive(true);
                Backend.PlayFabManager.Instance.playerStats.SetActive(true);


                //! calling the function from Inventory System script
                //InventorySystem.Instance.BuyItem()
                InventorySystem.Instance.GetItemPrice();
                InventorySystem.Instance.UpdateInventory();
                foreach(GameObject obj in InventorySystem.Instance.enableGameObject)
                {
                    obj.SetActive(true);
                }

                Backend.PlayerStats.Instance.SetUserData();
                Backend.PlayerStats.Instance.GetUserData(res.PlayFabId);
                Backend.MonsterStats.Instance.GetTitleData();

            },
            err =>
            {
                Debug.Log("Error: " + err.ErrorMessage);
                //LoginBtn.interactable = true;
                //ErrorLogin.text = err.GenerateErrorReport();
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
                    //Data.PlayfabAccountInfo.FillData(res.AccountInfo);
                    Data.PlayfabAccountInfo.FillData(res.AccountInfo, () => {
                        LevelManager.Instance.LoadNextLevel();
                    });
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