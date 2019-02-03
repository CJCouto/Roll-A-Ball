using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text scoreText;
    public Text livesText;
    public Text winText;

    private bool levelOneComplete;

    private Rigidbody rb;
    private int count;
    private int score;
    private int lives;

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        lives = 3;
        SetCountText ();
        SetScoreText ();
        SetLivesText();
        winText.text = "";
        levelOneComplete = false;
    }

    void Update() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            SetCountText ();
            SetScoreText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            score = score - 1;
            lives = lives - 1;
            SetScoreText ();
            SetLivesText ();
        }
    }

    void SetCountText () {
        countText.text = "Count: " + count.ToString();
        if (count >= 20) {
            winText.text = "You Win";
        }
        if (count == 12 && levelOneComplete == false) {
            transform.position = new Vector3(60 , 1 , 0);
            levelOneComplete = true;
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetLivesText() {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0) {
            gameObject.SetActive(false);
            winText.text = "You Lose";
        }
    }
}
