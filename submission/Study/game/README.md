# User Study

This Unity application was developed for the user study for predicting body movements. The participant is virtually placed via OptiTracks motion capturing system and an HTC Vive as head mounted device in a cage in a tropical environment. Via external adjustments the motion captured body movements are getting intercepted, changed and projected on the participants virtual model based on different prediction values.

### Body Visualization and Familiarization

At start the participant is located without any configuration in a large cage in a tropical environment. Wasps are closing in trying to sting the player's avatar. The participant can get used to its surroundings and is encouraged to slap the wasps as instructed beforehand before the actual study measurements begin.

### Performance Measurements

After the user had time to familiarize, the game is getting restarted. Now the game performance measurements with various prediction mechanisms are getting recorded.
Wasp are engaging the player through the four openings of the cage trying to sting its target. The Player can kill a wasp by slapping it with its left or right hand while the player got stung successfully by the wasp when it reaches the player's torso. Either way the wasp disappears instantly. The number of incoming wasps increases over time to a total of 100 bees.

# Configuration and Usage Instructions

### Configuration

For being able to project the body movements on the model from the motion capturing system, OptiTracks Unity plugin is required ([Link](https://optitrack.com/downloads/plugins.html#unity-plugin)). 

It should be configured on the "Client - OptiTrack" game object with the **ServerAddress** being the IP address of the streaming OptiTrack host and on the "Avatar" game object with **Skeleton Asset Name** the skeleton id defined inside the OptiTrack client.

### Study Conduct

Before each individual condition and before the scene can be started, the **probandId** and the **condition** have to be specified on the **GameController** object for being able to associate results to the participant and a condition.

Condition-IDs can be mapped to prediction models as follows:
- 1: 0 frames prediction
- 2: 12 frames prediction
- 3: 24 frames prediction
- 4: 36 frames prediction
- 5: 48 frames prediction
- 6: -12 frames prediction

After each condition the participant completes the questionnaire outside the virtual environment on a laptop ([Google-Questionnaire](https://docs.google.com/forms/d/1y2iOJb1yi_whFSzbAYHKQp34-JEaJwyAfJjkTT3wq4U/)). The study master has to initialize the questionnaire with the participant and condition id beforehand.

# Performance Results

The performance results are stored for each participant and each condition inside the game's main folder as CSV file. The name of this file (e.g. "ID5_CON4_28.01.2020-16_37_39.csv") is composed as follows: `<Proband-ID>_<Condition-ID>_<DateTime-Stamp>`. For each wasp a new line like the following is appended to the CSV: `1;1;16;1;13:14:26.687;13:14:31.131` and can be read as `<Proband-ID>;<Condition-ID>;<Bee-ID>;<Result>;<Start-Time>;<End-Time>`. Start and End-Time time the appearing and disappearing of the wasp. Results have the following characteristics:
- 1: Player killed the wasp
- 2: Wasp stung the player

# Questionnaire Results
The answers from the questionnaire are stored in Google Docs.
The used questionnaire is can be found ([here] (https://docs.google.com/forms/d/1y2iOJb1yi_whFSzbAYHKQp34-JEaJwyAfJjkTT3wq4U/)). 

