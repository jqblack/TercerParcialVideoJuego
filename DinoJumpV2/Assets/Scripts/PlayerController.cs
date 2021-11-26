using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad;
    public float velocidadMax;
    private bool miraDerecha = true;
    public bool colPies = false;
    public float fuerzaSalto;
    private Rigidbody2D rPlayer;
    private float h;
    // Start is called before the first frame update
    void Start()
    {
        rPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        giraPlayer(h);
        colPies = CheackGround.colPies;
        if (Input.GetButtonDown("Jump") && colPies)
        {
            rPlayer.velocity = new Vector2(rPlayer.velocity.x, 0f);
            rPlayer.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        }
        
    }

    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        rPlayer.AddForce(Vector2.right * velocidad * h);
        float limiteVelocidad = Mathf.Clamp(rPlayer.velocity.x, -velocidadMax, velocidadMax);
        rPlayer.velocity = new Vector2(limiteVelocidad, rPlayer.velocity.y);

    }
      void giraPlayer(float horizontal){

        if((horizontal > 0 && !miraDerecha) || (horizontal < 0 && miraDerecha) ){
           miraDerecha = !miraDerecha;
           Vector3 escalaGiro = transform.localScale;
           escalaGiro.x = escalaGiro.x * -1;
           transform.localScale = escalaGiro;
        }
    }
}
