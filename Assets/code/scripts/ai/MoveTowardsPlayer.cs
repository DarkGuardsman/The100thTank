using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour {

    public GameObject target;

    /** Distance to stop moving towards the target */
    public int stopDistance = 4;
    /** Meters per second to move towards target */
    public int moveSpeed = 3;
    /** Rotations per second to aim at the player */
    public int rotationSpeed = 3;
    
    void FixedUpdate()
    {
        //Find target
        if(target == null)
        {
            //TODO pass threw to some targetting system to find best target if there are several
            target = GameObject.FindWithTag("Player");
        }
       
        //If target then start doing logic
        if (target != null)
        {
            //TODO implement can-see check
            //TODO implement pathfinder to avoid hitting walls
            //TODO implement wiggle in path to make the AI harder to hit
            float distanceTo = distanceFrom(transform.position, target.transform.position);
            if (stopDistance < 1 || distanceTo > stopDistance)
            {
                //Aim the AI towards the target
                Vector3 lookDir = target.transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);

                //TODO add check to make sure AI is aiming near the player before it moves towards the player

                //Move forward      TODO add limiter for max movement so we move .1 if the remaining distance is .1 instead of full movement of 3
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    public float distanceFrom(Vector3 one, Vector3 two)
    {
        float x, y, z;
        x = one.x - two.x;
        y = one.y - two.y;
        z = one.z - two.z;
        return Mathf.Sqrt(x * x + z * z);
    }
}
