using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSoundControl : MonoBehaviour
{
    public GameObject virtual_hand;
    public float min_boundary; 
    public float max_boundary; 
    public float change_speed; 

    AudioSource audioSource;
    public CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool contact = capsuleCollider.bounds.Contains(virtual_hand.transform.position);

        if(contact) audioSource.pitch = Mathf.Min(max_boundary, audioSource.pitch + change_speed);
        else audioSource.pitch = Mathf.Max(min_boundary, audioSource.pitch - change_speed);
    }
}
