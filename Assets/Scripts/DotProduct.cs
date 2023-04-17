using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour
{
    public Transform ghost1;
    public Transform ghost2;
    public Transform ghost3;
    public Transform ghost4;

    public AudioSource heart_rate;

    float dotProduct1;
    float dotProduct2;
    float dotProduct3;
    float dotProduct4;

    bool m_HasAudioPlayed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void checkFov(float dotProduct){
        if(dotProduct == 1) //enemy is in front of player
        {
            if(!heart_rate.isPlaying)
            {
                heart_rate.Play();
            }
        } else 
        {
            heart_rate.Stop();
        }
    }

    void checkDistance(float distanceToEnemy, float dotProduct)
    {
        //if enemy is too far away heartbeat won't play when enemy is in player's fov
        if(distanceToEnemy < 4f){
            checkFov(dotProduct);
        }
    }

    // Update is called once per frame
    void Update()
    {    
        Vector3 vectorToEnemy1 = ghost1.position - transform.position;
        dotProduct1 = Mathf.Round(Vector3.Dot(transform.forward.normalized, vectorToEnemy1.normalized));
        float distanceToEnemy1 = Vector3.Distance(transform.position, ghost1.position);
        
        checkDistance(distanceToEnemy1, dotProduct1);

        Vector3 vectorToEnemy2 = ghost2.position - transform.position;
        dotProduct2 = Mathf.Round(Vector3.Dot(transform.forward.normalized, vectorToEnemy2.normalized));
        float distanceToEnemy2 = Vector3.Distance(transform.position, ghost2.position);

        checkDistance(distanceToEnemy2, dotProduct2);

        Vector3 vectorToEnemy3 = ghost3.position - transform.position; 
        dotProduct3 = Mathf.Round(Vector3.Dot(transform.forward.normalized, vectorToEnemy3.normalized));
        float distanceToEnemy3 = Vector3.Distance(transform.position, ghost3.position);

        checkDistance(distanceToEnemy3, dotProduct3);

        Vector3 vectorToEnemy4 = ghost4.position - transform.position;
        dotProduct4 = Mathf.Round(Vector3.Dot(transform.forward.normalized, vectorToEnemy4.normalized));
        float distanceToEnemy4 = Vector3.Distance(transform.position, ghost4.position);

        checkDistance(distanceToEnemy4, dotProduct4);
        
    }
}
