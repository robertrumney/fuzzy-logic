# Vehicle Control with Fuzzy Logic in Unity

This is an example Unity project that demonstrates how to use the Fuzzy Logic library to control a vehicle using `WheelCollider` components. The vehicle's movement is controlled by the engine power, brake power, and steering angle, which are determined using fuzzy logic based on the current speed and steering angle.

## How It Works

The vehicle control script is defined in `Assets/Scripts/VehicleController.cs`. This script uses the Fuzzy Logic library to determine the appropriate engine power, brake power, and steering angle based on the current speed and steering angle.

The Fuzzy Logic library is defined in `Assets/Scripts/FuzzyLogic.cs`. This library provides classes for defining fuzzy sets, linguistic variables, fuzzy rules, and a fuzzy inference system. In the `VehicleController` script, we define the fuzzy sets, linguistic variables, and rules that are used to determine the appropriate engine power, brake power, and steering angle.

The `VehicleController` script also uses the `WheelCollider` components to control the vehicle's movement. We calculate the current speed and steering angle based on the `WheelCollider` values, and use fuzzy logic to determine the appropriate engine power, brake power, and steering angle. We then apply the calculated engine power or brake power to the `WheelCollider` components to control the vehicle's movement.

## Future Improvements

This project is just a starting point for using fuzzy logic to control a vehicle in Unity. Some possible improvements that could be made include:

- Adding more fuzzy sets and rules to improve the accuracy of the control system
- Implementing a more sophisticated control system that takes into account other factors, such as terrain and obstacles
- Adding visualizations or debug information to help understand how the control system is working
