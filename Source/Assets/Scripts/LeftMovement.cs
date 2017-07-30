using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int jumpForce = 5;

    [SerializeField] private Animator anim;

    [SerializeField]private GameObject finishText;
    //private int energy = 100;
    //private int interval = 1;
    //private float temp;
    private bool onAir;
    public bool finished;
    private Camera cam;

	void Start ()
    {
		rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        finishText = GameObject.FindGameObjectWithTag("Finishs");
        finishText.SetActive(false);
    }
	
	void Update ()
	{
       // No va a ser implementado
	   /* if (!finished)
	    {
	        temp += Time.deltaTime;
	        if (temp >= interval)
	        {
	            energy--;
	            temp = 0;
	            Debug.Log(energy);
	        }
        }*/
	    
        Jump();
	    if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
	    {
	        transform.localScale = Input.GetAxis("Horizontal") > 0 ? new Vector3(5, transform.localScale.y, transform.localScale.z) : new Vector3(-5, transform.localScale.y, transform.localScale.z);
	        rb.transform.Translate(new Vector2(Input.GetAxis("Horizontal"), 0) * moveSpeed * Time.deltaTime);
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
        if (!Input.GetButtonDown("Jump") || onAir) return;
        rb.velocity = Vector2.up * jumpForce;
        onAir = true;
    }

   /* void OnCollisionExit2D(Collision2D c)
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
            gameObject.SetActive(true);
            onAir = false;
        }
        if (c.gameObject.tag == "Player" && !finished)
        {
            //energy = 100;
            //Debug.Log("Energy : " + energy);
            finished = true;
            cam.GetComponent<CameraMovement>().cameraSpeed = 0;
            finishText.SetActive(true);
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}