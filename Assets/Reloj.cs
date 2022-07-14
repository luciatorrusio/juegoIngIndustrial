using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reloj : MonoBehaviour
{
    int seconds;
    int minutes;
    bool corriendo;
    // Start is called before the first frame update
    void Start()
    {
        corriendo = false;
        seconds = 0;
        minutes = 0;
        gameObject.GetComponent<TextMeshProUGUI>().text = "00:00";
        
    }

    public void ComenzarReloj()
    {
        corriendo = true;
        StartCoroutine(Onesecond());
        
    }
    IEnumerator Onesecond()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        
        if (corriendo)
        {
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            //After we have waited 5 seconds print the time again.
            gameObject.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + ":" + ((seconds < 10) ? ("0" + seconds.ToString()) : seconds.ToString());
            StartCoroutine(Onesecond());
        }
    }

    public void FrenarReloj()
    {
        corriendo = false;
    }

    public void ResetearReloj()
    {

        corriendo = false;
        seconds = 0;
        minutes = 0;
        gameObject.GetComponent<TextMeshProUGUI>().text = "00:00";
    }
}
