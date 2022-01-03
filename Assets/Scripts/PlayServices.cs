// murasanca

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace murasanca
{
    public class PlayServices : MonoBehaviour
    {
        public static PlayServices playServices;

        private void Awake()
        {
            if (playServices is null)
                playServices = this;
            else if (playServices != this)
                Destroy(gameObject);
            DontDestroyOnLoad(playServices);
        }

        private void Start()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.CENTER_HORIZONTAL);
            });
        }

        public static void AddScoreToLeaderboard(int score)
        {
            if (Social.localUser.authenticated)
                Social.ReportScore(score, GPGSIds.leaderboard_dice, success => { });
        }

        public static void ShowLeaderboard()
        {
            if (Social.localUser.authenticated)
                Social.ShowLeaderboardUI();
        }

        public static void ShowAchievements()
        {
            if (Social.localUser.authenticated)
                Social.ShowAchievementsUI();
        }

        public static void UnlockAchievement()
        {
            if (Social.localUser.authenticated)
                Social.ReportProgress(GPGSIds.achievement_dice, 100, success => { });
        }
    }
}

// murasanca