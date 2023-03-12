using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public BallType ballType;
    [SerializeField] private Light2D lightComponent;
    [SerializeField] private GameManager gameManager;
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
        {
            AudioManager.Instance.PlaySound(SoundType.Jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
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
        if (col.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
        {
            Debug.Log("Coin has been picked up!");
            gameManager.PickedCoinUp();
            Destroy(col.gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            if (ballType == BallType.BlueBall && col.gameObject.tag == "BlueFinish")
            {
                Debug.Log("Blue has finished!");
                gameManager.PlayerFinished(ballType);
                gameObject.SetActive(false);
            }
            else if (ballType == BallType.RedBall && col.gameObject.tag == "RedFinish")
            {
                Debug.Log("Red has finished!");
                gameManager.PlayerFinished(ballType);
                gameObject.SetActive(false);
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Switch"))
        {
            col.GetComponent<SwitchController>().TriggerSwitch(ballType);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Acid"))
        {
            Debug.Log("Game Over!");
            gameManager.GameOver(GameOverConditions.FellIntoAcid);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            if (ballType == BallType.RedBall)
            {
                speed = speed / 2;
            }
            else
            {
                Debug.Log("Swimming in Lava!");
                gameManager.GameOver(GameOverConditions.BlueInLava);
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if (ballType == BallType.BlueBall)
            {
                speed = speed / 2;
            }
            else
            {
                gameManager.GameOver(GameOverConditions.RedInWater);
            }
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            if (ballType != BallType.RedBall)
            {
                Debug.Log("Swimming in Lava!");
            }
            else
            {
                speed = speed * 2;
            }
        }
    }
}
