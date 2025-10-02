using System.Collections.Generic;
using Game.UserData;

namespace Habits
{
    public class HabitsService
    {
        private readonly SaveSystem _saveSystem;

        public HabitsService(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public List<HabitData> GetHabits() => _saveSystem.GetData().UserHabitsData.ActiveHabits;

        public HabitData AddNewHabit(string name, int days, int eggId)
        {
            var data = _saveSystem.GetData().UserHabitsData.ActiveHabits;

            var habit = new HabitData()
            {
                Name = name,
                DaysChecked = 0,
                Duration = days,
                EggId = eggId
            };
            
            data.Add(habit);
            
            _saveSystem.Save();
            
            return habit;
        }
    }
}