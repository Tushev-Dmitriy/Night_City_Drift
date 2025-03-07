using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int moneyCount;
        public float carVolume;
        public float musicVolume;
        public List<MainCarData> userCars = new List<MainCarData>();

        public List<CarSaveData> cars = new List<CarSaveData>();
    }
}