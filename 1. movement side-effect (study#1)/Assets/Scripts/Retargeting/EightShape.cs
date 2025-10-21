using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightShape : MonoBehaviour
{
    float cycle = 0.5f; 
    float time = 0;
    float max_amplitude = 0.1f;
    Vector3 initialLocalPos;
    Vector3 initialScale;


    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = transform.localPosition;
        initialScale = transform.localScale;

        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.localPosition = initialLocalPos;

        Vector3 up = transform.up;
        Vector3 right = transform.right;

        transform.position += up * max_amplitude / 2 * Mathf.Sin((time/cycle * 2 * Mathf.PI));
        transform.position += right * max_amplitude * Mathf.Sin((time/(cycle*2) * 2 * Mathf.PI));        

    }

    public void deactivate(){
        transform.localScale = new Vector3 (0,0,0);
    }

    public void activate(){
        time = 0;
        transform.localScale = initialScale;
    }
}
