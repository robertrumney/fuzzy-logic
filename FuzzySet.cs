using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FuzzySet
{
    public string Name { get; set; }
    public abstract float GetMembership(float value);
}

public class LinguisticVariable
{
    public string Name { get; set; }
    public List<FuzzySet> FuzzySets { get; set; }

    public LinguisticVariable(string name)
    {
        Name = name;
        FuzzySets = new List<FuzzySet>();
    }

    public void AddFuzzySet(FuzzySet fuzzySet)
    {
        FuzzySets.Add(fuzzySet);
    }
}

public class FuzzyRule
{
    public Dictionary<LinguisticVariable, FuzzySet> Antecedents { get; set; }
    public Dictionary<LinguisticVariable, FuzzySet> Consequents { get; set; }

    public FuzzyRule()
    {
        Antecedents = new Dictionary<LinguisticVariable, FuzzySet>();
        Consequents = new Dictionary<LinguisticVariable, FuzzySet>();
    }
}

public class FuzzyRuleSet
{
    public List<FuzzyRule> Rules { get; set; }

    public FuzzyRuleSet()
    {
        Rules = new List<FuzzyRule>();
    }

    public void AddRule(FuzzyRule rule)
    {
        Rules.Add(rule);
    }
}

public class FuzzyInferenceSystem
{
    public FuzzyRuleSet RuleSet { get; set; }

    public FuzzyInferenceSystem(FuzzyRuleSet ruleSet)
    {
        RuleSet = ruleSet;
    }

    public float Infer(Dictionary<LinguisticVariable, float> inputs, LinguisticVariable outputVariable)
    {
        // Implement the fuzzy inference process here.
        // It typically involves fuzzification, rule evaluation, aggregation, and defuzzification.
        throw new NotImplementedException();
    }
}
