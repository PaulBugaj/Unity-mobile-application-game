using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployAsteroids : MonoBehaviour
{
    private GameMaster gm;
    public GameObject asteroidPrefab;
    public float respawnTime = 0.1f;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    private float screenMinX, screenMaxX, screenMinY, screenMaxY;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0,0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1,1, camDistance));

        screenMinX = bottomCorner.x;
        screenMaxX = topCorner.x;
        screenMinY = bottomCorner.y;
        screenMaxY = topCorner.y;

        StartCoroutine(asteroidWave());
    }

    private void spawnEnemy(){
        GameObject a = Instantiate(asteroidPrefab) as GameObject;
        if (gm.hasLost == true){
            Destroy(a);
        }
        int randomInt = Random.Range(0,4);
        //print(randomInt);
        if (randomInt == 0){
            a.transform.position = new Vector2(Random.Range(screenMinX,screenMaxX),screenMinY);
        }
        else if(randomInt == 1){
            a.transform.position = new Vector2(Random.Range(screenMinX,screenMaxX),screenMaxX);
        }
        else if(randomInt == 2){
            a.transform.position = new Vector2(screenMinX,Random.Range(screenMinY,screenMaxY));
        }
        else if(randomInt == 3){
            a.transform.position = new Vector2(screenMaxX,Random.Range(screenMinY,screenMaxY));
        }
    }

    // Update is called once per frame
    IEnumerator asteroidWave(){
        while(true){
            if (gm.hasLost == true){
                break;
            }
            else if (gm.hasLost == false){
                yield return new WaitForSeconds(respawnTime);
                spawnEnemy();
            }
        }
    }
}
