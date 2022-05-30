using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTiempo : MonoBehaviour
{
    private bool tiempoParado;
    [SerializeField] private float posInicialx;
    
    public void Mover()
    {
        tiempoParado = false;
        StartCoroutine(Mover1Unidad());
    }
    private IEnumerator Mover1Unidad()
    {
        yield return new WaitForSeconds(1);
        if (!tiempoParado)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y);
            StartCoroutine(Mover1Unidad());
        }
        
    }
    public void TiempoParado()
    {
        tiempoParado = true;
    }

    public void ReiniciarPosicion()
    {
        transform.localPosition = new Vector3(posInicialx, transform.localPosition.y);
    }
    
}
