using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private float random;
    public Transform player;
    public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.value;
        player = GameObject.FindWithTag("Player").transform;
        goal = GameObject.FindWithTag("Goal").transform;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Random " + random);
        if (random > .5)
        {
            if (Vector3.Distance(player.position, this.transform.position) < 15)
            {
                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .1f);

                if (direction.magnitude > 3)
                {
                    this.transform.Translate(0, 0, 0.05f);
                }
            }
        } else if(random <= .5) {
            if(Vector3.Distance(goal.position, this.transform.position) < 30) {
                Vector3 direction = goal.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .1f);
                if(direction.magnitude > 5) {
                    this.transform.Translate(0, 0, 0.05f);
                }
            }
        }
    }
}
