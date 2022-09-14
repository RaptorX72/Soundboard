# Soundboard

This project allows multiple devices to play different audio files via network timing on different audio channels.
Both the server and client application is contained within the project.

## Client applicaiton usage:

1.  Start the application `Soundboard.exe`
2.  From the Primary output list, select the device you want the application to stream the audio to
3.  (Optional) Enable and select which audio device to serve as a playback
4.  Enter the IP address and port where the server can be found
5.  Enter the username that will identify your client on the server
6.  Select the folder where the appropriate sound files are located
7.  Connect to the server

### Play single file is currently under development, unstable behaviour if used.

## Server application usage

The audiofiles to play is loaded from an external XML file, currently the name is hardcoded to `template.xml`.

The server will ask what port to use, default is set to 100.

Use the command `exit` to stop the server and shut it down.

Use the command `help` for additional information.

Commands:

- `exit` : Disconnects clients, shuts down the server and exists applicaiton.
- `help` : Displays all available commands
- `clear` : Clears the console window
- `listc` : Lists all connected clients
- `reloadq` : Reloads the queue file
- `listq` : Lists all the queues (ID, name)
- `playq ID` : Plays the specified queue
- `stopq` : Stops the current queue
