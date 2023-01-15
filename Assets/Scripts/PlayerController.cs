using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool gameOver = false;
    bool youWin = false;
    [SerializeField] float torqueAmount;
    [SerializeField] GameObject finishParticles;
    [SerializeField] GameObject crashParticles;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;
    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RespondtoBoost();

    }

    void RespondtoBoost()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            surfaceEffector2D.speed = 30;
        }
        else if(Input.GetKey(KeyCode.DownArrow)){
            surfaceEffector2D.speed = 10;
        }
        else{
            surfaceEffector2D.speed = 20;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
            
            if(other.tag == "Finish" && gameOver == false){
                youWin = true;
                Invoke("reloadGame",2);
                finishParticles.SetActive(true);
                winText.SetActive(true);

            }
            else if (other.tag == "Ground" && youWin == false){
                gameOver = true;
                Invoke("reloadGame",1);
                crashParticles.SetActive(true);
                loseText.SetActive(true);
                
            }
        }

    void reloadGame(){
        SceneManager.LoadScene("Main");
    }
    
}
