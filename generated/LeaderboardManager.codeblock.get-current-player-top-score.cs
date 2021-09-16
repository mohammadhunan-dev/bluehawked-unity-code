var realmPlayer = realm.All<Player>().Where(p => p.Name == username).First();
var realmPlayerTopStat = realmPlayer.Stats.OrderByDescending(s => s.Score).First().Score;
return realmPlayer.Stats.OrderByDescending(s => s.Score).First().Score;
