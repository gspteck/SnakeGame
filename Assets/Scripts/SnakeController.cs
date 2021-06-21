using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {
    private Vector2 direction = Vector2.right;
    private List<Transform> snakeSegments;
    public Transform snakeSegmentPrefab;

    private int points;

    void Start() {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) {
            direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) {
            direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) {
            direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate() {
        for (int i = snakeSegments.Count - 1; i > 0; i--) {
            snakeSegments[i].position = snakeSegments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );
    }

    private void Grow() {
        Transform segment = Instantiate(this.snakeSegmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Grow();
            points += 1;
            print(points);
        } else if (other.tag == "Obstacle" || other.tag == "Snake") {
            GameOver();
        }
    }

    private void GameOver() {
        //save points in shared preferences
        //open game over screen
    }
}
