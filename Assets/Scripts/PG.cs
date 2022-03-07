// Murat Sancak

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PG:MonoBehaviour // PG: Play Games.
{
    public static PG pG; // pG: Play Games.

    // Murat Sancak

    private void Awake()
    {
        if(pG is null)
            pG=this;
        else if(pG!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(pG);
    }

    private void Start()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled=true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate
        (
            (bool success) =>
            {
                if(success)
                {
                    ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);

                    if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex is 0) // Menu.
                        Menu.g.interactable=Menu.s.interactable=true;
                }
            }
        );
    }

    // Murat Sancak

    public static void Achievement(int a) // a: Achievement.
    {
        if(Social.localUser.authenticated)
        {
            switch(a)
            {
                case 8:
                    Social.ReportProgress(PGS.achievement_8,100,success => { });
                    break;
                case 9:
                    Social.ReportProgress(PGS.achievement_9,100,success => { });
                    break;
                case 10:
                    Social.ReportProgress(PGS.achievement_10,100,success => { });
                    break;
                case 11:
                    Social.ReportProgress(PGS.achievement_11,100,success => { });
                    break;
                case 12:
                    Social.ReportProgress(PGS.achievement_12,100,success => { });
                    break;
                case 13:
                    Social.ReportProgress(PGS.achievement_13,100,success => { });
                    break;
                case 14:
                    Social.ReportProgress(PGS.achievement_14,100,success => { });
                    break;
                case 132:
                    Social.ReportProgress(PGS.achievement_132,100,success => { });
                    break;
                default:
                    break;
            }
        }
    }
    public static void Achievements()
    {
        if(Social.localUser.authenticated)
            Social.ShowAchievementsUI();
    }

    public static void Leaderboard(int s) // s: Score.
    {
        if(Social.localUser.authenticated)
            Social.ReportScore(s,PGS.leaderboard_dice,success => { });
    }
    public static void Leaderboards() => Social.ShowLeaderboardUI();
}

// Murat Sancak