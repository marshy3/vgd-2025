using UnityEngine;

public class Enemy : MonoBehaviour
{
    //chase player
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundlayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
