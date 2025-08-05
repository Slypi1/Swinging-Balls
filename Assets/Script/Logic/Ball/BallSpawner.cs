using System;
using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{ 
    public Ball CurrentBall => _currentBall;

   [SerializeField] private Ball _ballPrefab;

   private Ball _currentBall;
   
   public void Initialize()
   {
       SpawnBall();
   }

   private void SpawnBall()
   {
       _currentBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity, transform);
       _currentBall.Initialize();
       
       _currentBall.OnBallLanded += HandleBallLanded;
   }
   
   private void HandleBallLanded()
   {
       _currentBall.OnBallLanded -= HandleBallLanded;
       
       StartCoroutine(SpawnNewBallWithDelay(0.5f));
   }

   private IEnumerator SpawnNewBallWithDelay(float delay)
   {
       yield return new WaitForSeconds(delay);
       SpawnBall();
   }
}
