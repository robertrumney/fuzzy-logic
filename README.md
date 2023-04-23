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
using FuzzyLogic;

public class Example
{
  public void Routine()
  {
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
    
    // Display the output in console
    print(output);
  }
}
```

In this example, the fuzzy logic system is used to determine the acceleration of an object in a game based on its distance from a target and its current speed. Two fuzzy rules are defined based on the inputs and outputs, and the `Infer` method is called to get the acceleration value. You can modify the fuzzy logic system definition and rules to suit your specific game requirements.

## Classes

### FuzzySet

An abstract class that represents a fuzzy set. It has a `Name` property and an abstract `GetMembership` method that calculates the membership degree of a value in the set.

### LinguisticVariable

A class that represents a linguistic variable. It has a `Name` property and a list of `FuzzySet` objects that belong to it. You can add `FuzzySet` objects to the variable using the `AddFuzzySet` method.

### FuzzyRule

A class that represents a fuzzy rule. It has two dictionaries: `Antecedents` and `Consequents`. The `Antecedents` dictionary maps `LinguisticVariable` objects to their corresponding `FuzzySet` objects, while the `Consequents` dictionary maps `LinguisticVariable` objects to their corresponding `FuzzySet` objects.

### FuzzyRuleSet

A class that represents a fuzzy rule set. It has a list of `FuzzyRule` objects. You can add `FuzzyRule` objects to the rule set using the `AddRule` method.

### FuzzyInferenceSystem

A class that represents a fuzzy inference system. It has a `FuzzyRuleSet` object. You can create a `FuzzyInferenceSystem` object by passing a `FuzzyRuleSet` object to its constructor. It has an `Infer` method that takes a dictionary of input values and a `LinguisticVariable` object representing the output variable. The `Infer` method returns a float value representing the output.

## Conclusion

This fuzzy logic system implementation can be used in games to make decisions based on fuzzy inputs. You can use it to create AI agents that make decisions based on a combination of factors like distance, speed, health, and more. By modifying the fuzzy sets and rules, you can fine-tune the behavior of the agents to suit your game requirements.
