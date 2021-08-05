using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManagerScript;
    
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private SkinnedMeshRenderer playerSMRend1; // These are used to access the two child meshed renderers used for the player
    //private SkinnedMeshRenderer playerSMRend2;
    //private AudioSource mainCameraAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip bananaSound;
    public float jumpForce = 1500f;
    
    public float walkSpeed = 1.0f;
    public float startPosX = 0f;

    private bool isOnGround = false;
    private bool canDoubleJump = false;

    private float runStaticSpeed = 1.0f;
    private float runStaticFast = 1.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        // These looks for the child Skinned Mesh Renderers for player. 'transform.' makes sure it only searches this object (the player) and childs. 
        playerSMRend1 = transform.Find("CH_Sheriff").GetComponent<SkinnedMeshRenderer>();
        // Hide player
        playerSMRend1.enabled = false;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        //mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        
        
        if (dirtParticle.isPlaying)
        {
            dirtParticle.Stop(); // Make sure to stop at touch ground on intro!
            dirtParticle.Clear();
        }
        Debug.Log("Jumpforce start: " + jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.GameIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                Debug.Log("Jumpforce update: " + jumpForce);
                // Simple added solution for doublejump
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                }
                else
                {
                    isOnGround = false;
                }
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1f);
                dirtParticle.Stop();
            }

            // Speed up - uses animator Speed_f as multiplier in animation to change animation speed.
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                gameManagerScript.SetHighSceneSpeed();
                playerAnim.SetFloat("Speed_f", runStaticFast);
            }
            // Slow down
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                gameManagerScript.SetNormalSceneSpeed();
                playerAnim.SetFloat("Speed_f", runStaticSpeed);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                gameManagerScript.TogglePause();
            }
        }
        // Running some cinematic (intro scene)
        else if (!gameManagerScript.GameOver) 
        {
            if (isOnGround)
            {
                playerAnim.SetBool("Grounded", true);
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            }
            if (transform.position.x >= startPosX) 
            {
                playerAnim.SetFloat("Speed_f", runStaticSpeed); // Start "run_static" animation Speed_f > 0.5
                dirtParticle.Play();
                gameManagerScript.SignalGameReady();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameManagerScript.GameIsPlaying)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                canDoubleJump = true;
                dirtParticle.Play();
            }
            else if (collision.gameObject.CompareTag("Score"))
            {
                playerAudio.PlayOneShot(bananaSound, 0.8f);
                collision.gameObject.GetComponent<MoveLeftScore>().AddMyScore();
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 1f);

                dirtParticle.Stop();
                dirtParticle.Clear();

                gameManagerScript.SignalGameOver();
            }
        }
        // Running some cinematic (intro scene)
        else if (!gameManagerScript.GameOver)
        {
            if (collision.gameObject.CompareTag("Carrier"))
            {
                // Show and drop player
                playerAnim.SetBool("Grounded", false);
                GetComponent<Rigidbody>().useGravity = true;
                playerSMRend1.enabled = true;              
            }
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                canDoubleJump = true;
            }

        }
    }
 
}
