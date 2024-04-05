using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float Range1 = 10;
    private float Range2 = 4;
    private float PosY = -2;
    public ParticleSystem explosivo;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(ForceCoordinates(),ForceMode.Impulse);
        targetRb.AddTorque(RandTorq(),RandTorq(),RandTorq(),ForceMode.Impulse);

        transform.position = ObjPos();
    }
    Vector3 ForceCoordinates(){
        return Vector3.up*Random.Range(minSpeed,maxSpeed);
    }
    float RandTorq(){
        return Random.Range(-Range1, Range1);
    }
    Vector3 ObjPos(){
        return new Vector3(Random.Range(-Range2,Range2),PosY);
    }
      private void OnMouseDown(){
       if(gameManager.isGameActive && gameManager.isGamePaused==false){ 
        Destroy(gameObject);
        Instantiate(explosivo,transform.position,explosivo.transform.rotation);
        if(gameObject.CompareTag("Bad")){
            score = -10;
        }
        if(gameObject.CompareTag("Good")){
            score = 5;
        }
        if(gameObject.CompareTag("Good1")){
            score = 10;
        }
        if(gameObject.CompareTag("Good2")){
            score = 15;
        }
        gameManager.UpdateScore(score);}
    }
    private void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad")){
           gameManager.Lives();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
