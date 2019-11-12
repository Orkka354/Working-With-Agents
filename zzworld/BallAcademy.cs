using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BallAcademy : Academy
{
  private Ballarea[] ballareas;

  public override void AcademyReset(){
      //get the ball onto the platform
        if(ballareas == null){
            ballareas = FindObjectsOfType<Ballarea>();
        }
        //set up ball land
        foreach(Ballarea ballarea in ballareas){
            ballarea.bananaSpeed = resetParameters["banana_speed"];
            ballarea.goalRadius = resetParameters["goal_radius"];
            ballarea.ResetArea();
        }
  }
}
