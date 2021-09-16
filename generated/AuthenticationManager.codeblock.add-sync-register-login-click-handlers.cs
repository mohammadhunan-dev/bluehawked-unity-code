public static async void onPressLogin()
{
    try
    {
        currentPlayer = await RealmController.setLoggedInUser(userInput.value, passInput.value);
        if (currentPlayer != null)
        {
            root.AddToClassList("hide");
        }
        ScoreCardManager.setLoggedInUser(currentPlayer.Name);
        LeaderboardManager.Instance.setLoggedInUser(currentPlayer.Name);
    }
    catch (Exception ex)
    {
        Debug.Log("an exception was thrown:" + ex.Message);
    }
}
public static async void onPressRegister()
{
    try
    {
        currentPlayer = await RealmController.OnPressRegister(userInput.value, passInput.value);

        if (currentPlayer != null)
        {
            root.AddToClassList("hide");
        }
        ScoreCardManager.setLoggedInUser(currentPlayer.Name);
        LeaderboardManager.Instance.setLoggedInUser(currentPlayer.Name);

    }
    catch (Exception ex)
    {
        Debug.Log("an exception was thrown:" + ex.Message);
    }
}
