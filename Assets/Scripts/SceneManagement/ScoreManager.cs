using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    
    GameObject player;
    JointDeleter playerJointDeleter;
    PlayerController playerController;
    PickUpManagerRedux pickUpManager;

    MapGenerator mapGenerator;

    List<GameObject> visitedPlanetsList;
    public static int visitedPlanets = 0;

    public static int consumedPlanets = 0;
    public TextMeshProUGUI consumedPlanetsText;



    private void Awake()
    {
        consumedPlanets = 0;
        consumedPlanetsText.text = consumedPlanets.ToString();
        consumedPlanetsText.color = Color.white;
        visitedPlanetsList = new List<GameObject>();
        visitedPlanets = 0;


        player = GameObject.FindGameObjectWithTag("Player");
        playerJointDeleter = player.GetComponent<JointDeleter>();
        playerController = player.GetComponent<PlayerController>();
        pickUpManager = player.GetComponent<PickUpManagerRedux>();
        score = 0;
        scoreText.text = score.ToString();
        mapGenerator = FindObjectOfType<MapGenerator>();
    }

    private void Start()
    {
        foreach (var attractor in mapGenerator.attractors)
        {
            attractor.GetComponent<PlanetFuel>().OnPlanetEmpty += AddPointsForPlanet;
        }
        playerJointDeleter.OnJointDeleted += AddPointsForJoint;
        //
        playerJointDeleter.PassAttractorInfo += RecalculateVisitedAttractors;
        //
        pickUpManager.OnDiscardPickup += AddPointsForDiscard;
        playerController.OnPlayerDeath += UpdateFinalScore;
    }

    public void RecalculateVisitedAttractors(GameObject attractor)
    {
        if (!visitedPlanetsList.Contains(attractor))
        {
            visitedPlanetsList.Add(attractor);
            visitedPlanets++;
        }
    }

    public void AddPoints(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    void AddPointsForPlanet()
    {
        AddPoints(5);
        consumedPlanets++;
        consumedPlanetsText.text = consumedPlanets.ToString();
        if (consumedPlanets == mapGenerator.attractorCount)
            consumedPlanetsText.color = Color.green;
    }

    void AddPointsForJoint()
    {
        AddPoints(2);
    }

    void AddPointsForDiscard(PickableItem pickup)
    {
        AddPoints(pickup.discardValue);
    }

    void UpdateFinalScore()
    {
        finalScoreText.text = "Your score is\n" + score.ToString();
        if (score > HighscoreManager.highscore)
        {
            HighscoreManager.highscore = score;
            HighscoreManager.SaveHighscore();
        }
    }

    public int GetPoints()
    {
        return score;
    }
}
