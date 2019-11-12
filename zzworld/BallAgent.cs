using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class BallAgent : Agent
{
  public GameObject heartPrefab;
  public GameObject regurgitatedBananaPrefab;

  private Ballarea ballArea;
  private Animator animator;
  private RayPerception3D rayPerception;
  private GameObject goal;
  private bool isFull; // goal completed

  public override void AgentAction(float[] vectorAction, string textAction){
      //Convert action to axis values
      float forward = vectorAction[0];// alows for discrte timesteps to decide between 0 and 1 only moes on 1.
      float leftOrRight = 0f;
      if (vectorAction[1] == 1f){
          leftOrRight = -1f;

      }
      else if (vectorAction[1] == 2f){
          leftOrRight = 1f;
      }
      //Later for animation
        //animator.SetFloat("Vertical",forward);
        //animator.SetFloat("Horizontal, leftOrRight);
      // negative rewards
      AddReward(-1f / agentParameters.maxStep);
  }
  public override void AgentReset(){
      isFull = false;
      ballArea.ResetArea();

  }
  public override void CollectObservations(){
      //has the banana been collected
      AddVectorObs(isFull);
      //distance to goal
      AddVectorObs(Vector3.Distance(goal.transform.position,transform.position));
      //Direction to goal
      AddVectorObs((goal.transform.position = transform.position).normalized);
      //direction of ball
      AddVectorObs(transform.forward);

      //a sphere of rays called raysperception(sight)
      float rayDistance = 20f;
      float [] rayAngles = {30f, 60f, 90f, 120f, 150f};
      string [] detectableObjects = {"goal","banana","wall"};
      AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));

  }
  private void Start(){
      ballArea = GetComponentInParent<Ballarea>();
      goal = ballArea.goal;
      //animator = GetComponent<Animator>();
      rayPerception = GetComponent<RayPerception3D>();

  }
  private void FixedUpdate(){
    if(Vector3.Distance(transform.position, goal.transform.position) < ballArea.goalRadius){
        //good enough just shove it into the goal
        TossintoGoal();
    }

  }
  private void OnCollisionEnter(Collision collision){
      if(collision.transform.CompareTag("banana")){
          Consumebanana(collision.gameObject);
      }
      else if (collision.transform.CompareTag("goal")){
          TossintoGoal();
      }
  }
  private void Consumebanana(GameObject bananaObject){
      if(isFull) return;//Ya get one banana thats it!
      isFull = true;
      ballArea.RemoveSpecificBanana(bananaObject);

      AddReward(1f);
  }
  private void TossintoGoal(){
      if(!isFull) return; //no bananas in inventory
      

      GameObject consumedBanana = Instantiate<GameObject>(regurgitatedBananaPrefab);
      consumedBanana.transform.parent = transform.parent;
      consumedBanana.transform.position = goal.transform.position;
      Destroy(consumedBanana, 4f);

      GameObject heart = Instantiate<GameObject>(heartPrefab);
      heart.transform.parent = transform.parent;
      heart.transform.position = goal.transform.position + Vector3.up;
      Destroy(heart, 4f);

      AddReward(1f);
      isFull = false;


    }
}
