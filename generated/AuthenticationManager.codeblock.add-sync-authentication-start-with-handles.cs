void Start()
{
    root = GetComponent<UIDocument>().rootVisualElement;

    subtitle = root.Q<Label>("subtitle");
    startButton = root.Q<Button>("start-button");
    userInput = root.Q<TextField>("username-input");
    passInput = root.Q<TextField>("password-input");
    passInput.isPasswordField = true; // sync line
    toggleLoginOrRegisterUIButton = root.Q<Button>("toggle-login-or-register-ui-button");

    toggleLoginOrRegisterUIButton.clicked += () =>
    {
        // if the registerUI is already visible, switch to the loginUI and set isShowingRegisterUI to false	
        if (isShowingRegisterUI == true)
        {
            switchToLoginUI();
            isShowingRegisterUI = false;
        }
        else
        {
            switchToRegisterUI();
            isShowingRegisterUI = true;
        }
    };

    startButton.clicked += async () =>
    {
        if (isShowingRegisterUI == true)
        {
            onPressRegister();
        }
        else
        {
            onPressLogin();
        }
    };
}
