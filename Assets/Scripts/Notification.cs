// Murat Sancak

using System;
using Unity.Notifications.Android;
using UnityEngine;

public class Notification:MonoBehaviour
{
    public static Notification n; // n: Notification.

    // Murat Sancak

    private void Awake()
    {
        if(n is null)
            n=this;
        else if(n!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(n);
    }

    private void Start()
    {
        AndroidNotificationCenter.CancelAllNotifications();
        AndroidNotificationCenter.RegisterNotificationChannel
        (
            new("D","Dice","Just roll with it.",Importance.High)
            {
                CanBypassDnd=true,
                CanShowBadge=true,
                // Description = "Just roll with it.",
                EnableLights=true,
                EnableVibration=true,
                // Id = "D",
                // Importance = Importance.High,
                LockScreenVisibility=LockScreenVisibility.Public,
                // Name = "Dice",
                VibrationPattern=new long[8] { 0,1,2,4,8,16,32,64 }
            }
        );
        AndroidNotificationCenter.SendNotification
        (
            new("Dice","Just roll with it.",DateTime.Now.AddDays(1),new TimeSpan(864000000000),"small") // 864.000.000.000
                {
                Color=Color.cyan,
                CustomTimestamp=DateTime.Now.AddDays(1),
                // FireTime = DateTime.Now.AddDays(1),
                Group="Murat Sancak",
                GroupAlertBehaviour=GroupAlertBehaviours.GroupAlertAll,
                GroupSummary=true,
                IntentData="Dice",
                LargeIcon="large",
                Number=1,
                // RepeatInterval = new TimeSpan(864000000000), // 864.000.000.000
                ShouldAutoCancel=false,
                ShowTimestamp=true,
                // SmallIcon = "small",
                SortKey="Dice",
                Style=NotificationStyle.BigTextStyle,
                // Text = "Just roll with it.",
                // Title = "Dice",
                UsesStopwatch=false
            },
            "D"
        );
    }
}

// Murat Sancak