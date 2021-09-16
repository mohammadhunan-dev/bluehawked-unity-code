public static async Task<Player> setLoggedInUser(string userInput, string passInput)
{        
    syncUser = await realmApp.LogInAsync(Credentials.EmailPassword(userInput, passInput));
    if (syncUser != null)
    {
        realm = await GetRealm(syncUser);
        currentPlayer = realm.Find<Player>(syncUser.Id);

        if (currentPlayer != null)
        {
            var s1 = new Stat();
            s1.StatOwner = currentPlayer;

            realm.Write(() =>
            {
                currentStat = realm.Add(s1);
                currentPlayer.Stats.Add(currentStat);
            });

            startGame();
        }
        else
        {
            Debug.Log("This player exists a MongoDB Realm User but not as a Realm Object, please delete the Sync User and create one ussing the register button");
        }
    }

    return currentPlayer;
}
