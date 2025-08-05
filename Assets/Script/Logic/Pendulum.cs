using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float _leght;
    [SerializeField] private float _maxAngle;
    
    private float _angle; 
    private float _angularVelocity;
    
    private float _gravity = 9.81f; 
    
    private void Start()
    {
        _angle = Mathf.Deg2Rad * _maxAngle;
    }

   private void FixedUpdate()
    {
        float angularAcceleration = -(_gravity /_leght) * Mathf.Sin(_angle);
        
        _angularVelocity += angularAcceleration * Time.fixedDeltaTime;
        _angle += _angularVelocity * Time.fixedDeltaTime;
        
        transform.Rotate(new Vector3(0, 0, _angle));
    } 
}

