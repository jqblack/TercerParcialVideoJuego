using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    
    [SerializeField] private Transform[] puntosMov;
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject padre;
    private int i = 0;
    private Vector3 scalaIni, scalaTem;
    private float miraDer = 1;
    // Start is called before the first frame update
    void Start()
    {
        scalaIni = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMov[i].transform.position, velocidad * Time.deltaTime);
      if(Vector2.Distance(transform.position, puntosMov[i].transform.position) < 0.1f){
            if(puntosMov[i] != puntosMov[puntosMov.Length - 1]) i++;
            else i = 0;
            miraDer = Mathf.Sign(puntosMov[i].transform.position.x - transform.position.x);

            gira(miraDer);
      }
    }

/*public void Muere(){
    Destroy(padre);
    }*/
    private void gira(float lado){
         if(miraDer == -1){
             scalaTem = transform.localScale;
             scalaTem.x = scalaTem.x * -1;
         }else scalaTem = scalaIni;

         transform.localScale = scalaTem;
    }
}
