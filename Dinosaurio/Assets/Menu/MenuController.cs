using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Opciones Generales")]
    [SerializeField] GameObject pantallaMenu;
    [SerializeField] GameObject pantallaOpciones;
    [SerializeField] float tiempoCambiaOpcion;


    [Header("Elementos menu")]
    [SerializeField] SpriteRenderer comenzar;
    [SerializeField] SpriteRenderer opciones;
    [SerializeField] SpriteRenderer salir;


    [Header("Sprites menu")]
    [SerializeField] Sprite comenzar_off;
    [SerializeField] Sprite comenzar_on;
    [SerializeField] Sprite opciones_off;
    [SerializeField] Sprite opciones_on;
    [SerializeField] Sprite salir_off;
    [SerializeField] Sprite salir_on;

    [Header("Sprites menu")]
    [SerializeField] AudioSource snd_opcion;
    [SerializeField] AudioSource snd_seleccion;

    int pantalla;
    int opcionMenu, opcionMenuAnt;
    bool pulsadoSubmit;
    float v, h;
    float tiempoV, tiempoH;
 

    void Awake()
    {
        pantalla = 0;
        tiempoV = tiempoH = 0;
        AjustaOpciones();
        
    }

    private void AjustaOpciones()
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonUp("Submit")) pulsadoSubmit = false;
        if (v == 0) tiempoV = 0;
        if (pantalla == 0) MenuPrincipal();
    }
    void MenuPrincipal()
    {
        if (v != 0)
        {
            if (tiempoV == 0 || tiempoV > tiempoCambiaOpcion)
            {
                if (v == 1 && opcionMenu > 1) SeleccionMenu(opcionMenu - 1);
                if (v == -1 && opcionMenu < 3) SeleccionMenu(opcionMenu + 1);
                if (tiempoV > tiempoCambiaOpcion) tiempoV = 0;
            }
            tiempoV += Time.deltaTime;
        }
    }
    void SeleccionMenu(int op)
    {
        snd_opcion.Play();
        opcionMenu = op;
        if (op == 1) comenzar.sprite = comenzar_on;
        if (op == 2) opciones.sprite = opciones_on;
        if (op == 3) salir.sprite = salir_on;
        if (opcionMenuAnt == 1) comenzar.sprite = comenzar_off;
        if (opcionMenuAnt == 2) opciones.sprite = opciones_off;
        if (opcionMenuAnt == 3) salir.sprite = salir_off;
        opcionMenuAnt = op;

    }
}
