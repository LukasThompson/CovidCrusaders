using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    


    [Header("Movement")]
    [Header("Player Properties")]
    [Tooltip("This shows the current movement based on player input.")]
    public float horizontalInput;
    public float verticalInput;
    [Tooltip("The player's speed.")]
    public float speed = 10.0f;
    [Tooltip("The play area limit.")]
    public float xRange = 10;
    private Rigidbody rb;

    [Header("Projectiles")]
    [Tooltip("Item to be thrown by player.")]
    public GameObject projectilePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayer();
        ShootProjectile(KeyCode.Space);
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    void ConstrainPlayer()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    void ShootProjectile(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
