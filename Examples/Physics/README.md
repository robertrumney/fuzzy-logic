# JumpController with Fuzzy Logic

This is a Unity C# script that uses Fuzzy Logic to control the jumping behavior of a Rigidbody in a 3D game. The script computes the desired jump height based on the distance between the Rigidbody and the ground, using a Fuzzy Rule Set that maps the distance to the desired jump height.

## How it works

The JumpController script uses Fuzzy Logic to compute the desired jump height of a Rigidbody based on the distance between the Rigidbody and the ground. The goal is to achieve a smooth, natural-looking jump that adapts to the environment and feels responsive to the player.

The script implements the four steps of Fuzzy Logic:

1. Fuzzification: The script computes the membership degree of each input value (distance to the ground) in each fuzzy set of the input linguistic variable (distanceToGround).
2. Rule evaluation: The script evaluates each rule by computing the minimum membership degree of its antecedents.
3. Aggregation: The script aggregates the output fuzzy sets of the fired rules by taking their union and computing their maximum membership degree.
4. Defuzzification: The script computes the centroid of the aggregated fuzzy set and returns it as the output value.

The result of this process is a desired jump height that depends on the distance to the ground. When the distance to the ground is high, the jump is lower, and when the distance to the ground is low, the jump is higher. This behavior is controlled by the fuzzy logic rules defined in the Fuzzy Rule Set.

## How to use

To use the JumpController script in your Unity project, follow these steps:

1. Attach the script to a GameObject that has a Rigidbody component.
2. Assign the `distanceToGround` LinguisticVariable, the `jumpHeight` LinguisticVariable, and the `ruleSet` FuzzyRuleSet in the inspector.
3. Tune the fuzzy sets and rules of the `distanceToGround` and `jumpHeight` variables to achieve the desired behavior.
