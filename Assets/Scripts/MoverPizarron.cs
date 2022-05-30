using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPizarron : MonoBehaviour
{
    [SerializeField] private float posGrandex;
    [SerializeField] private float posGrandey;
    [SerializeField] private float posChicax;
    [SerializeField] private float posChicay;
    [SerializeField] private float escalaGrandex;
    [SerializeField] private float escalaGrandey;
    [SerializeField] private float escalaChicax;
    [SerializeField] private float escalaChicay;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(posChicax, posChicay);
        transform.localScale = new Vector3(escalaChicax, escalaChicay);
    }

    public void Agrandar()
    {
        transform.position = new Vector3(posGrandex, posGrandey);
        transform.localScale = new Vector3(escalaGrandex, escalaGrandey);
    }
    public void Achicar()
    {
        transform.position = new Vector3(posChicax, posChicay);
        transform.localScale = new Vector3(escalaChicax, escalaChicay);
    }
}
