using System;

namespace Habits
{
    [Serializable]
    public class HabitData
    {
        public string Name;
        public int Duration;
        public int DaysChecked;
        public string LastDayChecked = String.Empty;
        public int EggId;
        public bool Hatched;
    }
}