using System;

namespace GameCore.Save
{
    [Serializable]
    public class SaveDataModel
    {
        public int Coins;
        public int Health;
        public int Speed;
        public int Regen;
        public int Range;
    }
}