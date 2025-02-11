﻿using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text infoText;
    public GameObject gameOverImage;
    public GameObject tutorialImage;
    public Text windStrengthText;
    public Button restartButton;
    public Button mainButton;
    public GameObject gameOverPanel;
    public AudioClip[] bgm;
    public Text scoreText;
    public Text bestScoreText;

    private float hitPoint;
	private int score;
    private int bestScore;
	private int numberOfRope;
	private int stage;
    private AudioSource audio;
	private Vector2 windStrength;

	private MapController mapController;

	private Player player;

	private bool isGameOver;

	// Make map objects
	void Awake() {
		player = FindObjectOfType<Player> ();
		hitPoint = 3.0f;
		score = 0;
		numberOfRope = 0;
		stage = 1;
		isGameOver = false;
		windStrength = new Vector2 (0, 0);
		mapController = GetComponent<MapController>();
        if (PlayerPrefs.GetInt("BestScore", -1) == -1)
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        BackgroundMusicManager.instance.PlaySingle(bgm[Random.Range(0, bgm.Length)]);
    }

	// Use this for initialization
	void Start () {
        bestScore = PlayerPrefs.GetInt("BestScore");
        gameOverImage.SetActive(false);
        windStrengthText.text = "";
        restartButton.gameObject.SetActive(false);
        mainButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        scoreText.gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (!isGameOver) { 
            // Show game information
            setText (ToString ());

            if (score >= bestScore)
            {
                bestScore = score;
            }

            bestScoreText.text = string.Format("BestScore\n{0}", bestScore);

            // Potentiate player with wind
            player.wind (windStrength.x, windStrength.y);

			// If you want to potentiate RObjects with wind, remove this comment(//)
			//		foreach(RObject tempObject in FindObjectsOfType<RObject>()) {
			//			tempObject.wind (windStrength);
			//		}

			showWindStrength ();
		}

        if (!BackgroundMusicManager.instance.player.isPlaying)
        {
            BackgroundMusicManager.instance.PlaySingle(bgm[Random.Range(0, bgm.Length)]);
        }
	}

	public void setWindStrength(Vector2 windStrength) {
		this.windStrength = windStrength;
	}

	public void setWindStrength(float windStrength_x, float windStrength_y) {
		this.windStrength = new Vector2 (windStrength_x, windStrength_y);
	}

	public void setText(string str) {
		infoText.text = str;
	}

	public void nextStage() {
		stage++;
	}

	public float getHP() {
		return hitPoint;
	}

	public void setHP(float hitPoint) {
		this.hitPoint = hitPoint;
	}

	public void addHP(float hp) {
		setHP (getHP () + hp);
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public void addScore(int score) {
		setScore (getScore () + score);
	}

	public int getNumberOfRope() {
		return numberOfRope;
	}

	public void setNumberOfRope(int numberOfRope) {
		this.numberOfRope = numberOfRope;
	}

	public void addNumberOfRope(int numberOfRope) {
		setNumberOfRope(getNumberOfRope() + numberOfRope);
	}

	public int getStage() {
		return stage;
	}

	public override string ToString() {
        return string.Format("Score : {0}", score);
	}

    public void gameOverFunction ()
    {
        Destroy(player);
        gameOverImage.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainButton.gameObject.SetActive(true);
        gameOverPanel.SetActive(true);
        isGameOver = true;
        scoreText.gameObject.SetActive(true);
        scoreText.text = string.Format("{0}", score);

        PlayerPrefs.SetInt("BestScore", bestScore);

#if UNITY_EDITOR
        Debug.Log ("Game Over!");
#endif
    }

    public void showWindStrength()
    {
        if (windStrength == new Vector2(0, 0))
            windStrengthText.text = "0";
        else
            windStrengthText.text = "( x : " + windStrength.x + ", y : " + windStrength.y + " )";
    }
}