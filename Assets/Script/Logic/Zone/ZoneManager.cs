using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ZoneManager : MonoBehaviour
{
    [SerializeField] private List <Zone> zones;
    [SerializeField] private ParticleSystem destroyEffect;

    private int _maxBallZone = 3;
    private GameManager _gameManager;
    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        
        foreach (var zone in zones)
        {
            zone.Initialize(this);
        }
    }
    
    public void CheckAllMatches()
    {
        CheckHorizontalMatches();
        CheckVerticalMatches();
        CheckDiagonalMatches();
        CheckGameCompletion();
    }
    
    private void CheckHorizontalMatches()
    {
        foreach (var zone in zones)
        {
            if (zone.Balls.Count != _maxBallZone)
            {
                continue;
            }
            
            if (IsMatch(zone.Balls))
            {
                RemoveBalls(zone.Balls);
                zone.Clear();
                return;
            }
        }
    }
    
    private void CheckVerticalMatches()
    {
        for (int i = 1; i < _maxBallZone + 1; i++)
        {
            bool allZonesFull = zones.All(zone => zone.Balls.Count >= i);

            if (allZonesFull)
            {
                List<Ball> newBalls = new List<Ball>();
                
                foreach (var zone in zones)
                {
                    newBalls.Add(zone.Balls[i - 1]);
                }

                if (IsMatch(newBalls))
                {
                    RemoveBalls(newBalls);
                    foreach (var zone in zones)
                    {
                        zone.RemoveBall(zone.Balls[i - 1]);
                        zone.UpdateBallPositions();
                    }
                    return;
                }
            }
        }
        
    }
    private void CheckDiagonalMatches()
    {
        List<Ball> newBalls = new List<Ball>();
        
        for (int i = 0; i < zones.Count; i++)
        {
            if (zones[i].Balls.Count >= i + 1)
            {
                newBalls.Add(zones[i].Balls[i]);
            }
        }

        if (newBalls.Count == _maxBallZone && IsMatch(newBalls))
        {
            RemoveBalls(newBalls);
            for (int i = 0; i < zones.Count; i++)
            {
                zones[i].RemoveBall(zones[i].Balls[i]);
                zones[i].UpdateBallPositions();
            }
            return;
        }
        
        newBalls.Clear();
    
        for (int i = 0; i < zones.Count; i++)
        {
            if (zones[i].Balls.Count >= zones.Count - i)
            {
                newBalls.Add(zones[i].Balls[zones.Count - i - 1]);
            }
        }

        if (newBalls.Count == _maxBallZone && IsMatch(newBalls))
        {
            RemoveBalls(newBalls);
            for (int i = 0; i < zones.Count; i++)
            {
                zones[i].RemoveBall(zones[i].Balls[zones.Count - i - 1]);
                zones[i].UpdateBallPositions();
            }
        }
    }

    private void CheckGameCompletion()
    {
        bool allZonesFull = zones.All(zone => zone.Balls.Count == _maxBallZone);
        
        if (allZonesFull)
        {
            _gameManager.LoadScene();
        }
    }
    
    private bool IsMatch(List<Ball> balls)
    {
        if (balls.Count < _maxBallZone) return false;
    
        var firstColor = balls[0].ColorType;
        foreach (var ball in balls)
        {
            if (ball.ColorType != firstColor)
                return false;
        }
        return true;
    }
    
    private void RemoveBalls(List<Ball> balls)
    {
        ParticleSystem instance = Instantiate(destroyEffect, balls[1].transform.position, Quaternion.identity);
        instance.Play();
        
        _gameManager.ScoreManager.AddScore(balls[0].ColorType);
        
        foreach (var ball in balls)
        {
            Destroy(ball.gameObject);
        }
        
        Destroy(instance.gameObject, instance.main.duration);
    }
}


