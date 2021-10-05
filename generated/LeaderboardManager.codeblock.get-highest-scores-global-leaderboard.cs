topStats = realm.All<Stat>().OrderByDescending(s => s.Score).ToList();
