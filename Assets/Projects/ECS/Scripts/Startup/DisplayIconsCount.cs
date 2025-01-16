using TMPro;

namespace ECSProject
{
    public class DisplayIconsCount
    {
        private TextMeshProUGUI _archerCountText;
        private TextMeshProUGUI _swordsmenCountText;

        public DisplayIconsCount(TextMeshProUGUI archerCountText, TextMeshProUGUI swordsmenCountText)
        {
            _archerCountText = archerCountText;
            _swordsmenCountText = swordsmenCountText;
        }

        public void DisplayCount(int archerCount, int swordsmenCount)
        {
            _archerCountText.text = archerCount.ToString();
            _swordsmenCountText.text = swordsmenCount.ToString();
        }
    }
}