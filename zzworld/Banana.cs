using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
  public float bananaSpeed;
  private float randomizedSpeed = 0f;

  private float nextActionTime = 1f;
  private Vector3 targetPosition;
  private void FixedUpdate(){
      if(bananaSpeed > 0f){
          Swim();
      }
  }
  private void Swim(){
      if(Time.fixedTime >= nextActionTime){
          //radom speeds
          randomizedSpeed = bananaSpeed * UnityEngine.Random.Range(.5f, 1.5f);
          // pic random spawn
          targetPosition = Ballarea.ChooseRandomPosition(transform.parent.position, 100f, 260f, 2f,13f);
          //rotate to direction
          transform.rotation = Quaternion.LookRotation(targetPosition = transform.position, Vector3.up);

          // calculate the time to get ther
          float timetoGetThere = Vector3.Distance(transform.position, targetPosition) / randomizedSpeed;
          nextActionTime = Time.fixedTime + timetoGetThere;

      }
      else{
            //make sure we dont go off screen hopefully this time
            Vector3 moveVector = randomizedSpeed * transform.forward * Time.fixedDeltaTime;
          if(moveVector.magnitude <= Vector3.Distance(transform.position, targetPosition)){
              transform.position += moveVector;

          }
          else{
              transform.position = targetPosition;
              nextActionTime = Time.fixedTime;

          }
      }
  }
}
