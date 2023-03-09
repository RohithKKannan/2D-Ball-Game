using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask acidLayer;
    [SerializeField] private LayerMask finishLayer;
    [SerializeField] private LayerMask collectiblesLayer;
    [SerializeField] public BallType ballType;
    [SerializeField] private Light2D lightComponent;
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical == 1f && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    public void LightDisable()
    {
        lightComponent.enabled = false;
    }
    public void LightEnable()
    {
        lightComponent.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (collectiblesLayer == (collectiblesLayer | (1 << col.gameObject.layer)))
        {
            Debug.Log(col.gameObject.layer);
            Debug.Log(1 >> col.gameObject.layer);
            Debug.Log(2 << col.gameObject.layer);
            Debug.Log(1 << col.gameObject.layer);
            Debug.Log(collectiblesLayer | (1 << col.gameObject.layer));
            Debug.Log(collectiblesLayer);
            Debug.Log("Coin has been picked up!");
            Destroy(col.gameObject);
        }
        if (finishLayer == (finishLayer | (1 << col.gameObject.layer)))
        {
            if (ballType == BallType.BlueBall && col.gameObject.tag == "BlueFinish")
            {
                Debug.Log("Blue has finished!");
            }
            else if (ballType == BallType.RedBall && col.gameObject.tag == "RedFinish")
            {
                Debug.Log("Red has finished!");
            }
            gameObject.SetActive(false);
        }
        if (acidLayer == (acidLayer | (1 << col.gameObject.layer)))
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }
}
