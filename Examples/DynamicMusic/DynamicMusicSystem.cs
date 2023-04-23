using UnityEngine;
using System.Collections.Generic;
using FuzzyLogic;

public class DynamicMusicSystem : MonoBehaviour
{
    // Define the linguistic variables for the music system
    LinguisticVariable playerHealth;
    LinguisticVariable enemyCount;
    LinguisticVariable musicIntensity;

    // Define the fuzzy sets for the music system
    FuzzySet low;
    FuzzySet medium;
    FuzzySet high;

    // Define the fuzzy rules for the music system
    FuzzyRuleSet ruleSet;

    // Define the audio sources for the different music intensities
    public AudioSource lowIntensityMusic;
    public AudioSource mediumIntensityMusic;
    public AudioSource highIntensityMusic;

    // Define the API for the music system
    public void SetPlayerHealth(float health)
    {
        playerHealth.FuzzySets[0].Name = GetHealthName(health);
    }

    public void SetEnemyCount(float count)
    {
        enemyCount.FuzzySets[0].Name = GetEnemyCountName(count);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Define the linguistic variables
        playerHealth = new LinguisticVariable("PlayerHealth");
        enemyCount = new LinguisticVariable("EnemyCount");
        musicIntensity = new LinguisticVariable("MusicIntensity");

        // Define the fuzzy sets
        low = new TrapezoidalFuzzySet("Low", 0, 0, 20, 40);
        medium = new TriangularFuzzySet("Medium", 20, 50, 80);
        high = new TrapezoidalFuzzySet("High", 60, 80, 100, 100);

        // Add the fuzzy sets to the linguistic variables
        playerHealth.AddFuzzySet(low);
        enemyCount.AddFuzzySet(low);
        musicIntensity.AddFuzzySet(low);

        playerHealth.AddFuzzySet(medium);
        enemyCount.AddFuzzySet(medium);
        musicIntensity.AddFuzzySet(medium);

        playerHealth.AddFuzzySet(high);
        enemyCount.AddFuzzySet(high);
        musicIntensity.AddFuzzySet(high);

        // Define the fuzzy rules
        ruleSet = new FuzzyRuleSet();

        // If the player health is low and the enemy count is high, increase the music intensity
        FuzzyRule rule1 = new FuzzyRule();
        rule1.Antecedents.Add(playerHealth, low);
        rule1.Antecedents.Add(enemyCount, high);
        rule1.Consequents.Add(musicIntensity, medium);
        ruleSet.AddRule(rule1);

        // If the player health is medium and the enemy count is medium, keep the music intensity the same
        FuzzyRule rule2 = new FuzzyRule();
        rule2.Antecedents.Add(playerHealth, medium);
        rule2.Antecedents.Add(enemyCount, medium);
        rule2.Consequents.Add(musicIntensity, low);
        ruleSet.AddRule(rule2);

        // If the player health is high, decrease the music intensity
        FuzzyRule rule3 = new FuzzyRule();
        rule3.Antecedents.Add(playerHealth, high);
        rule3.Consequents.Add(musicIntensity, high);
        ruleSet.AddRule(rule3);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input values for the fuzzy inference
        float playerHealthValue = CalculatePlayerHealth();
        float enemyCountValue =     CalculateEnemyCount();

        // Set the input values for the fuzzy inference
        SetPlayerHealth(playerHealthValue);
        SetEnemyCount(enemyCountValue);

        // Perform the fuzzy inference
        Dictionary<LinguisticVariable, float> inputs = new Dictionary<LinguisticVariable, float>();
        inputs.Add(playerHealth, playerHealthValue);
        inputs.Add(enemyCount, enemyCountValue);
        float musicIntensityValue = ruleSet.Infer(inputs, musicIntensity);

        // Set the music intensity based on the fuzzy inference result
        if (musicIntensityValue < 0.33f)
        {
            lowIntensityMusic.volume = 1f;
            mediumIntensityMusic.volume = 0f;
            highIntensityMusic.volume = 0f;
        }
        else if (musicIntensityValue < 0.66f)
        {
            lowIntensityMusic.volume = 0f;
            mediumIntensityMusic.volume = 1f;
            highIntensityMusic.volume = 0f;
        }
        else
        {
            lowIntensityMusic.volume = 0f;
            mediumIntensityMusic.volume = 0f;
            highIntensityMusic.volume = 1f;
        }
    }

    // Calculate the player health value based on the game state
    float CalculatePlayerHealth()
    {
        float playerHealth = 0f;
        // TODO: Calculate the player health based on the game state
        return playerHealth;
    }

    // Calculate the enemy count value based on the game state
    float CalculateEnemyCount()
    {
        float enemyCount = 0f;
        // TODO: Calculate the enemy count based on the game state
        return enemyCount;
    }

    // Get the name of the health fuzzy set based on the player health value
    string GetHealthName(float health)
    {
        if (health < 20f)
        {
            return "VeryLow";
        }
        else if (health < 40f)
        {
            return "Low";
        }
        else if (health < 60f)
        {
            return "Medium";
        }
        else if (health < 80f)
        {
            return "High";
        }
        else
        {
            return "VeryHigh";
        }
    }

    // Get the name of the enemy count fuzzy set based on the enemy count value
    string GetEnemyCountName(float count)
    {
        if (count < 10f)
        {
            return "VeryLow";
        }
        else if (count < 20f)
        {
            return "Low";
        }
        else if (count < 30f)
        {
            return "Medium";
        }
        else if (count < 40f)
        {
            return "High";
        }
        else
        {
            return "VeryHigh";
        }
    }
}
