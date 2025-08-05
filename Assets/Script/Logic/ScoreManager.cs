using System.Collections.Generic;

public class ScoreManager
{
    public int TotalScore => _totalScore;

    private List<ColorScore> _colorScores = new List<ColorScore>
    {
        new ColorScore { colorType = Ball.BallColorType.Red, scoreValue = 10 },
        new ColorScore { colorType = Ball.BallColorType.Green, scoreValue = 15 },
        new ColorScore { colorType = Ball.BallColorType.Blue, scoreValue = 20}
    };
    
    private int _totalScore;
    
    public void AddScore(Ball.BallColorType colorType)
    {
        var colorScore = _colorScores.Find(c => c.colorType == colorType);
        if (colorScore != null)
        {
            _totalScore += colorScore.scoreValue;
        }
    }

    public void Clear()
    {
        _totalScore = 0;
    }

}
[System.Serializable]
public class ColorScore
{
    public Ball.BallColorType colorType;
    public int scoreValue;
}
    