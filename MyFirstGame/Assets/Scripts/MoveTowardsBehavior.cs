using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsBehavior : MonoBehaviour
{
     private float screenMinX, screenMaxX, screenMinY, screenMaxY;

     public float minSpeed;
     public float maxSpeed;

     private Vector2 screenBounds;

     float speed;

     public float secondsToMaxDifficulty;

     private GameObject[] earth;
     public GameObject deathEffect;
     private Vector2 target;

     // Start is called before the first frame update
     void Start()
     {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0,0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1,1, camDistance));

        screenMinX = bottomCorner.x;
        screenMaxX = topCorner.x;
        screenMinY = bottomCorner.y;
        screenMaxY = topCorner.y;
     }

     // Update is called once per frame
     void Update()
     {
         earth = GameObject.FindGameObjectsWithTag("Planet");
         int rand = Random.Range(0, earth.Length);
         target = earth[rand].transform.position;
         if ((Vector2)transform.position != target)
         {
             speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent());
             transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
             if(transform.position.x < screenMinX || transform.position.x > screenMaxX){
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
             }
             if (Time.timeSinceLevelLoad > 79)
             {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * 2 * (Time.deltaTime));
             }
         }
     }

     float GetDifficultyPercent(){
         return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
     }
}
