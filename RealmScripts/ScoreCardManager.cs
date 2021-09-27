using Realms;
using UnityEngine;
using UnityEngine.UIElements;
using System.ComponentModel;

public class ScoreCardManager : MonoBehaviour
{
    private Realm realm;
    private VisualElement root;
    private Label scoreCardHeader;
    private string username;
    private Stat currentStat;

    private PropertyChangedEventHandler propertyHandler;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        scoreCardHeader = root.Q<Label>("score-card-header");
    }

    // setLoggedInUser() is a method that sets values that are displayed in the ScoreCard UI, such as the username and current Stat,
    // and calls WatchForChangesToCurrentStats to watch for changes to the current Stat object
    public void SetLoggedInUser(string loggedInUser)
    {
        username = loggedInUser;
        currentStat = FindObjectOfType<RealmController>().currentStat;
        UpdateCurrentStats(); // set initial stats
        WatchForChangesToCurrentStats();
    }
    // updateCurrentStats() is a method that updates the EnemiesDefeated,TokensCollected, and Score in the UI
    public void UpdateCurrentStats() // updates stats in UI
    {
        scoreCardHeader.text = username + "\n" +
        "Enemies Defeated: " + currentStat.EnemiesDefeated + "\n" +
        "Tokens Collected: " + currentStat.TokensCollected + "\n" +
        "Current Score: " + currentStat.Score;
    }

    // WatchForChangesToCurrentStats() is a method that defines a property handler on the current playthrough Stat object
    public void WatchForChangesToCurrentStats()
    {
        // create a listener that responds to changes to the particular stats for this run/playthrough
        propertyHandler = new PropertyChangedEventHandler((sender, e) => UpdateCurrentStats());
        currentStat.PropertyChanged += propertyHandler;
    }
    // UnRegisterListener() is a method that removes a property handler on the current playthrough Stat object
    // and resets the ScoreCard UI to it's initial values
    public void UnRegisterListener()
    {
        // unregister when the player has lost
        currentStat.PropertyChanged -= propertyHandler;
        scoreCardHeader.text = username + "\n" +
        "Enemies Defeated: " + 0 + "\n" +
        "Tokens Collected: " + 0 + "\n" +
        "Current Score: " + 0;

    }
    // SetCurrentStat() is a method that sets the current playthrough Stat object
    // and calls updateCurrentStats() to update the UI
    public void SetCurrentStat(Stat newStat)
    {
        // called when the game has reset
        currentStat = newStat;
        UpdateCurrentStats();
    }
}