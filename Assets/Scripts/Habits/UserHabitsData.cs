using System;
using System.Collections.Generic;

namespace Habits
{
    [Serializable]
    public class UserHabitsData
    {
        public List<HabitData> ActiveHabits = new()
        {
            new HabitData()
            {
                DaysChecked = 0,
                Duration = 30,
                EggId = 8,
                Hatched = true,
                Name = "Login Daily!"
            }
        };
    }
}