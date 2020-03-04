# Predicting Avatar Movement in Virtual Reality using Neural Networks
![Starting Scene](https://github.com/Cele3x/research-seminar/blob/master/submission/Images/teaser.png)
This repository was used for the project "Predicting Avatar Movement in Virtual Reality using Neural Networks" carried out as part of the course "Forschungsseminar" in the winter semester 2019/2020 at the University of Regensburg. 
Furthermore, this repository will be used to submit the final product. 

For the actual submission only the folder __submission__ , found in the root directory of this repository, is relevant. The folder contains several subfolders, sorted alphabetically: 
- [Docs](https://github.com/Cele3x/research-seminar/tree/master/submission/Docs)
- [Images](https://github.com/Cele3x/research-seminar/tree/master/submission/Images)
- [Interceptor Client](https://github.com/Cele3x/research-seminar/tree/master/submission/Interceptor%20Client)
- [Neural Network](https://github.com/Cele3x/research-seminar/tree/master/submission/Neural%20Network)
- [Study](https://github.com/Cele3x/research-seminar/tree/master/submission/Study)
- [Video](https://github.com/Cele3x/research-seminar/tree/master/submission/Video)

Each subfolder will be described in the following, starting with __Docs__: 

## Docs 
This folder contains the final version of the scientific paper written during the project. 

## Images
This folder contains all needed image for this repository, as well as all images needed for the paper.

## Interceptor Client
This folder is used for the developed Intercepter Client and all its dependencies, its containment is structured as follows: 
- GuiClient: Graphical user interface for intercepting stream data and changing prediction models.
- CSVClient: Command line tool for reading and writing motive stream data to a CSV file. (First Draft)

## Neural Network
This folder contains everything related to the used neural network, it is structured as shown in the following: 
- Data-Handling: Contains all scripts needed to handle the data gathered in the data acquisiton study. 
- Evaluation: Contains all scripts needed for evaluating the presented neural network. 
- Models: Contains all trained models, even those not evaluated in the evaluation study. 
- Training: Contains all scripts needed for training the presented neural networks. 

Does NOT contain the virtual enviroment used. Please make sure you have python 3.7.X and the needed packages installed. 

## Study
This folder contains all data gathered during the study conduct. It does not contain or discuss any results or findings, please see the submitted paper for findings, results and a conclusion.
- Demographics: Contains a list of the demographics of all study participants, as well as some information about whether they were wearing glasses or not. Additionally it contains information about previous experience in VR and sportiness of the participants.
- Game Perfomance Data: Contains the measurements of the game performance for each participant and condition. 
- Questionnaire Data: Contains the gathered results of the asked questionnaire. 
- Game: Contains the Unity 3D game used to evaluate the prediction system and to collect perfomance data. Instructions on how to use the application can be found here: [README](https://github.com/Cele3x/research-seminar/blob/master/submission/Study/game/README.md)

## Video
The video stored in this folder visualizes scenes from an exemplary study conduct

