The Gameplay Test Recorder lets you record your gameplay and create a test from the recording.<br>
<br>
How to use:
1. Open a scene you want to record (i.e. Gameplay Test Recorder\Samples\Input Manager)
2. Open Window->2GuyGames->Gameplay Test Recorder
3. Make sure the appropriate input system is selected ("Legacy Input" for scenes in Samples\Input Manager)
4. Press "Analyze Project"
5. Press "Record"
6. Enter a name and press "Create"
7. Press "Start Recording" and wait (compilation takes longer when recording)
8. Play the game and end play mode or press "Stop Recording"
9. Go back and press "Refresh Unity Test Runner"
10. Open Window->General->Test Runner
11. Execute the test you just recorded

Tests will fail if the recorded final game state differs from the test's final game state.
