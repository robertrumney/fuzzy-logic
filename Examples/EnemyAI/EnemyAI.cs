using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyLogic;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform

    // Define the linguistic variables and their associated fuzzy sets
    private LinguisticVariable distanceVariable;
    private LinguisticVariable actionVariable;

    // Define the fuzzy rules
    private FuzzyRule rule1;
    private FuzzyRule rule2;
    private FuzzyRule rule3;

    // Define the fuzzy rule set
    private FuzzyRuleSet ruleSet;

    // Define the fuzzy inference system
    private FuzzyInferenceSystem fis;

    // Start is called before the first frame update
    void Start()
    {
        // Define the "distance" linguistic variable
        distanceVariable = new LinguisticVariable("distance");
        distanceVariable.AddFuzzySet(new TriangularFuzzySet("close", 0f, 20f, 40f));
        distanceVariable.AddFuzzySet(new TriangularFuzzySet("medium", 20f, 40f, 60f));
        distanceVariable.AddFuzzySet(new TriangularFuzzySet("far", 40f, 60f, 80f));

        // Define the "action" linguistic variable
        actionVariable = new LinguisticVariable("action");
        actionVariable.AddFuzzySet(new TriangularFuzzySet("attack", 0f, 0f, 0.5f));
        actionVariable.AddFuzzySet(new TriangularFuzzySet("retreat", 0.5f, 1f, 1f));

        // Define the fuzzy rules
        rule1 = new FuzzyRule();
        rule1.Antecedents[distanceVariable] = distanceVariable.FuzzySets[0];
        rule1.Consequents[actionVariable] = actionVariable.FuzzySets[0];

        rule2 = new FuzzyRule();
        rule2.Antecedents[distanceVariable] = distanceVariable.FuzzySets[1];
        rule2.Consequents[actionVariable] = actionVariable.FuzzySets[0];

        rule3 = new FuzzyRule();
        rule3.Antecedents[distanceVariable] = distanceVariable.FuzzySets[2];
        rule3.Consequents[actionVariable] = actionVariable.FuzzySets[1];

        // Define the fuzzy rule set
        ruleSet = new FuzzyRuleSet();
        ruleSet.AddRule(rule1);
        ruleSet.AddRule(rule2);
        ruleSet.AddRule(rule3);

        // Define the fuzzy inference system
        fis = new FuzzyInferenceSystem();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Set the inputs for the fuzzy inference system
        var inputs = new Dictionary<LinguisticVariable, float>();
        inputs[distanceVariable] = distance;

        // Infer the action using the fuzzy inference system
        float actionValue = fis.Infer(inputs, actionVariable, ruleSet);

        // Take action based on the fuzzy inference result
        if (actionValue < 0.5f)
        {
            // Attack the player
            Debug.Log("Attacking player");
        }
        else
        {
            // Retreat
            Debug.Log("Retreating");
        }
    }
}
