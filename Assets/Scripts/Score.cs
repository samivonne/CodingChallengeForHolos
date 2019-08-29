using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
   
    private int goals;
    Rigidbody rb;
     Canvas canvas;
    public Text scoreText;
    private bool restartBall= false;
	// Use this for initialization
	void Start () {
        goals = 0;
        rb = GetComponent<Rigidbody>();

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        scoreText = canvas.GetComponentInChildren<Text>();
	}

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            goals += 1;
            scoreText.text = "Score: " + goals;
            //score
            this.gameObject.tag = "Scored";

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Barrier") {
            transform.position = new Vector3(0, 1f, -9);
            rb.velocity = Vector3.zero;

            restartBall = true;
            rb.isKinematic = true;
        }
    }

    void Update () {
        if (restartBall)
        {
            restartBall = false;
            rb.isKinematic = false;
        }

        if (this.gameObject.tag == "Scored")
        {
            transform.position = new Vector3(0, 1f, -9);
            rb.velocity = Vector3.zero;

            restartBall = true;
            rb.isKinematic = true;
            this.gameObject.tag = "Untagged";
        }

        if(this.gameObject.tag == "Shot" && !Input.GetMouseButton(0)) {
            this.gameObject.tag = "Untagged";
        }

    }

}
