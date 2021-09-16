var finalScore = calculatePoints();
// calculate final points + write to realm with points
realm.Write(() =>
{
    currentStat.Score = finalScore;
});
