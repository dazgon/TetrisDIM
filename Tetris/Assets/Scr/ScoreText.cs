using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text ScoreSisas;
    // Start is called before the first frame update
    void Start()
    {
        ScoreSisas = GetComponent<Text>();
        PieceLogic.updateScore += PieceLogic_updateScore;
        
    }

    private void PieceLogic_updateScore(int obj)
    {
        ScoreSisas.text = obj.ToString();
    }

    private void OnDestroy()
    {
        PieceLogic.updateScore -= PieceLogic_updateScore;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
