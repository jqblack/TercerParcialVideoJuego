using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{
    [SerializeField] private int vida = 3; 
    public float velocidad;
    public float velocidadMax;
    public float friccionSuelo;
    public bool colPies = false;

    public Text Textlvl;
    public float fuerzaSalto;
    public bool enPlataforma = false;
    private CapsuleCollider2D ccPlayer;
    private Rigidbody2D rPlayer;
    private Animator aPlayer;
            
    private SpriteRenderer sPlayer;
    private float h;
    private Camera camara;
    private Color colorOriginal;
    private Vector3 posIni;
    private bool muerto = false;
    private bool tocando = false;
    private bool miraDerecha = true;
    float altoPlayer, altoCam, posPlayer;

    [Header("Barra de vida")]
    [SerializeField] private GameObject barraVida;
    [SerializeField] private Sprite vida3, vida2, vida1, vida0;

    [Header("Efectos de Sonido")]
    [SerializeField] private GameObject oSaltoPlayer;
    [SerializeField] private GameObject oMuertePlayer;

    private AudioSource sSaltoPlayer;
    private AudioSource sMuertePlayer;

    // Start is called before the first frame update
    void Start()
    {
        posIni = transform.position;
        rPlayer = GetComponent<Rigidbody2D>();
        aPlayer = GetComponent<Animator>();
        ccPlayer = GetComponent<CapsuleCollider2D>();
        sPlayer = GetComponent<SpriteRenderer>();
        colorOriginal = sPlayer.color;
        camara = Camera.main;
        sSaltoPlayer = oSaltoPlayer.GetComponent<AudioSource>();
        sMuertePlayer = oMuertePlayer.GetComponent<AudioSource>();

        altoCam = camara.orthographicSize * 2;
         altoPlayer = GetComponent<Renderer>().bounds.size.y;
         Textlvl.text = "";
    }

    // Update is called once per frame
    void Update()
    {


        giraPlayer(h);
        aPlayer.SetFloat("VelocidadX", Mathf.Abs(rPlayer.velocity.x));
        aPlayer.SetFloat("VelocidadY", rPlayer.velocity.y);
        aPlayer.SetBool("TocaSuelo", colPies);
        //Salto
        colPies = CheckGround.colPies;
        if(Input.GetButtonDown("Jump") && colPies)
        {
            rPlayer.velocity = new Vector2(rPlayer.velocity.x, 0f);
            rPlayer.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
            sSaltoPlayer.Play();
            
        }

        if(muerto)
        {
            posPlayer = camara.transform.InverseTransformDirection(transform.position - camara.transform.position).y;
            if (posPlayer < ((altoCam/2) * -1 ) - (altoPlayer/2))
            {
                Invoke("llamaRecarga", 1);
                muerto = false;
            }

            
        }
    }

    private void llamaRecarga()
    {
        GameController.playerMuerto = true;
    }

    private void BarraVida(int salud)
    {
        if (salud == 2) barraVida.GetComponent<Image>().sprite = vida2;
        if(salud == 1) barraVida.GetComponent<Image>().sprite = vida1;
        
        rPlayer.velocity = Vector2.zero;
        rPlayer.AddForce(new Vector2(0.0f, fuerzaSalto), ForceMode2D.Impulse);
    }

     void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        rPlayer.AddForce(Vector2.right * velocidad * h);
        float limiteVelocidad = Mathf.Clamp(rPlayer.velocity.x, -velocidadMax, velocidadMax);
        rPlayer.velocity = new Vector2(limiteVelocidad, rPlayer.velocity.y);

        if(h==0 && colPies)
        {
            Vector3 velocidadArreglada = rPlayer.velocity;
            velocidadArreglada.x *= friccionSuelo;
            rPlayer.velocity = velocidadArreglada;
        }
    }

    void giraPlayer(float horizontal)
    {
        if((horizontal > 0 && !miraDerecha) ||( horizontal < 0 && miraDerecha))
        {
            miraDerecha = !miraDerecha;
            Vector3 escalaGiro = transform.localScale;
            escalaGiro.x = escalaGiro.x * -1;
            transform.localScale = escalaGiro;
        }
    }

    private void muertePlayer()
    {
        sMuertePlayer.Play();
        barraVida.GetComponent<Image>().sprite = vida0;
        aPlayer.Play("Muerto");
        GameController.gameOn = false;
        rPlayer.velocity = Vector2.zero;
        rPlayer.AddForce(new Vector2(0.0f, fuerzaSalto), ForceMode2D.Impulse);
        ccPlayer.enabled = false;
        muerto = true;
        vida =3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pinchos" && !muerto)
        {
            Debug.Log("Quita salud");
            if (vida > 1)
            {
                vida--;
                BarraVida(vida);
                /*float lado = Mathf.Sign(collision.transform.position.x - transform.position.x);
                rPlayer.velocity = Vector2.zero;
                rPlayer.AddForce(new Vector2(10f * lado, 4f), ForceMode2D.Impulse);*/
            }
          else{
                muertePlayer();
            }

            
        }
        if(collision.gameObject.tag == "CaidaAlVacio" )
        {
            Debug.Log("Muerte por caida al vacio");
            muertePlayer();
            Invoke("llamaRecarga", 1);
        }

        if(collision.gameObject.tag == "lvl2")
            {
                 Debug.Log("Hola");
                 Textlvl.text = "CARGANDO SEGUNDO NIVEL";   
                
            }
    }

   

    private void recibePulsaciones()
    {
        if (Input.GetKey(KeyCode.R)) GameController.playerMuerto = true; //Volver a colocar al jugador.
    }

    //Deteccion de Plataformas Moviles
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
        {
            rPlayer.velocity = Vector3.zero;
            transform.parent = collision.transform;
            enPlataforma = true;
        }
        if (collision.gameObject.tag == "enemigoPupa")
        {
            tocado(collision.transform.position.x);
        }
    }

     private void tocado(float posX){
       if(vida > 1){
        if(!tocando){
         Color nuevoColor = new Color(255f / 255f, 100f / 255f, 100f / 255f);    
        // sPlayer.color = nuevoColor;
        tocando = true;
        float lado = Mathf.Sign(posX - transform.position.x);
        rPlayer.velocity = Vector2.zero;
      rPlayer.AddForce(new Vector2(4f * lado, 4f), ForceMode2D.Impulse);
      vida--;
      BarraVida(vida);
        }else{
            muertePlayer();
            //sPlayer.color = colorOriginal;
        }
    }
    }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "PlataformaMovil")
            {
                transform.parent = null;
                enPlataforma = false;
            }
        }


    }
