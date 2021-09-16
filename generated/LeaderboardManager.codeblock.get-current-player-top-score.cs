// TODO: Query the realm instance for the current player, find the current player's top score and return that value
 return 0;
yDescending(s => s.Score).First().Score;
return realmPlayer.Stats.OrderByDescending(s => s.Score).First().Score;
