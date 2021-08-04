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
    private AudioSource mainCameraAudio;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip bananaSound;
    [SerializeField] private float jumpForce = 40;
    [SerializeField] private float gravityModifier = 1;
    [SerializeField] private float walkSpeed = 4.0f;
    [SerializeField] private float startPosX = 0f;

    private CanvasScript canvasScript;

    private bool isOnGround = false;
    private bool canDoubleJump = false;

    private float runStaticSpeed = 1.0f;
    private float runStaticFast = 1.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        // These looks for the child Skinned Mesh Renderers for player. 'transform.' makes sure it only searches this object (the player) and childs. 
        //playerSMRend2 = transform.Find("CH_Punk").GetComponent<SkinnedMeshRenderer>();
        playerSMRend1 = transform.Find("CH_Sheriff").GetComponent<SkinnedMeshRenderer>();
        // Hide player
        playerSMRend1.enabled = false;
        //playerSMRend2.enabled = false;

        // Getting the CanvasScript for printing on screen
        canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        canvasScript.ShowBigText("GET READY");
       
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        
        Physics.gravity *= gravityModifier;

        if (dirtParticle.isPlaying)
        {
            dirtParticle.Stop(); // Make sure to stop at touch ground on intro!
            dirtParticle.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.GameIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameManagerScript.GameIsPlaying)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
            if (gameManagerScript.GameIsPlaying && Input.GetKeyDown(KeyCode.LeftShift))
            {
                gameManagerScript.SetHighSceneSpeed();
                playerAnim.SetFloat("Speed_f", runStaticFast);
            }
            // Slow down
            if (gameManagerScript.GameIsPlaying && Input.GetKeyUp(KeyCode.LeftShift))
            {
                gameManagerScript.SetNormalSceneSpeed();
                playerAnim.SetFloat("Speed_f", runStaticSpeed);
            }
        }
        // Running some cinematic (intro scene)
        else if (!gameManagerScript.GameOver) 
        {
            if (isOnGround)
            {
                playerAnim.SetBool("Grounded", true);
                canvasScript.HideBigText(); // Move for new canvas
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
                gameManagerScript.SignalGameOver();

                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 1f);

                dirtParticle.Stop();
                dirtParticle.Clear();

                // Stop main audio
                mainCameraAudio.Stop();
                canvasScript.ShowBigText("GAME OVER!!");
                //Debug.Log("GAME OVER! Score: " + scoreKeeper);
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
