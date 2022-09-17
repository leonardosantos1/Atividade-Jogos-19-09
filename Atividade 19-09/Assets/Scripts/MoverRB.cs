using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoverRB : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip yahoo;
    [SerializeField] private AudioClip falling;

    public float velocidadeMax = 10.0f;
    public float forcaPulo = 5.0f;

    private Rigidbody rb;
    private float movX;
    private bool alreadyPlayedSong = false;

    public int cogumelos = 0;

    [SerializeField] private Text textoCogumelo;

    [SerializeField] private GameObject painelFimJogo;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        textoCogumelo.text = cogumelos.ToString();
        alreadyPlayedSong = false;
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && isGrounded)
        {
            _animator.SetInteger("animacao", 1);

        }
        else if(Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            _animator.SetInteger("animacao", 0);

        }

        if (transform.position.y <= -1 && !alreadyPlayedSong)
        {
            alreadyPlayedSong = true;
            _animator.SetTrigger("caindo");
            _audioSource.PlayOneShot(falling);
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(cogumelos == 5)
        {
            Time.timeScale = 0;
            painelFimJogo.SetActive(true);
        }
        Movimentar();
        Pular();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }

    private void Movimentar()
    {
         movX = Input.GetAxisRaw("Horizontal");
         rb.AddForce(new Vector3(movX * velocidadeMax, 0, 0));
         FliparPersonagem(movX);
    }

    void FliparPersonagem(float mov)
    {
        if(mov > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);

        }if(mov < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);

        }
    }

    private void Pular()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _audioSource.PlayOneShot(yahoo);
            _animator.SetTrigger("pular");
            rb.AddForce(new Vector3(0, forcaPulo, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.parent = collision.transform;
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.parent = null;
            isGrounded = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Item"))
        {
            cogumelos++;
            _audioSource.PlayOneShot(clip);
            textoCogumelo.text = cogumelos.ToString();
            Destroy(collision.gameObject);

        }
    }

    public void JogarNovamente()
    {            
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   public void SairJogo()
    {
        Application.Quit();
    }

}
