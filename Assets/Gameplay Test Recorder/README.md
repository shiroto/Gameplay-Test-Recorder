Thank you for using 2GuyGames' Replay Assistant! This package allows you to record the input you do in a game and play it back later. This way you can confirm the behaviour of your game without having to play the same levels over and over again.

What is included in version 0.4.0:
	- Recording and Replay for:
		- Unity's Legacy Input (the UnityEngine.Input class)
		- Unity Input System (limited)
		- Rewired (https://assetstore.unity.com/packages/tools/utilities/rewired-21676)
		- uGUI
		- TextMeshPro
	- Integration into the Unity Test Runner

Upcoming features:
	- Support for the new Unity Input System in batchmode and UI


# HOW TO
The Replay feature is used via the Replay window you find at Window->2GuyGames->Replay.

## Enable Replay
When using Replay for the first time and when changing input related classes (e.g. adding new input calls) it is necessary for Replay to analyze your project. You can trigger the analysis via the "Analyze Project" button.

##Record
Press "Start Recording". All recordings are saved to unity assets. You can create a new asset via the "Create Record Asset" button. You can overwrite an existing recording by dragging it into the "Record to" field. Once an asset is selected, you can select a scene to start and the framerate to record in. Make sure your game holds the selected framerate consistently, otherwise the replay will fail. Press "Start Recording" to begin recording your input. The scene will start automatically, which will take longer than you expect, since Replay has to intialize the recording code. You can enable "Fast Load" as described below. When you are done recording your input, press "Stop Recording" or simply quit playmode.
 
## Replay
Press "Start Replaying". Drag the test asset you want to replay into the "Replay From" field. "On Replay End" lets you select what happens once the replay is finished. "KEEP_RUNNING" leaves the scene running and hands control back to you. "PAUSE" pauses the scene and hands control back to you. "EXIT_PLAY_MODE" exits playmode (who would have thought). In any case, a log will be generated to Assets/2GuyGames/Replay Logs.

## Inerpret Replay Results
When a replay finishes, you will see 2 outputs in your console. Firstly, "Logging to *some path*" which tells you where to find to logs of this replay. Secondly, "Replay result: *some number*" which shows you the replay result condensed in one number. This number tells you how much the observed result matches the expectation. 0 is a perfect fit; below 10 is considered expected variation; below 100 is considerable variation; above 100 is most likely an error.

## Fast Load
By default, Unity reloads the domain (i.e. all scripts, DLLs, etc.) when entering playmode. This means that GTR must reinitialize whenever a scene is started. Disabling domain reloading allows GTR to keep running even when starting a new scene.

## Integrate into Unity Test Runner
By pressing "Refresh Unity Test Runner" a new test assembly for replays will be created (if not present). This also adds test cases to the Unity Test Runner for each replay you have recorded. These can then be started via the Unity Test Runner. Replay results below 10 are considered as passing, values above as failing.

## Run Replays in Batchmode
To run replays in batchmode they must be integrated into the Unity Test Runner by clicking "Refresh Unity Test Runner" in the GTR window. Replays will then be executed alongside your other tests in batchmode. **BEWARE** Unity must compile in debug mode for replays to work properly. Add *-debugCodeOptimization* when running replays in batchmode.

# KNOWN ISSUES
1. When opening the asset picker in recording or replay, an ExitGUIException is thrown. This doesn't affect functionality.
2. Sometimes applying patches (when "enable fast loading" or starting a record/replay for the first time) fails. Restarting Unity mostly solves the issue, otherwise try to analyze the project again.
3. Unity Input System issues:
	- No batchmode support.
	- UI replays don't work consistently.
	- Replay reacts to user input.

If you notice any more issues, please contact us at *support@2guygames.com*.