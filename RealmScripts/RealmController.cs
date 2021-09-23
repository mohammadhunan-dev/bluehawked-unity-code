using UnityEngine;
using Realms;
using System.Threading.Tasks;
using Realms.Sync;
using UnityEngine.SceneManagement;
using MongoDB.Bson;
using System.Linq;
public class RealmController : MonoBehaviour
{
    private static Realm realm;
    private static int runTime; // total amount of time you've been playing during this playthrough/run (losing/winning resets runtime)
    private static int bonusPoints = 0; // start with 0 bonus points and at the end of the game we add bonus points based on how long you played

    private static Player currentPlayer; // the Player object for the current playthrough
    public static Stat currentStat; // the Stat object for the current playthrough

    private static App realmApp = App.Create(Constants.Realm.AppId); // (Part 2 Sync): realmApp represents the MongoDB Realm backend application
    public static User syncUser; // (Part 2 Sync): syncUser represents the realmApp's currently logged in user


    private void Start()
    {
        // Attach C# Scripts to UI GameObjects
        var authScreen = GameObject.Find("AuthenticationScreen");
        authScreen.AddComponent<AuthenticationManager>();

        var leaderboard = GameObject.Find("Leaderboard");
        leaderboard.AddComponent<LeaderboardManager>();

        var scorecard = GameObject.Find("Scorecard");
        scorecard.AddComponent<ScoreCardManager>();

    }

    // GetRealm() is a method that returns a realm instance
    private static Realm GetRealm()
    {
        // TODO: open a realm and return it
        return null;
    }

    // setLoggedInUser() is a method that finds a Player object and creates a new Stat object for the current playthrough
    // setLoggedInUser() takes a userInput, representing a username, as a parameter
    public static void setLoggedInUser(string userInput)
    {
        realm = GetRealm();
        // TODO: "Set the `currentPlayer` variable by querying the realm for the
        // player. If the player exists, give the player a new Stat object,
        // otherwise create a new player and give the new player a new Stat
        // object
        startGame();
    }




    // LogOut() is an asynchronous method that logs out and reloads the scene
    public static async void LogOut()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // startGame() is a method that records how long the player has been playing during the current playthrough (i.e since logging in or since last losing or winning)
    private static void startGame()
    {
        // execute a timer every 10 second
        var myTimer = new System.Timers.Timer(10000);
        myTimer.Enabled = true;
        myTimer.Elapsed += (sender, e) => runTime += 10; // increment runTime (runTime will be used to calculate bonus points once the player wins the game)
    }

    // collectToken() is a method that performs a write transaction to update the current playthrough Stat object's TokensCollected count
    public static void collectToken()
    {
        // TODO: within a write transaction, increment the number of token's collected in the current playthrough/run's stat
    }
    // defeatEnemy() is a method that performs a write transaction to update the current playthrough Stat object's enemiesDefeated count
    public static void defeatEnemy()
    {
        // TODO: within a write transaction, increment the number of enemies's defeated in the current playthrough/run's stat
    }

    // deleteCurrentStat() is a method that performs a write transaction to delete the current playthrough Stat object and remove it from the current Player object's Stats' list
    public static void deleteCurrentStat()
    {
        ScoreCardManager.UnRegisterListener();
        // TODO: within a write transaction, delete the current Stat object, and it's reference in the current Player object
    }
    // restartGame() is a method that creates a new plathrough Stat object and shares this new Stat object with the ScoreCardManager to update in the UI and listen for changes to it
    public static void restartGame()
    {
        var s1 = new Stat();
        s1.StatOwner = currentPlayer;
        realm.Write(() =>
        {
            currentStat = realm.Add(s1);
            currentPlayer.Stats.Add(currentStat);
        });

        ScoreCardManager.SetCurrentStat(currentStat); // call `SetCurrentStat()` to set the current stat in the UI using ScoreCardManager
        ScoreCardManager.WatchForChangesToCurrentStats(); // call `WatchForChangesToCurrentStats()` to register a listener on the new score in the ScoreCardManager

        startGame(); // start the game by resetting the timer and officially starting a new run/playthrough
    }


    // playerWon() is a method that calculates and returns the final score for the current playthrough once the player has won the game
    public static int playerWon()
    {
        if (runTime <= 30) // if the game is beat in in less than or equal to 30 seconds, +80 bonus points
        {
            bonusPoints = 80;
        }
        else if (runTime <= 60) // if the game is beat in in less than or equal to 1 min, +70 bonus points
        {
            bonusPoints = 70;
        }
        else if (runTime <= 90) // if the game is beat in less than or equal to 1 min 30 seconds, +60 bonus points
        {
            bonusPoints = 60;
        }
        else if (runTime <= 120) // if the game is beat in less than or equal to 2 mins, +50 bonus points
        {
            bonusPoints = 50;
        }

        var finalScore = (currentStat.EnemiesDefeated + 1) * (currentStat.TokensCollected + 1) + bonusPoints;
        realm.Write(() =>
        {
            currentStat.Score = finalScore;
        });

        return finalScore;
    }
}