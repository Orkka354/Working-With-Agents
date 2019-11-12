using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;
using System;

public class Ballarea : Area
{
  public BallAgent ballAgent;
  public GameObject goal;
  public Banana bananaPrefab;
  public TextMesh cumulativerewardtext;

  [HideInInspector]
  public float bananaSpeed = 0;
  [HideInInspector]
  public float goalRadius = 0f;

  private List<GameObject> bananaList;

  public override void ResetArea(){
      RemoveAllBananas();
      PlaceBall();
      PlaceGoal();
      SpawnBanana(4,bananaSpeed);


  }
  public void RemoveSpecificBanana(GameObject bananaObject){
      bananaList.Remove(bananaObject);
      Destroy(bananaObject);
  }
  public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius){
      float radius = minRadius;
      if(maxRadius > minRadius){
          radius = UnityEngine.Random.Range(minRadius,maxRadius);
      }

      return center + Quaternion.Euler(0f,UnityEngine.Random.Range(minAngle, maxAngle), 0f) * Vector3.forward * radius;
  }
    private void RemoveAllBananas(){
      if (bananaList != null){
          for (int i = 0; i < bananaList.Count; i++){
              if(bananaList[i] != null){
                  Destroy(bananaList[i]);
              }
          }
      }
      bananaList = new List<GameObject>();
  }
  private void PlaceBall(){
      //ballAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
     // ballAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f); 
  }
    private void PlaceGoal()
    {
       // goal.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * .5f;
      //  goal.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    private void SpawnBanana(int count, float bananaSpeed){
        for(int i = 0; i < count; i++){
            GameObject bananaObject = Instantiate<GameObject>(bananaPrefab.gameObject);
            bananaObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
            bananaObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            bananaObject.transform.parent = transform;
            bananaList.Add(bananaObject);
            bananaObject.GetComponent<Banana>().bananaSpeed = bananaSpeed;
        }

  }
 
  private void Update(){
      cumulativerewardtext.text = ballAgent.GetCumulativeReward().ToString("0.00");
  }
}
