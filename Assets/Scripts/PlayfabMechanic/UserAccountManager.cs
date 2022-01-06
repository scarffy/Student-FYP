using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;

public class UserAccountManager : MonoBehaviour
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
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(req,
        res =>
        {
            Debug.Log("Login Success");
            //masukgamescene
        },
        err =>
        {
            Debug.Log("Error: " + err.ErrorMessage);
            LoginBtn.interactable = true;
        });
    }
    #endregion
}
