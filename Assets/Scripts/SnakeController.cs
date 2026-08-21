using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour {
    // Movement and direction
    private Vector2 direction = Vector2.right;
    private List<Transform> snakeSegments;
    public Transform snakeSegmentPrefab;

    // Player statistics
    private int points;
    private int credits;
    private int addedCredits;

    // Touch input variables
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;
    public float swipeRange;
    public float tapRange;

    // Speed of the snake
    public float moveSpeed = 10f;

    void Start() {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);

        credits = PlayerPrefs.GetInt("credits");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) {
            GoUp();
        } else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) {
            GoLeft();
        } else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) {
            GoDown();
        } else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) {
            GoRight();
        }

        Swipe();        
    }

    private void FixedUpdate() {
        // Move the snake segment
        for (int i = snakeSegments.Count - 1; i > 0; i--) {
            snakeSegments[i].position = Vector3.Lerp(snakeSegments[i].position, snakeSegments[i - 1].position, moveSpeed * Time.deltaTime);
        }

        // Move the head of the snake
        Vector3 newPosition = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    // Movement direction functions
    private void GoUp() { if (direction != Vector2.down) { direction = Vector2.up; } }
    private void GoDown() { if (direction != Vector2.up) { direction = Vector2.down; } }
    private void GoLeft() { if (direction != Vector2.right) { direction = Vector2.left; } }
    private void GoRight() { if (direction != Vector2.left) { direction = Vector2.right; } }

    private void Grow() {
        Transform segment = Instantiate(snakeSegmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Grow();
            points += 1;
            // Increase speed with each food item eaten
            moveSpeed += 0.5f; 

        } else if (other.tag == "Obstacle" || (other.tag == "Snake" && other.gameObject != this.gameObject)) {
            GameOver();
        }
    }

    private void GameOver() {
        credits += points * 2;
        addedCredits = points * 2;
        PlayerPrefs.SetInt("credits", credits);
        PlayerPrefs.SetInt("added_credits", addedCredits);
        SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
    }

    private void Swipe() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 distance = currentTouchPosition - startTouchPosition;
            if (!stopTouch) {
                if (distance.x < -swipeRange) {
                    GoLeft();
                    stopTouch = true;
                } else if (distance.x > swipeRange) {
                    GoRight();
                    stopTouch = true;
                } else if (distance.y > swipeRange) {
                    GoUp();
                    stopTouch = true;
                } else if (distance.y < -swipeRange) {
                    GoDown();
                    stopTouch = true;
                }
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange) {
                // Handle tap event if needed
            }
        }
    }
}
