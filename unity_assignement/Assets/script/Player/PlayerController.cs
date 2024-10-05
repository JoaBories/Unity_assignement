using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    
    public Vector2 dashVector;
    public bool cantMove;
    public int coin = 0;


    [Header("Character Fix Assign")]
    public GameObject collisionFixLeft;
    public GameObject collisionFixRight;
    public GameObject isOnGround;

    [Header("GUI Assign")]
    public GameObject hudCanva;
    public GameObject pauseCanva;

    [Header("Misc Assign")]
    public GameObject spawnPoint;
    public Text coinCounter;
    public GameObject bulletPrefab;
    public CinemachineVirtualCamera playerCamera;

    bool canDoubleJump = true;
    bool ground;
    bool collisionLeft;
    bool collisionRight;
    bool pauseBool;
    float bulletDirection;
    
    Vector2 oldVelocity;

    private void Start()
    {
        transform.position = spawnPoint.transform.position;
    }
    void Update()
    {
        if (!pauseBool)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Animator animator = GetComponent<Animator>();
            Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
            Light2D spotLight = GetComponent<Light2D>();

            collisionRight = collisionFixRight.GetComponent<CollisionFix>().collisionState;
            collisionLeft = collisionFixLeft.GetComponent<CollisionFix>().collisionState;

            ground = isOnFloor();
            oldVelocity = rigidBody2D.velocity;

            coinCounter.text = ": " + coin;

            if (!cantMove)
            {

                //move
                if (Input.GetKey(KeyCode.D) && !collisionRight)
                {
                    spriteRenderer.flipX = false;
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                    if (oldVelocity.x < 0)
                    {
                        rigidBody2D.velocity = new Vector2(0, oldVelocity.y);
                    }
                    animator.SetBool("walking", true);
                }
                if (Input.GetKey(KeyCode.A) && !collisionLeft)
                {
                    spriteRenderer.flipX = true;
                    transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                    if (oldVelocity.x > 0)
                    {
                        rigidBody2D.velocity = new Vector2(0, oldVelocity.y);
                    }
                    animator.SetBool("walking", true);
                }
                if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                {
                    animator.SetBool("walking", false);
                }


                //Dash
                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.D)) { dashVector = new Vector2(1.5f, 2); bulletDirection = -126.87f; }
                    else if (Input.GetKey(KeyCode.A)) { dashVector = new Vector2(-1.5f, 2); bulletDirection = -53.13f; }
                    else if (Input.GetKey(KeyCode.S)) { dashVector = new Vector2(0, 0); bulletDirection = 10; }
                    else { dashVector = new Vector2(0, 3); bulletDirection = 270; }
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.D)) { dashVector = new Vector2(1.5f, -2); bulletDirection = 126.87f; }
                    else if (Input.GetKey(KeyCode.A)) { dashVector = new Vector2(-1.5f, -2); bulletDirection = 53.13f; }
                    else if (Input.GetKey(KeyCode.W)) { dashVector = new Vector2(0, 0); bulletDirection = 10; }
                    else { dashVector = new Vector2(0, -3); bulletDirection = 90; }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (Input.GetKey(KeyCode.A)) { dashVector = new Vector2(0, 0); bulletDirection = 10; }
                    else { dashVector = new Vector2(3, 0); bulletDirection = 180; }
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    if (Input.GetKey(KeyCode.D)) { dashVector = new Vector2(0, 0); bulletDirection = 10; }
                    else { dashVector = new Vector2(-3, 0); bulletDirection = 0; }
                }

                if (Input.GetKeyDown(KeyCode.L) && canDoubleJump)
                {
                    if (bulletDirection == 10)
                    {
                        if (spriteRenderer.flipX) { dashVector = new Vector2(-3, 0); bulletDirection = 0; }
                        else { dashVector = new Vector2(3, 0); bulletDirection = 180; }
                    }

                    rigidBody2D.velocity = dashVector;
                    canDoubleJump = false;
                    animator.Play("dash");
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().directionVector = new Vector3(-dashVector.x, -dashVector.y, 0);
                    bullet.GetComponent<Bullet>().directionAngle = bulletDirection;
                    bullet.GetComponent<Bullet>().player = gameObject;
                }


                //jump
                if (Input.GetKeyDown(KeyCode.K) && ground) rigidBody2D.velocity = new Vector2(oldVelocity.x, jumpForce);


                // air & ground states
                if (!ground)
                {
                    animator.SetBool("air", true);
                    if (rigidBody2D.velocity.y < 0) animator.SetBool("up", false);
                    else animator.SetBool("up", true);
                }
                else animator.SetBool("air", false);

                if (ground) canDoubleJump = true;

                if (canDoubleJump)
                {
                    spriteRenderer.color = Color.green;
                    spotLight.color = Color.green;
                }
                else
                {
                    spriteRenderer.color = Color.yellow;
                    spotLight.color = Color.yellow;
                }
            }

            //restart
            if (Input.GetKeyDown(KeyCode.R)) die();

            if (transform.position.y < -2)
            {
                die();
                rigidBody2D.velocity = Vector2.zero;
            }
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape)) pauseFunction();

    }

    public void pauseFunction()
    {
        if (hudCanva.activeSelf)
        {
            hudCanva.SetActive(false);
            pauseCanva.SetActive(true);
            Time.timeScale = 0;
            pauseBool = true;
        }
        else
        {
            pauseCanva.SetActive(false);
            hudCanva.SetActive(true);
            Time.timeScale = 1;
            pauseBool = false;
        }
    }

    bool isOnFloor()
    {
        return isOnGround.GetComponent<IsOnGround>().isOnGround;
    }

    void respawn()
    {
        transform.position = spawnPoint.transform.position;
        GetComponent<Animator>().Play("spawn");
    }

    void die()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("die");
    }

    void dashVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = dashVector;
    }

    void cameraShake(float amplitude)
    {
        playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage")) die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            collision.gameObject.SetActive(false);
            coin++;
        }
    }
}
