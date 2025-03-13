using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int moneyCount;
        public float carVolume;
        public float musicVolume;
        public int maxScore;
        public List<CarSaveData> cars = new List<CarSaveData>();
        public List<string> userCarsName = new List<string>();
    }
}