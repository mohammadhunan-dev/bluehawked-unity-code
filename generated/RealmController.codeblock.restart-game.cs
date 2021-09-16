var s1 = new Stat();

realm.Write(() =>
{
    currentStat = realm.Add(s1);
    currentPlayer.Stats.Add(currentStat);
});
