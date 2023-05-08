using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public GameObject[] trees;
    public int currentTreeIndex = 0;
    public  float speed = 10.0f;
    float trackerAhead = 10.0f;
     GameObject tracker ;
    public float turnSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }
    
    void ProgressTracker(){
        if(Vector3.Distance(tracker.transform.position,this.transform.position) > 10)return;
        Vector3 TankDist = trees[currentTreeIndex].transform.position - tracker.transform.position;
        if(TankDist.magnitude < 3) currentTreeIndex++;
        if(currentTreeIndex >= trees.Length) currentTreeIndex=0;
        tracker.transform.LookAt(trees[currentTreeIndex].transform);
        tracker.transform.Translate(0,0,(speed + 10f)*Time.deltaTime);
        
    }
    void AdjustMovement(){
         Vector3 TankDist = tracker.transform.position - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(TankDist);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,lookRotation,Time.deltaTime*turnSpeed);
         this.transform.Translate(Vector3.forward * Time.deltaTime * speed,Space.Self);
    }
    
    // Update is called once per frame
    void Update()
    {
        // Vector3 TankDist = trees[currentTreeIndex].transform.position - this.transform.position;
        // if(TankDist.magnitude < 10) currentTreeIndex++;
        // if(currentTreeIndex >= trees.Length) currentTreeIndex=0;

        AdjustMovement();
        ProgressTracker();
    }
}
