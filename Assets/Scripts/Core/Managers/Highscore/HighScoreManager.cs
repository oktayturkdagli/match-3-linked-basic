using UnityEngine;

namespace Match3Linked.Core
{
    public class HighScoreManager : Singleton<HighScoreManager>
    {
        private const string Key = "HighScore";
        
        public int HighScore
        {
            get => PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetInt(Key) : 0;
            set
            {
                PlayerPrefs.SetInt(Key, value);
                PlayerPrefs.Save();
            }
        }
    }
}