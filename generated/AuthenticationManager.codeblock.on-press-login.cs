public static void onPressLogin()
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
