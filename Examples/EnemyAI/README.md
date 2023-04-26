# FuzzyLogic Library

The `FuzzyLogic` library is a C# library for implementing fuzzy logic systems. It provides classes for defining linguistic variables, fuzzy sets, fuzzy rules, and fuzzy rule sets, and for performing fuzzy inference using the centroid method.

## Installation

To use the `FuzzyLogic` library, simply download the `FuzzyLogic.dll` file and add it as a reference to your C# project.

## Usage

Here's an example of how to use the `FuzzyLogic` library in Unity for A.I. decision making.

Let's say we have a game where the player controls a spaceship and the A.I. controls enemy ships. The A.I. needs to decide whether to attack the player or retreat, based on the distance between the player and the A.I. ship.

First, we define our linguistic variables and their associated fuzzy sets. We define two variables: `distance` and `action`.

```csharp
// Define the "distance" linguistic variable
var distanceVariable = new LinguisticVariable("distance");
distanceVariable.AddFuzzySet(new TriangularFuzzySet("close", 0f, 20f, 40f));
distanceVariable.AddFuzzySet(new TriangularFuzzySet("medium", 20f, 40f, 60f));
distanceVariable.AddFuzzySet(new TriangularFuzzySet("far", 40f, 60f, 80f));

// Define the "action" linguistic variable
var actionVariable = new LinguisticVariable("action");
actionVariable.AddFuzzySet(new TriangularFuzzySet("attack", 0f, 0f, 0.5f));
actionVariable.AddFuzzySet(new TriangularFuzzySet("retreat", 0.5f, 1f, 1f));
```

Next, we define our fuzzy rules. We use three rules:

If the distance is close, then the action is attack.
If the distance is medium, then the action is attack with medium probability.
If the distance is far, then the action is retreat.

```csharp
// Define the fuzzy rules
var rule1 = new FuzzyRule();
rule1.Antecedents[distanceVariable] = distanceVariable.FuzzySets[0];
rule1.Consequents[actionVariable] = actionVariable.FuzzySets[0];

var rule2 = new FuzzyRule();
rule2.Antecedents[distanceVariable] = distanceVariable.FuzzySets[1];
rule2.Consequents[actionVariable] = actionVariable.FuzzySets[0];

var rule3 = new FuzzyRule();
rule3.Antecedents[distanceVariable] = distanceVariable.FuzzySets[2];
rule3.Consequents[actionVariable] = actionVariable.FuzzySets[1];
```

Then, we define the fuzzy rule set and the fuzzy inference system.

```csharp
// Define the fuzzy rule set
var ruleSet = new FuzzyRuleSet();
ruleSet.AddRule(rule1);
ruleSet.AddRule(rule2);
ruleSet.AddRule(rule3);

// Define the fuzzy inference system
var fis = new FuzzyInferenceSystem();
```
Finally, in the Update method of our EnemyAI script, we calculate the distance between the enemy and the player, set the inputs for the fuzzy inference system, infer the action using the fuzzy inference system, and take action based on the fuzzy inference result.

```csharp
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
    ShootAt(playerTransform);
}
else
{
    // Retreat from the player
    MoveAwayFrom(playerTransform);
}
```

This is just a simple example of how the FuzzyLogic library can be used in Unity for A.I. decision making. The library is flexible enough to support more complex scenarios and rule sets.
