using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;
    [SerializeField] AudioSource audioSource;
    private int bpm;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        bpm = UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
    }

    

    private void Update()
    {
        
        if (body != null)
        {
            body.velocity = new Vector2 ( (bpm/60) * speed, body.velocity.y );

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 5);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") { grounded = true; }
        
    }
}
