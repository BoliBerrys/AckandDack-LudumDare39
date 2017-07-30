using System.Collections;
using UnityEngine;

public class RightMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int jumpForce = 5;
    [SerializeField] private Animator anim;
    private Lever lever;
    private bool onAir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            transform.localScale = Input.GetAxis("Vertical") > 0 ? new Vector3(5, transform.localScale.y, transform.localScale.z) : new Vector3(-5, transform.localScale.y, transform.localScale.z);
            rb.transform.Translate(new Vector2(Input.GetAxis("Vertical"), 0) * moveSpeed * Time.deltaTime);
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        anim.SetBool("IsJumping", onAir);
        if (!Input.GetButtonDown("RightJump") || onAir) return;
        rb.velocity = Vector2.up * jumpForce;
        onAir = true;
    }
    /*
    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground")
        {
            onAir = true;
        }
    }*/

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground" && onAir)
        {
            onAir = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Lever")
        {
            anim.SetBool("IsLever", true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("IsLever", false);
    }
}