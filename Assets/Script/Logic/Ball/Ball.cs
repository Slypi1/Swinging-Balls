using System;
using System.Collections;
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
    [SerializeField] private float _spawnForce;
    [SerializeField] private  Vector2 _spawnDirection;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxTime;
    
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
    
    public void ThrowDown()
    {
        _rb.isKinematic = false;
        Down();
    }
    
    private BallColorType GetRandomColorType()
    {
        var values = Enum.GetValues(typeof(BallColorType));
        return (BallColorType)values.GetValue(Random.Range(0, values.Length));
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasLanded) return;
        
        Zone zone = collision.gameObject.GetComponent<Zone>();
        Ball ball = collision.gameObject.GetComponent<Ball>();
        
        StartCoroutine(CheckStuckCoroutine());
        
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

    private IEnumerator CheckStuckCoroutine() 
    {
        while (true) 
        {
            CheckStuckCondition();
            yield return new WaitForSeconds(_waitTime);
        }
    }

    private void CheckStuckCondition()
    {
        if (_currentZone != null) return;

    
        if (Vector2.Distance(_lastPosition, _rb.position) < _minDistance)
        {
            _stuckTimer += Time.deltaTime;
            
            if (_stuckTimer > _maxTime)
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
        StopCoroutine(CheckStuckCoroutine());
    }
    
    private void Down() => _rb.AddForce(_spawnDirection * _spawnForce, ForceMode2D.Impulse);
}
