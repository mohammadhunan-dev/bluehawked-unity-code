using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AuthenticationManager : MonoBehaviour
{

    private static VisualElement root;
    private static VisualElement authWrapper;
    private static Label subtitle;
    private static Button startButton;
    private static Button logoutButton;
    private static string loggedInUser;
    private static TextField userInput;

    private static bool isInRegistrationMode = false; // (Part 2 Sync): isInRegistrationMode is used to toggle between authentication modes
    private static TextField passInput; // (Part 2 Sync): passInput represents the password input
    private static Button toggleLoginOrRegisterUIButton; // (Part 2 Sync): toggleLoginOrRegisterUIButton is the button to toggle between login or registration modes

    #region PrivateMethods
    // OnPressLogin() is an asynchronous method that calls RealmController.SetLoggedInUser to login with the values from the userInput and passInput
    // and passes the currentPlayer to ScoreCardManager and LeaderboardManager; once logged in the login screen is hidden and the logout button is shown
    private static async void OnPressLogin()
    {
        try
        {
            var currentPlayer = await RealmController.SetLoggedInUser(userInput.value, passInput.value);
            if (currentPlayer != null)
            {
                authWrapper.AddToClassList("hide");
                logoutButton.AddToClassList("show");
            }
            ScoreCardManager.SetLoggedInUser(currentPlayer.Name);
            LeaderboardManager.Instance.SetLoggedInUser(currentPlayer.Name);
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }
    // OnPressRegister() is a method that passes RealmController.OnPressRegister() the
    // values of the userInput and  passInput TextFields in order to register a user
    private static async void OnPressRegister()
    {
        try
        {
            var currentPlayer = await RealmController.OnPressRegister(userInput.value, passInput.value);

            if (currentPlayer != null)
            {
                authWrapper.AddToClassList("hide");
                logoutButton.AddToClassList("show");
            }
            ScoreCardManager.SetLoggedInUser(currentPlayer.Name);
            LeaderboardManager.Instance.SetLoggedInUser(currentPlayer.Name);

        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }


    // SwitchToLoginUI() is a method that switches the UI to the Login UI mode
    private static void SwitchToLoginUI()
    {
        subtitle.text = "Login";
        startButton.text = "Login & Start Game";
        toggleLoginOrRegisterUIButton.text = "Don't have an account yet? Register";
    }
    // SwitchToRegisterUI() is a method that switches the UI to the Register UI mode
    private static void SwitchToRegisterUI()
    {
        subtitle.text = "Register";
        startButton.text = "Signup & Start Game";
        toggleLoginOrRegisterUIButton.text = "Have an account already? Login";
    }
    #endregion

    #region UnityLifecycleMethods
    // Start() is a method inherited from MonoBehavior and is called on the frame when a script is enabled
    // Start() defines AuthenticationScreen UI elements, and sets click event handlers for them
    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        authWrapper = root.Q<VisualElement>("auth-wrapper");
        logoutButton = root.Q<Button>("logout-button");
        subtitle = root.Q<Label>("subtitle");
        startButton = root.Q<Button>("start-button");
        userInput = root.Q<TextField>("username-input");
        passInput = root.Q<TextField>("password-input");
        passInput.isPasswordField = true;

        logoutButton.clicked += RealmController.LogOut;
    //  when the start button is clicked, toggle between registration modes
        startButton.clicked += () =>
        {
            if (isInRegistrationMode == true)
            {
                OnPressRegister();
            }
            else
            {
                OnPressLogin();
            }
        };
        toggleLoginOrRegisterUIButton = root.Q<Button>("toggle-login-or-register-ui-button");
        toggleLoginOrRegisterUIButton.clicked += () =>
        {
            // if already in registration mode, switch to the login mode and set isInRegistrationMode to false
            if (isInRegistrationMode == true)
            {
                SwitchToLoginUI();
                isInRegistrationMode = false;
            }
            else
            {
                SwitchToRegisterUI();
                isInRegistrationMode = true;
            }
        };
    }
    #endregion

}

