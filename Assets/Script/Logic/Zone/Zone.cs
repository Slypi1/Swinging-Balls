using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<Ball> Balls => _balls;
    
    private List<Ball> _balls = new List<Ball>();
    private ZoneManager _zoneManager;

    public void Initialize(ZoneManager zoneManager)
    {
        _zoneManager = zoneManager;
    }
    
    public void AddBall(Ball ball)
    {
        if (_balls.Count == 3)
        {
            Destroy(ball.gameObject);
            return;
        }
        
        if (!_balls.Contains(ball))
        {
            _balls.Add(ball);
            ball.SetZone(this);
            _zoneManager.CheckAllMatches();
        }
    }
    
    public void RemoveBall(Ball ball)
    {
        _balls.Remove(ball);
    }
    
    public void Clear()
    {
        _balls.Clear();
    }
    
    public void UpdateBallPositions()
    {
        foreach (var ball in _balls)
        {
            if (ball != null)
            {
                ball.ThrowDown();
            }
        }
    }
}
