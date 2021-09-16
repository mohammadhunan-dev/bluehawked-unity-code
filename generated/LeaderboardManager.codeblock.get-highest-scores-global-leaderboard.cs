topStats = realm.All<Stat>().OrderByDescending(s => s.Score).ToList();
st scores to the lowest scores
