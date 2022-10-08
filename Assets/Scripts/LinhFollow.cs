using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;
    public float distanceToTarget;
    private float distance;

    public float runSpeed = 5.0f;

    private IEnumerator coroutine;

    private float distanceTT;

    Rigidbody2D npcRB;

    private bool isRun =false;

    

    [SerializeField]  float waitTime = 5.0f;
    // Start is called before the first frame update
    private void Awake() {
        npcRB=GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        distanceTT = distanceToTarget;
    }
    
    void canFollow(){
        if(Mathf.Abs(target.transform.position.x - this.transform.position.x)<1)
            isRun=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        if(isRun){
            Move(runSpeed);
        }else{
            canFollow();
        }
    }
   
    void Move(float runSpeed){
         distance=target.transform.position.x - this.transform.position.x;
                 if(Mathf.Abs(distance)>distanceTT&& distance>0){
                     
                        npcRB.velocity=new Vector3(runSpeed,0,0);
                     
                 }
                 else if(Mathf.Abs(distance)>distanceTT && distance<0){
                     
                        npcRB.velocity=new Vector3(-runSpeed,0,0);
                     
                } 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("StandTrigger")){

            isRun=false;
            coroutine = WaitAndRun(3.0f);
            StartCoroutine(coroutine);
            Debug.Log("OnTriggerEnter");
            
        }
    }
    IEnumerator WaitAndRun(float waitTime){
     
        
           // distanceTT=float.NegativeInfinity;
            yield return new WaitForSeconds(waitTime);
            Debug.Log(Time.time);
            isRun=true;  
              
            
    }
}
