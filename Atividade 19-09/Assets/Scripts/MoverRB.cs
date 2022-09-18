using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoverRB : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool isGrounded;// variavel que realiza a verificação se o player está no chão ou não
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip cogumelo; //Variável armazenando o aúdio do Cogumelo para quando o mario coletar
    [SerializeField] private AudioClip yahoo; //Variável armazenando o aúdio do Mario para quando ele pular
    [SerializeField] private AudioClip falling; //Variável armazenando o aúdio do Mario para quando cair das plataformas

    public float velocidadeMax = 10.0f;
    public float forcaPulo = 5.0f;

    private Rigidbody rb;
    private float movX;
    private bool alreadyPlayedSong = false; //Variável que é utilizada para validar se ele já ativou o aúdio para n]ao ser ativado mais de uma vez

    public int cogumelos = 0;// variavel responsavel pela quantidade de cogumelos que o player pegou

    [SerializeField] private Text textoCogumelo;
    [SerializeField] private GameObject painelFimJogo;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
        rb = GetComponent<Rigidbody>();
        textoCogumelo.text = cogumelos.ToString();
        alreadyPlayedSong = false;
        isGrounded = false;
    }

    private void Update()
    {
        //verificação para realizar a troca das animações de idle e run
        if (Input.GetAxis("Horizontal") != 0 && isGrounded)
        {
            _animator.SetInteger("animacao", 1);

        }
        else if(Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            _animator.SetInteger("animacao", 0);

        }

        if (transform.position.y <= -1 && !alreadyPlayedSong) //Validação para ver se o Mario saiu da área de jogo e está caindo do mapa
        {
            alreadyPlayedSong = true; //Colocando a variável como true para ele não entrar nessa validação novamente e ativar o aúdio varias vezes
            _animator.SetTrigger("caindo"); //Ativando animação do Mario caindo
            _audioSource.PlayOneShot(falling); //Ativando o som do Mario gritando
        }

        //verificação para realizar a reinicialização da fase quando o jogador cai da plataforma
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //verificação para ativar o painel de quando o jogador finaliza o jogo
        if(cogumelos == 5)
        {
            Time.timeScale = 0;// deixa o jogo parado
            painelFimJogo.SetActive(true);//ativa o componente gameobject do painel
            //Avisa o Audio MAnager que o jogo venceu
            AudioManager.Instance.winner = true;
        }
        Movimentar();
        Pular();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }

    //metodo para movimentar o player
    private void Movimentar()
    {
         movX = Input.GetAxisRaw("Horizontal");
         rb.AddForce(new Vector3(movX * velocidadeMax, 0, 0));
         FliparPersonagem(movX);
    }

    //metodo que realizar a flipagem do personagem
    void FliparPersonagem(float mov)
    {
        if(mov > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);//realiza a rotação do personagem no angulo especificado

        }if(mov < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);//realiza a rotação do personagem no angulo especificado

        }
    }

    //metodo que realizar a mecanica de pulo do player
    private void Pular()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _audioSource.PlayOneShot(yahoo); //Ativando o som do Mario falando yahoo
            _animator.SetTrigger("pular");//chama um trigger da animação de pulo do player
            rb.AddForce(new Vector3(0, forcaPulo, 0), ForceMode.Impulse);
        }
    }

    //verificação para ver se o player está no chão ou no ar
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = collision.transform;// atribui o player como componente filho do componente que ele está colidindo com a tag ground
            isGrounded = true;
        }
    }
    //verificação para ver se o player está no chão ou no ar
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = null;// remove o componente filho de seu pai
            isGrounded = false;
        }
    }

    //verificação para as coletas do cogumelo
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Item"))
        {
            cogumelos++;
            _audioSource.PlayOneShot(cogumelo);//toca o clip de audio de quando o player coleta o cogumelo
            textoCogumelo.text = cogumelos.ToString();// realiza a conversão da quantidade de cogumelos para string para ela ser mostrada no componente text
            Destroy(collision.gameObject);// delete o gameobject especificado

        }
    }

    //metodo que realiza a reinicialização do jogo após o jogador zerar o mesmo
    public void JogarNovamente()
    {            
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//realiza o recarregamento da cena
        //Avisa o Audio manager que o jogo não ganhou 
        AudioManager.Instance.winner = false;
    }

   public void SairJogo()
    {
        Application.Quit();//realiza o fechamento do jogo
    }

}
