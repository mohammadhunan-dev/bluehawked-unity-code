using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AuthenticationManager : MonoBehaviour
{
    public AuthenticationManager Instance;

    private VisualElement root;
    private VisualElement authWrapper;
    private Label subtitle;
    private Button startButton;
    private Button logoutButton;
    private string loggedInUser;
    private TextField userInput;

    private bool isInRegistrationMode = false; // (Part 2 Sync): isInRegistrationMode is used to toggle between authentication modes
    private TextField passInput; // (Part 2 Sync): passInput represents the password input
    private Button toggleLoginOrRegisterUIButton; // (Part 2 Sync): toggleLoginOrRegisterUIButton is the button to toggle between login or registration modes

    // Start() is a method inherited from MonoBehavior and is called on the frame when a script is enabled
    // Start() defines AuthenticationScreen UI elements, and sets click event handlers for them

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        authWrapper = root.Q<VisualElement>("auth-wrapper");
        logoutButton = root.Q<Button>("logout-button");
        subtitle = root.Q<Label>("subtitle");
        startButton = root.Q<Button>("start-button");
        userInput = root.Q<TextField>("username-input");
        passInput = root.Q<TextField>("password-input");
        passInput.isPasswordField = true;

        logoutButton.clicked += FindObjectOfType<RealmController>().LogOut;
        //  when the start button is clicked, toggle between registration modes
        startButton.clicked += () =>
        {
            if (isInRegistrationMode == true)
            {
                onPressRegister();
            }
            else
            {
                onPressLogin();
            }
        };
        toggleLoginOrRegisterUIButton = root.Q<Button>("toggle-login-or-register-ui-button");
        toggleLoginOrRegisterUIButton.clicked += () =>
        {
            // if already in registration mode, switch to the login mode and set isInRegistrationMode to false
            if (isInRegistrationMode == true)
            {
                switchToLoginUI();
                isInRegistrationMode = false;
            }
            else
            {
                switchToRegisterUI();
                isInRegistrationMode = true;
            }
        };
    }

    // switchToLoginUI() is a method that switches the UI to the Login UI mode
    private void switchToLoginUI()
    {
        subtitle.text = "Login";
        startButton.text = "Login & Start Game";
        toggleLoginOrRegisterUIButton.text = "Don't have an account yet? Register";
    }
    // switchToRegisterUI() is a method that switches the UI to the Register UI mode
    private void switchToRegisterUI()
    {
        subtitle.text = "Register";
        startButton.text = "Signup & Start Game";
        toggleLoginOrRegisterUIButton.text = "Have an account already? Login";
    }



    // onPressLogin() is an asynchronous method that calls RealmController.setLoggedInUser to login with the values from the userInput and passInput
    // and passes the currentPlayer to ScoreCardManager and LeaderboardManager; once logged in the login screen is hidden and the logout button is shown
    private async void onPressLogin()
    {
        try
        {
            var currentPlayer = await FindObjectOfType<RealmController>().setLoggedInUser(userInput.value, passInput.value);
            if (currentPlayer != null)
            {
                root.AddToClassList("hide");
            }
            FindObjectOfType<ScoreCardManager>().setLoggedInUser(currentPlayer.Name);
            FindObjectOfType<LeaderboardManager>().setLoggedInUser(currentPlayer.Name);
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }
    // onPressRegister() is a method that passes RealmController.OnPressRegister() the
    // values of the userInput and  passInput TextFields in order to register a user
    private async void onPressRegister()
    {
        try
        {
            var currentPlayer = await FindObjectOfType<RealmController>().OnPressRegister(userInput.value, passInput.value);

            if (currentPlayer != null)
            {
                root.AddToClassList("hide");
            }
            FindObjectOfType<ScoreCardManager>().setLoggedInUser(currentPlayer.Name);
            FindObjectOfType<LeaderboardManager>().setLoggedInUser(currentPlayer.Name);

        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }
}

