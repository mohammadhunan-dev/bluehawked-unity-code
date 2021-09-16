realm.Write(() =>
{
    realm.Remove(currentStat);
    currentPlayer.Stats.Remove(currentStat);
});
ove the reference from the currentPlayer object
