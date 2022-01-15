//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using System;
//using PlayFab;
//using PlayFab.ClientModels;

//public class PlayfabControl : MonoBehaviour
//{
//    [SerializeField] GameObject signUpTab, logInTab, startPanel, HUD;
//    public TMP_Text username; // this is for sign up 
//    public TMP_Text userEmail; //this is for sign up
//    public TMP_Text userPassword; // this is for sign up
//    public TMP_Text userEmailLogin; //this is for sign in
//    public TMP_Text userPasswordLogin; //this is for sign in
//    public TMP_Text errorSignUp; //this is for failed to signup
//    public TMP_Text errorLogin; //this is for failed to signin

//    string encryptedPassword;

//    #region first attempt

//    public void SignUp()
//    {
//        var registerRequest = new RegisterPlayFabUserRequest {Email = userEmail.text, Password = userPassword.text, Username = username.name};
//        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, RegisterSuccess, RegisterError);
//    }

//    public void RegisterSuccess(RegisterPlayFabUserResult result)
//    {
//        errorSignUp.text = " ";
//        errorLogin.text = " ";
//        StartGame();
//    }

//public void RegisterError(PlayFabError error)
//{
//    errorSignUp.text = error.GenerateErrorReport();
//}

//    public void LogIn()
//    {
//        var request = new LoginWithEmailAddressRequest { Email = userEmailLogin.text, Password = userPasswordLogin.text };
//        PlayFabClientAPI.LoginWithEmailAddress(request, LogInSuccess, LogInSuccess);
//    }

//    private void LogInSuccess(PlayFabError obj)
//    {
//        throw new NotImplementedException();
//    }

//    public void LogInSuccess(LoginResult result)
//    {
//        errorSignUp.text = " ";
//        errorLogin.text = " ";
//        StartGame();
//    }
//    public void LogInError(PlayFabError error)
//    {
//        errorLogin.text = error.GenerateErrorReport();
//    }

//    void StartGame()
//    {
//        startPanel.SetActive(false);
//        HUD.SetActive(true);
//    }
//    #endregion
//    #region 2nd try
//    public void CreateAccount(string username, string userEmail, string userPassword)
//    {
//        PlayFabClientAPI.RegisterPlayFabUser
//        (
//            new RegisterPlayFabUserRequest()
//            {
//                Email = userEmail,
//                Username = username,
//                Password = userPassword,
//                RequireBothUsernameAndEmail = true
//            },
//        response =>
//        {
//            Debug.Log($"Successful Account Creation: {username}, {userEmail}");
//        },
//        error =>
//        {
//            Debug.Log($"Unuccessful Account Creation: {username}, {userEmail} \n {error.ErrorMessage}");
//        }
//       );
//    }
//}
//#endregion

