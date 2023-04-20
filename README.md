# Fuzzy Logic System in C#

This is a basic implementation of a fuzzy logic system in C#. The code defines classes for FuzzySet, LinguisticVariable, FuzzyRule, FuzzyRuleSet, and FuzzyInferenceSystem.

## Usage

To use this implementation in your C# project, follow these steps:

1. Copy the code into your project.
2. Create `LinguisticVariable`, `FuzzySet`, and `FuzzyRule` objects to define your fuzzy logic system.
3. Add the `FuzzySet` objects to the `LinguisticVariable` using the `AddFuzzySet` method.
4. Add the `FuzzyRule` objects to a `FuzzyRuleSet` using the `AddRule` method.
5. Create a `FuzzyInferenceSystem` object using the `FuzzyRuleSet`.
6. Call the `Infer` method of the `FuzzyInferenceSystem` object to get the output value based on the input values.

Here's an example usage of this implementation within the context of a game:

```csharp
// Create the linguistic variables
LinguisticVariable distance = new LinguisticVariable("Distance");
LinguisticVariable speed = new LinguisticVariable("Speed");
LinguisticVariable acceleration = new LinguisticVariable("Acceleration");

// Create the fuzzy sets for each linguistic variable
FuzzySet near = new TriangleFuzzySet("Near", 0, 0, 10);
FuzzySet far = new TriangleFuzzySet("Far", 5, 10, 15);
FuzzySet slow = new TriangleFuzzySet("Slow", 0, 0, 5);
FuzzySet fast = new TriangleFuzzySet("Fast", 5, 10, 15);
FuzzySet negative = new TriangleFuzzySet("Negative", -15, -10, -5);
FuzzySet positive = new TriangleFuzzySet("Positive", 5, 10, 15);

// Add the fuzzy sets to their corresponding linguistic variables
distance.AddFuzzySet(near);
distance.AddFuzzySet(far);
speed.AddFuzzySet(slow);
speed.AddFuzzySet(fast);
acceleration.AddFuzzySet(negative);
acceleration.AddFuzzySet(positive);

// Create the fuzzy rules
FuzzyRule rule1 = new FuzzyRule();
rule1.Antecedents.Add(distance, near);
rule1.Antecedents.Add(speed, slow);
rule1.Consequents.Add(acceleration, negative);

FuzzyRule rule2 = new FuzzyRule();
rule2.Antecedents.Add(distance, far);
rule2.Antecedents.Add(speed, fast);
rule2.Consequents.Add(acceleration, positive);

// Add the fuzzy rules to a rule set
FuzzyRuleSet ruleSet = new FuzzyRuleSet();
ruleSet.AddRule(rule1);
ruleSet.AddRule(rule2);

// Create the fuzzy inference system
FuzzyInferenceSystem fis = new FuzzyInferenceSystem(ruleSet);

// Define the input values
Dictionary<LinguisticVariable, float> inputs = new Dictionary<LinguisticVariable, float>();
inputs.Add(distance, 6);
inputs.Add(speed, 9);

// Infer the output value
float output = fis.Infer(inputs, acceleration);
```

In this example, the fuzzy logic system is used to determine the acceleration of an object in a game based on its distance from a target and its current speed. Two fuzzy rules are defined based on the inputs and outputs, and the `Infer` method is called to get the acceleration value. You can modify the fuzzy
