﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {
	private Text text;
	private Image background;
	private WaveControl waveControl;
	private GameControl gameControl;
	public float fadeSpeed = 50.0f;
	public float fadeHold = 3.0f;

    void Start() {
		text = GameObject.Find("WaveBanner Text").GetComponent<Text>();
		background = GameObject.Find("WaveBanner Background").GetComponent<Image>();
		waveControl = WaveControl.instance;
		gameControl = GameControl.instance;
		waveControl.onWaveStartedCallback += WaveStart;
    }

	private void WaveStart() {
		print(gameControl.GetGameState());
		StartCoroutine(DisplayBanner(fadeSpeed, fadeHold, gameControl.GetWaveNumber()));
	}

	private IEnumerator DisplayBanner(float fadeSpeed, float hold, int wave) {
		text.gameObject.SetActive(true);
		background.gameObject.SetActive(true);
		text.text = "WAVE " + wave;
		Color textColor = text.color;
		Color backgroundColor = background.color;
		float fadeAmount;
		while (text.color.a < 1) {
			textColor = text.color;
			backgroundColor = background.color;
			fadeAmount = textColor.a + (fadeSpeed * Time.deltaTime);
			text.color = new Color(textColor.r, textColor.g, textColor.b, fadeAmount);
			background.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, fadeAmount);
			yield return null;
		}
		yield return new WaitForSeconds(hold);
		while (text.color.a > 0) {
			textColor = text.color;
			backgroundColor = background.color;
			fadeAmount = textColor.a - (fadeSpeed * Time.deltaTime);
			text.color = new Color(textColor.r, textColor.g, textColor.b, fadeAmount);
			background.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, fadeAmount);
			yield return null;
		}
		text.gameObject.SetActive(false);
		background.gameObject.SetActive(false);
		yield return null;
	}

}
