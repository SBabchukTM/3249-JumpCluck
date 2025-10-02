using System;
using Habits;

namespace Game.UserData
{
    [Serializable]
    public class UserData
    {
        public SettingsData SettingsData = new();
        public TutorialData TutorialData = new();
        public UserInventoryData UserInventoryData = new();
        public UserHabitsData UserHabitsData = new(); 
    }
}