// murasanca

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace murasanca
{
    public class PG : MonoBehaviour // PG: Play Games.
    {
        public static PG pG;

        // murasanca

        private void Awake()
        {
            if (pG is null)
                pG = this;
            else if (pG != this)
                Destroy(gameObject);
            DontDestroyOnLoad(pG);
        }

        private void Start()
        {
            PlayGamesClientConfiguration pGCC = new PlayGamesClientConfiguration.Builder().Build(); // pGCC: Play Games Client Configuration.
            PlayGamesPlatform.InitializeInstance(pGCC);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
            });
        }

        // murasanca

        public static void Achievements()
        {
            if (Social.localUser.authenticated)
                Social.ShowAchievementsUI();
        }

        public static void Leaderboard()
        {
            if (Social.localUser.authenticated)
                Social.ShowLeaderboardUI();
        }

        public static void Progress(int achievement)
        {
            if (Social.localUser.authenticated)
            {
                switch (achievement)
                {
                    case 8:
                        Social.ReportProgress(PGS.achievement_8, 100, success => { });
                        break;
                    case 9:
                        Social.ReportProgress(PGS.achievement_9, 100, success => { });
                        break;
                    case 10:
                        Social.ReportProgress(PGS.achievement_10, 100, success => { });
                        break;
                    case 11:
                        Social.ReportProgress(PGS.achievement_11, 100, success => { });
                        break;
                    case 12:
                        Social.ReportProgress(PGS.achievement_12, 100, success => { });
                        break;
                    case 13:
                        Social.ReportProgress(PGS.achievement_13, 100, success => { });
                        break;
                    case 14:
                        Social.ReportProgress(PGS.achievement_14, 100, success => { });
                        break;
                    case 132:
                        Social.ReportProgress(PGS.achievement_132, 100, success => { });
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Score(int score)
        {
            if (Social.localUser.authenticated)
                Social.ReportScore(score, PGS.leaderboard_dice, success => { });
        }
    }
}

// murasanca