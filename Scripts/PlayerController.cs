using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce;
    public float gravityModifier;
    public bool isonGround = true;
    public bool gameOver = false;
    private Animator playerAnimation;
    public ParticleSystem playerExplosion;
    public ParticleSystem dirtSplatter;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    public AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Animator>();
        playerExplosion = GameObject.Find("FX_Explosion_Smoke").GetComponent<ParticleSystem> ();
        dirtSplatter = GameObject.Find("FX_DirtSplatter").GetComponent<ParticleSystem>();
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isonGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isonGround = false;
            playerAnimation.SetTrigger("Jump_trig");
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            dirtSplatter.Play();
            isonGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            Debug.Log("GameOver");
            gameOver = true;
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
            playerExplosion.Play();
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
        
    }
}
