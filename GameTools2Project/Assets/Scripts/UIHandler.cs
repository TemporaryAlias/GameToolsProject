﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public Image fadeImage, rotLockNotif;

    public Sprite rotLocked, rotUnlocked;

    public float fadeTime; 
    

    Color fadeColour;

	void Start () {
        fadeColour = new Color(0, 0, 0, 1);
        StartCoroutine("FadeIn");

        rotLockNotif.sprite = rotUnlocked;
    }

    public void StartFadeOut() {
        StartCoroutine("FadeOut");
    }

    public void StartImageFade(Image image) {
        StartCoroutine("ImageFade", image);
    }

    public void StartTextFade(Text text) {
        StartCoroutine("TextFade", text);
    }

    public void StartImageFadeIn(Image image) {
        StartCoroutine("ImageFadeIn", image);
    }

    public void StarTextFadeIn(Text text) {
        StartCoroutine("TextFadeIn", text);
    }

    public IEnumerator ImageFade(Image imageToFade) {
        imageToFade.gameObject.SetActive(true);

        Color imageColour = imageToFade.color;
        imageColour.a = 1;

        while (imageColour.a >= 0) {
            imageColour.a -= 0.025f;
            imageToFade.color = imageColour;

            yield return new WaitForSeconds(fadeTime);
        }

        imageToFade.gameObject.SetActive(false);
    }

    public IEnumerator TextFade(Text textToFade) {
        textToFade.gameObject.SetActive(true);

        Color textColour = textToFade.color;
        textColour.a = 1; 

        while (textColour.a >= 0) {
            textColour.a -= 0.025f;
            textToFade.color = textColour;

            yield return new WaitForSeconds(fadeTime);
        }

        textToFade.gameObject.SetActive(false);
    }

    public IEnumerator ImageFadeIn(Image imageToFade)  {
        imageToFade.gameObject.SetActive(true);

        Color imageColour = imageToFade.color;
        imageColour.a = 0;

        while (imageColour.a <= 1)  {
            imageColour.a += 0.025f;
            imageToFade.color = imageColour;

            yield return new WaitForSeconds(fadeTime);
        }
    }

    public IEnumerator TextFadeIn(Text textToFade)  {
        textToFade.gameObject.SetActive(true);

        Color textColour = textToFade.color;
        textColour.a = 0;

        while (textColour.a <= 1) {
            textColour.a += 0.025f;
            textToFade.color = textColour;

            yield return new WaitForSeconds(fadeTime);
        }
    }

    public IEnumerator FadeIn() {
        fadeImage.gameObject.SetActive(true);

        while (fadeColour.a >= 0) {
            fadeColour.a -= 0.025f;
            fadeImage.color = fadeColour;

            yield return new WaitForSeconds(fadeTime);
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut() {
        fadeImage.gameObject.SetActive(true);

        fadeColour.a = 0;

        fadeImage.color = fadeColour;

        while (fadeColour.a <= 1) {
            fadeColour.a += 0.025f;
            fadeImage.color = fadeColour;

            yield return new WaitForSeconds(fadeTime);
        }
    }

    public void RotLockChange() {
        if (rotLockNotif.sprite == rotLocked) {
            rotLockNotif.sprite = rotUnlocked;
        } else {
            rotLockNotif.sprite = rotLocked;
        }
    }

}
