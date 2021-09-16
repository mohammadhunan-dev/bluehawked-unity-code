public static void setLoggedInUser(string userInput)
{
    realm = GetRealm();
    var matchedPlayers = realm.All<Player>().Where(p => p.Name == userInput);

    if (matchedPlayers.Count() > 0) // if the player exists
    {
        currentPlayer = matchedPlayers.First();
        var s1 = new Stat();
        s1.StatOwner = currentPlayer;

        realm.Write(() =>
        {
            currentStat = realm.Add(s1);
            currentPlayer.Stats.Add(currentStat);
        });
    }
    else
    {
        var p1 = new Player();
        p1.Id = ObjectId.GenerateNewId().ToString();
        p1.Name = userInput;

        var s1 = new Stat();
        s1.StatOwner = p1;

        realm.Write(() =>
        {
            currentPlayer = realm.Add(p1);
            currentStat = realm.Add(s1);
            currentPlayer.Stats.Add(currentStat);
        });
    }

    startGame();
}
