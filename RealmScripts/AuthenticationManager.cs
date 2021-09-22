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

    // Start() is a method inherited from MonoBehavior and is called on the frame when a script is enabled
    // Start() defines AuthenticationScreen UI elements, and sets click event handlers for them
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        authWrapper = root.Q<VisualElement>("auth-wrapper");
        subtitle = root.Q<Label>("subtitle");
        startButton = root.Q<Button>("start-button");
        logoutButton = root.Q<Button>("logout-button");
        userInput = root.Q<TextField>("username-input");

        logoutButton.clicked += RealmController.LogOut;
        startButton.clicked += () =>
        {
            onPressLogin();
        };
    }


    // onPressLogin() is a method that passes the username to the RealmController, ScoreCardManager, and LeaderboardManager
    private static void onPressLogin()
    {
        try
        {
            root.AddToClassList("hide");
            loggedInUser = userInput.value;
            RealmController.setLoggedInUser(loggedInUser);
            ScoreCardManager.setLoggedInUser(loggedInUser);
            LeaderboardManager.Instance.setLoggedInUser(loggedInUser);
        }
        catch (Exception ex)
        {
            Debug.Log("an exception was thrown:" + ex.Message);
        }
    }


}

