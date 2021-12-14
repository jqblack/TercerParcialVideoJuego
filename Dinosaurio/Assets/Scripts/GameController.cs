using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject fundidoNegro;
    [SerializeField] private GameObject camara;

    public static bool gameOn = false;
    private Image sprFundido;
    public static bool playerMuerto;
    private AudioSource musica;

    private void Awake()
    {
        fundidoNegro.SetActive(true);
        
    }

    private void Start()
    {
        sprFundido = fundidoNegro.GetComponent<Image>();
        Invoke("QuitaFundido", 0.05f);
        musica = camara.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerMuerto)
        {
            musica.Stop();
            StartCoroutine("PonFC");
            playerMuerto = false;
        }
    }

    private void  QuitaFundido()
    {
        StartCoroutine("QuitaFC");
    }

    IEnumerator QuitaFC()
    {
        for(float alpha = 1f; alpha >=0; alpha -= Time.deltaTime * 2f)
        {
            sprFundido.color = new Color(sprFundido.color.r, sprFundido.color.g, sprFundido.color.b, alpha);
            yield return null;
        }

        gameOn = true;
        musica.Play();
    }

    IEnumerator PonFC()
    {
        for (float alpha = 0f; alpha <= 1; alpha += Time.deltaTime * 2f)
        {
            sprFundido.color = new Color(sprFundido.color.r, sprFundido.color.g, sprFundido.color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
