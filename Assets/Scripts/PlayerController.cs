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

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip bananaSound;

    public CanvasScript canvasScript;

    public bool isOnGround = false;
    public bool canDoubleJump = false;    
    public float jumpForce = 40;
    public float gravityModifier = 1;
    public float walkSpeed = 4.0f;
    public float startPosX = 0f;

    public float runStaticSpeed = 1.0f;
    public float runStaticFast = 1.1f;
    
    public bool gameOver = false;
    public bool gameReady = false;

    private int scoreKeeper = 0;// minus the objects already in scene
    private int bananasCaught = 0;
    private int bananasMissed = 0;


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

        //Debug.Log("Start() : Before IF : dirtParticle.isPlaying " + dirtParticle.isPlaying);

        if (dirtParticle.isPlaying)
        {
            dirtParticle.Stop(); // Make sure to stop at touch ground on intro!
            dirtParticle.Clear();

            //Debug.Log("Start() : Inside IF : dirtParticle.isPlaying " + dirtParticle.isPlaying);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.GameIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameReady && !gameOver)
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
                canvasScript.HideBigText();
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            }
            if (transform.position.x >= startPosX) 
            {
                //canvasScript.HideBigText();
                playerAnim.SetFloat("Speed_f", runStaticSpeed); // Start "run_static" animation Speed_f > 0.5
                dirtParticle.Play();
                gameReady = true;
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
                Destroy(collision.gameObject);
                gameManagerScript.AddScore(1, false);
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
                //playerSMRend2.enabled = true;
            }
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                canDoubleJump = true;
            }

        }
    }
    public void AddScore()
    {
        if (!gameOver)
        {
            scoreKeeper++;
            canvasScript.ShowScore(scoreKeeper, bananasCaught, bananasMissed);
            //Debug.Log("Score: " + scoreKeeper);
        }
    }
    public void AddDoubleScore()
    {
        if (!gameOver)
        {
            scoreKeeper += 2;
            canvasScript.ShowScore(scoreKeeper, bananasCaught, bananasMissed);
            //Debug.Log("Score: " + scoreKeeper);
        }
    }
    public void AddMissedBananas()
    {
        if (!gameOver)
        {
            bananasMissed++;
            canvasScript.ShowScore(scoreKeeper, bananasCaught, bananasMissed);
        }
    }
    public void AddMissedDoubleBananas()
    {
        if (!gameOver)
        {
            bananasMissed += 2;
            canvasScript.ShowScore(scoreKeeper, bananasCaught, bananasMissed);
        }
    }
    public int GetScore()
    {
        return scoreKeeper;
    }
}
