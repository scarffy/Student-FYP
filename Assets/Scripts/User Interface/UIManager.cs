//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using System;

//public class UIManager : MonoBehaviour
//{
//    public static UIManager Instance;

//    [Header("Screens")]
//    public GameObject loginPanel;
//    public GameObject registerPanel;

//    [Header("Login Screen")]
//    public TMP_InputField loginEmailField;
//    public TMP_InputField loginPasswordField;
//    public Button loginBtn;
//    public Button registerBtn;

//    [Header("Register Screen")]
//    public TMP_InputField registerEmailField;
//    public TMP_InputField registerUsernameField;
//    public TMP_InputField registerPasswordField;
//    public Button registerAccountBtn;
//    public Button backBtn;

//    [Header("Error SignIn/Up")]
//    public TMP_Text errorLogin;
//    public TMP_Text errorSignUp;

//    public void UpdateUsername(TMP_InputField _username)
//    {
//        registerUsernameField =_username;
//    }

//    public void UpdateEmailAddress(TMP_InputField _emailAddress)
//    {
//        registerEmailField = _emailAddress;
//    }

//    public void CreateAccount()
//    {
//        UserAccountController.Instance.OnTryRegisterNewAccount();
//    }

 
//}
