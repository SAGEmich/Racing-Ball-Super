using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Text CrystalCounterText;
    public Text CountDownText;
    public Text EndOfGameText;

    public AudioClip GameWinSound;
    public AudioClip GameLooseSound;

    void Start()
    {
        UpdateCrystalCounterText();

        StartCoroutine(CountDownCoroutine());
    }

   
    void Update()
    {
     
    }

    IEnumerator CountDownCoroutine()
    {
        SetIfSphereCanMove(false);
        CountDownText.enabled = true;

        for (int i = 5; i > 0; i--)
        {
            CountDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        CountDownText.text = "Start!";
        SetIfSphereCanMove(true);
        yield return new WaitForSeconds(1f);

        CountDownText.enabled = false;
    }

    void SetIfSphereCanMove(bool canMove)
    {
        var sphere = FindObjectOfType<Sphere>();
        sphere.CanMove = canMove;

        sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void UpdateCrystalCounterText()
    {
        var crystals = FindObjectsOfType<Crystal>();

        var crystalCount = crystals.Length;
        var crystalInactive = crystals.Count(crystal => !crystal.Active);

        var text = crystalInactive + " / " + crystalCount;
        CrystalCounterText.text = text;
    }

    public void CheckIfEndOfGame()
    {
        var endOfGame = FindObjectsOfType<Crystal>().All(crystal => !crystal.Active);

        if (!endOfGame) return;

        EndOfGame(true);
    }

    public void EndOfGame(bool win)
    {
        StartCoroutine(EndOfGameCoroutine(win));
    }

    IEnumerator EndOfGameCoroutine(bool win)
    {
        SetIfSphereCanMove(false);

        EndOfGameText.enabled = true;
        EndOfGameText.text = win ? "WIN" : "LOOSE";

        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = win ? GameWinSound : GameLooseSound;
        audioSource.Play();

        yield return new WaitForSeconds(3f);

       
        Application.Quit();
    }
}
