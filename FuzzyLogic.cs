using UnityEngine;

using System;
using System.Linq;
using System.Collections.Generic;

namespace FuzzyLogic
{
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

    public float Infer(Dictionary<LinguisticVariable, float> inputs, LinguisticVariable outputVariable)
    {
        // Step 1: Fuzzification
        Dictionary<FuzzySet, float> inputMemberships = new Dictionary<FuzzySet, float>();
        foreach (var input in inputs)
        {
            if (!RuleSet.Rules.Any(rule => rule.Antecedents.ContainsKey(input.Key)))
            {
                throw new ArgumentException($"Linguistic variable {input.Key.Name} is not used in any rule antecedent.");
            }
            foreach (var fuzzySet in input.Key.FuzzySets)
            {
                float membership = fuzzySet.GetMembership(input.Value);
                inputMemberships.Add(fuzzySet, membership);
            }
        }

        // Step 2: Rule evaluation
        Dictionary<FuzzySet, float> ruleOutputs = new Dictionary<FuzzySet, float>();
        foreach (var rule in RuleSet.Rules)
        {
            if (!rule.Antecedents.Keys.All(input => inputs.ContainsKey(input)))
            {
                throw new ArgumentException($"Not all antecedent linguistic variables of rule {rule} are present in the inputs.");
            }
            float ruleOutput = float.MaxValue;
            foreach (var antecedent in rule.Antecedents)
            {
                float antecedentMembership = inputMemberships[antecedent.Value];
                if (antecedentMembership < ruleOutput)
                {
                    ruleOutput = antecedentMembership;
                }
            }
            if (ruleOutput < float.MaxValue)
            {
                if (!rule.Consequents.ContainsKey(outputVariable))
                {
                    throw new ArgumentException($"Output variable {outputVariable.Name} is not present in the consequents of rule {rule}.");
                }
                ruleOutputs[rule.Consequents[outputVariable]] = ruleOutput;
            }
        }

        if (ruleOutputs.Count == 0)
        {
            throw new InvalidOperationException("No rules were fired.");
        }

        // Step 3: Aggregation
        FuzzySet aggregatedFuzzySet = null;
        float aggregatedMembership = 0f;
        foreach (var outputMembership in ruleOutputs)
        {
            if (aggregatedFuzzySet == null)
            {
                aggregatedFuzzySet = outputMembership.Key;
            }
            else
            {
                aggregatedFuzzySet = aggregatedFuzzySet.Union(outputMembership.Key);
            }
            aggregatedMembership = Math.Max(aggregatedMembership, outputMembership.Value);
        }

        // Step 4: Defuzzification
        if (aggregatedFuzzySet == null)
        {
            throw new InvalidOperationException("No rule output fuzzy sets were generated.");
        }
        return aggregatedFuzzySet.Centroid(aggregatedMembership);
    }
}
