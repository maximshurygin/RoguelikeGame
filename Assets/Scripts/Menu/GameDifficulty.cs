namespace Menu
{
    public enum DifficultyEnum
    {
        Normal,
        Hard
    }
    
    public class GameDifficulty
    {
        private DifficultyEnum _difficulty = DifficultyEnum.Normal;
        public DifficultyEnum Difficulty => _difficulty;


        public void SetDifficulty(DifficultyEnum difficulty)
        {
            _difficulty = difficulty;
        }
    }
}