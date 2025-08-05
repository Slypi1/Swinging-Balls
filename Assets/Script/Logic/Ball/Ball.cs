using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public Zone CurrentZone => _currentZone;
    public BallColorType ColorType => _colorType;
    public Action OnBallLanded;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float spawnForce;
    [SerializeField] private  Vector2 spawnDirection;
    
    private bool _hasLanded;
    private float _stuckTimer;
    private Vector2 _lastPosition;
    private Zone _currentZone;
    
    private BallColorType _colorType;
  
    private readonly Dictionary<BallColorType, Color> _colors = new Dictionary<BallColorType, Color>
    {
        { BallColorType.Red, Color.red },
        { BallColorType.Green, Color.green },
        { BallColorType.Blue, Color.blue }
    };

    public enum BallColorType { Red, Green, Blue }

    public void Initialize()
    {
        _colorType = GetRandomColorType();
        _spriteRenderer.color = _colors[_colorType];
    }
    
    public void Drop()
    {
        transform.SetParent(null);
        Down();
        
        OnBallLanded?.Invoke();
    }

    public void SetZone(Zone zone)
    {
        _currentZone = zone;
    }
    
    private BallColorType GetRandomColorType()
    {
        var values = Enum.GetValues(typeof(BallColorType));
        return (BallColorType)values.GetValue(Random.Range(0, values.Length));
    }
    
    public void ThrowDown()
    {
        _rb.isKinematic = false;
        Down();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasLanded) return;
        
        Zone zone = collision.gameObject.GetComponent<Zone>();
        Ball ball = collision.gameObject.GetComponent<Ball>();
        
        if (zone != null)
        {
            zone.AddBall(this);
            LandOnZone();
        }

        if (ball != null && ball.CurrentZone != null)
        {
            ball.CurrentZone.AddBall(this);
            LandOnZone();
        }
    }
    
    private void Update()
    {
        CheckStuckCondition();
    }

    public void CheckStuckCondition()
    {
        if (_currentZone != null) return;

    
        if (Vector2.Distance(_lastPosition, _rb.position) < 0.01f)
        {
            _stuckTimer += Time.deltaTime;
            
            if (_stuckTimer > 0.5f)
            {
                ForceUnstuck();
            }
        }
        else
        {
            _stuckTimer = 0;
        }

        _lastPosition = _rb.position;
    }
    
    private void ForceUnstuck()
    {
        Down();
        _stuckTimer = 0;
    }
    
    private void LandOnZone()
    {
        _hasLanded = true;
        _rb.linearVelocity = Vector2.zero;
        _rb.isKinematic = true;
        
    }
    
    private void Down() => _rb.AddForce(spawnDirection * spawnForce, ForceMode2D.Impulse);
}
