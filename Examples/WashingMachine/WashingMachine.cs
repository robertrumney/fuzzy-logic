using UnityEngine;
using FuzzyLogic;
using System.Collections.Generic;

public class WashingMachine : MonoBehaviour
{
    public float laundryAmount; // Input variable: laundry amount
    public string laundryType; // Input variable: laundry type
    public float washingTime; // Output variable: washing time

    // Define the fuzzy sets for the input variables
    LinguisticVariable laundryAmountVariable = new LinguisticVariable("Laundry Amount");
    LinguisticVariable laundryTypeVariable = new LinguisticVariable("Laundry Type");

    // Define the fuzzy sets for the output variable
    LinguisticVariable washingTimeVariable = new LinguisticVariable("Washing Time");

    // Define the fuzzy rules
    FuzzyRuleSet ruleSet = new FuzzyRuleSet();

    private void Start()
    {
        // Initialize the fuzzy sets for the input variables
        laundryAmountVariable.AddFuzzySet(new TriangularFuzzySet("Small", 0f, 1f, 2f));
        laundryAmountVariable.AddFuzzySet(new TriangularFuzzySet("Medium", 1f, 3f, 5f));
        laundryAmountVariable.AddFuzzySet(new TriangularFuzzySet("Large", 3f, 6f, 10f));

        laundryTypeVariable.AddFuzzySet(new TriangularFuzzySet("Delicates", 0f, 2f, 4f));
        laundryTypeVariable.AddFuzzySet(new TriangularFuzzySet("Cotton", 2f, 4f, 6f));
        laundryTypeVariable.AddFuzzySet(new TriangularFuzzySet("Jeans", 4f, 6f, 8f));

        // Initialize the fuzzy sets for the output variable
        washingTimeVariable.AddFuzzySet(new TriangularFuzzySet("Short", 0f, 5f, 10f));
        washingTimeVariable.AddFuzzySet(new TriangularFuzzySet("Medium", 5f, 15f, 25f));
        washingTimeVariable.AddFuzzySet(new TriangularFuzzySet("Long", 20f, 30f, 40f));

        // Define the fuzzy rules
        FuzzyRule rule1 = new FuzzyRule();
        rule1.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[0];
        rule1.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[0];
        rule1.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[0];
        ruleSet.AddRule(rule1);

        FuzzyRule rule2 = new FuzzyRule();
        rule2.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[0];
        rule2.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[1];
        rule2.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[1];
        ruleSet.AddRule(rule2);

        FuzzyRule rule3 = new FuzzyRule();
        rule3.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[0];
        rule3.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[2];
        rule3.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[1];
        ruleSet.AddRule(rule3);

        FuzzyRule rule4 = new FuzzyRule();
        rule4.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[1];
        rule4.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[0];
        rule4.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[1];
        ruleSet.AddRule(rule4);

        FuzzyRule rule5 = new FuzzyRule();
        rule5.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[1];
        rule5.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[1];
        rule5.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[1];
        ruleSet.AddRule(rule5);

        FuzzyRule rule6 = new FuzzyRule();
        rule6.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[1];
        rule6.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[2];
        rule6.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[2];
        ruleSet.AddRule(rule6);

        FuzzyRule rule7 = new FuzzyRule();
        rule7.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[2];
        rule7.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[0];
        rule7.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[1];
        ruleSet.AddRule(rule7);

        FuzzyRule rule8 = new FuzzyRule();
        rule8.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[2];
        rule8.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[1];
        rule8.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[2];
        ruleSet.AddRule(rule8);

        FuzzyRule rule9 = new FuzzyRule();
        rule9.Antecedents[laundryAmountVariable] = laundryAmountVariable.FuzzySets[2];
        rule9.Antecedents[laundryTypeVariable] = laundryTypeVariable.FuzzySets[2];
        rule9.Consequents[washingTimeVariable] = washingTimeVariable.FuzzySets[2];
        ruleSet.AddRule(rule9);
    }

    private void Update()
    {
        // Convert the laundry type string to a fuzzy set
        FuzzySet laundryTypeSet;
        if (laundryType == "Delicates")
        {
            laundryTypeSet = laundryTypeVariable.FuzzySets[0];
        }
        else if (laundryType == "Cotton")
        {
            laundryTypeSet = laundryTypeVariable.FuzzySets[1];
        }
        else // laundryType == "Jeans"
        {
            laundryTypeSet = laundryTypeVariable.FuzzySets[2];
        }

        // Compute the washing time using the fuzzy logic inference
        Dictionary<LinguisticVariable, float> inputs = new Dictionary<LinguisticVariable, float>();
        inputs[laundryAmountVariable] = laundryAmount;
        washingTime = ruleSet.Infer(inputs, washingTimeVariable);

        Debug.Log($"Laundry amount: {laundryAmount}, Laundry type: {laundryType}, Washing time: {washingTime}");
    }
}
