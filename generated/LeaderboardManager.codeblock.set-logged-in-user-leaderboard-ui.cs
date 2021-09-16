public void setLoggedInUser(string loggedInUser)
{
    username = loggedInUser;

    realm = Realm.GetInstance();

    // only create the leaderboard on the first run, consecutive restarts/reruns will already have a leaderboard created
    if (isLeaderboardGUICreated == false)
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        createLeaderboardUI();
        root.Add(toggleButton);
        root.Add(displayTitle);
        root.Add(listView);
        isUIVisible = true;

        toggleUIVisible();

        toggleButton.clicked += () =>
        {
            toggleUIVisible();
        };
        setStatListener(); // start listening for score changes once the leaderboard GUI has launched
        isLeaderboardGUICreated = true;
    }
}

}
