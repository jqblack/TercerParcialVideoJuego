using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{

    [SerializeField] private GameObject fondoLejosGo;
    [SerializeField] private GameObject fondoMedioGo;
    [SerializeField] private float velocidadScroll;

    private Renderer fondoLejosR, fondoMedioR;
    private float iniCamX, difCamX;

    public Vector2 minCamPos, maxCamPos;
    public GameObject seguir;
    public float movSuave;

    private Vector2 velocidad;
    // Start is called before the first frame update
    void Start()
    {
        fondoLejosR = fondoLejosGo.GetComponent<Renderer>();
        fondoMedioR = fondoMedioGo.GetComponent<Renderer>();
        iniCamX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        difCamX = iniCamX - transform.position.x;
        Vector2 offsetFL = new Vector2(difCamX * velocidadScroll * -1, 0.0f);
        Vector2 offsetML = new Vector2(difCamX * (velocidadScroll * 1.5f)* -1, 0.0f);

        fondoLejosR.material.mainTextureOffset = offsetFL;
        fondoMedioR.material.mainTextureOffset = offsetML;
        fondoLejosGo.transform.position = new Vector3(transform.position.x, fondoLejosGo.transform.position.y, fondoLejosGo.transform.position.z);
        fondoMedioGo.transform.position = new Vector3(transform.position.x, fondoMedioGo.transform.position.y, fondoMedioGo.transform.position.z);

        float posX = Mathf.SmoothDamp(transform.position.x, seguir.transform.position.x, ref velocidad.x, movSuave);
        float posY = Mathf.SmoothDamp(transform.position.y, seguir.transform.position.y, ref velocidad.y, movSuave);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
             Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);

    }
}
