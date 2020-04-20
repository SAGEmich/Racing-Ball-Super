using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public bool Active = true;

    void Update()
    {
        transform.rotation = Quaternion.Euler(
            45f, Time.timeSinceLevelLoad * 60f, 45f);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Active) return;
        if (other.name != "Sphere") return;

        // mogę również napisać
        // var audioSource = GetComponent<AudioSource>();
        // audioSource.Play();
        // var renderer = GetComponent<Renderer>();
        // renderer.enabled = false;
        // zamiast 28 i 29 linikjki
        // UŻYWAĆ TEGO ZAPISU GDY BEDE MUSIAŁ POZNIEJ UZYWAC TEGO KOMPONENTU

        GetComponent<AudioSource>().Play();
        GetComponent<Renderer>().enabled = false;

        Active = false;

       FindObjectOfType<GameController>().UpdateCrystalCounterText();
    }
}
