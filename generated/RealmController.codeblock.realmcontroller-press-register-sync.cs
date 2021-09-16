public static async Task<Player> OnPressRegister(string userInput, string passInput)
{
    await realmApp.EmailPasswordAuth.RegisterUserAsync(userInput, passInput);
    syncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
    realm = await GetRealm(syncUser);
    Debug.Log($"Realm is located at: {realm.Config.DatabasePath}");

    var p1 = new Player();
    p1.Id = syncUser.Id;
    p1.Name = userInput;

    var s1 = new Stat();
    s1.StatOwner = p1;


    realm.Write(() =>
    {
        currentPlayer = realm.Add(p1);
        currentStat = realm.Add(s1);
        currentPlayer.Stats.Add(currentStat);
    });

    startGame();

    Debug.Log("Game has started " + currentPlayer.Name);

    return currentPlayer;
}
