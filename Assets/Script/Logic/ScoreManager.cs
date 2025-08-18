using System.Collections.Generic;
using System;
public class ScoreManager
{
    public int TotalScore => _totalScore;

    private List<ColorScore> _colorScores = new List<ColorScore>
    {
        new ColorScore { ColorType = Ball.BallColorType.Red, ScoreValue = 10 },
        new ColorScore { ColorType = Ball.BallColorType.Green, ScoreValue = 15 },
        new ColorScore { ColorType = Ball.BallColorType.Blue, ScoreValue = 20}
    };
    
    private int _totalScore;
    
    public void AddScore(Ball.BallColorType colorType)
    {
        var colorScore = _colorScores.Find(c => c.ColorType == colorType);
        
        if (colorScore != null)
        {
            _totalScore += colorScore.ScoreValue;
        }
    }

    public void Clear()
    {
        _totalScore = 0;
    }

}
[Serializable]
public class ColorScore
{
    public Ball.BallColorType ColorType;
    public int ScoreValue;
}
    