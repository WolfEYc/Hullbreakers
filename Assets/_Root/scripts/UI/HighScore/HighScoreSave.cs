using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    public class HighScoreSave : MonoBehaviour
    {
        public TMP_Text texttoSave;
    
        public void SaveText()
        {
            GameMaster.Inst.highscoreMaster.SaveScore(GameMaster.Inst.lvlMaster.Score, texttoSave.text);    
        }
    }
}
