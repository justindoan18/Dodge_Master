using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float moveSpeed;
    public FloatingJoystick joystick;
    private Rigidbody2D rb;
    public Vector2 move;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }
    private void FixedUpdate()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float screenWidth = Camera.main.orthographicSize * 2 * screenRatio;
        float screenHeight = Camera.main.orthographicSize * 2;

        // Giới hạn vị trí của gameObject
        float newX = Mathf.Clamp(rb.position.x + move.x * moveSpeed * Time.deltaTime, -screenWidth / 2, screenWidth / 2);
        float newY = Mathf.Clamp(rb.position.y + move.y * moveSpeed * Time.deltaTime, -screenHeight / 2, screenHeight / 2);
        if(rb!=null)
        {
            rb.MovePosition(new Vector2(newX, newY));
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(rb);
        animator.Play("destroy");
    }
}