I recommend reading this file on [GitHub](https://github.com/RoJoJoey/GhunterGhostHunter)
# Ghunter Ghost Hunter

Ghunter Ghost Hunter is a hide-and-seek style game made in the Unity engine created by Joey Einerhand and Bas Körver.

In Ghunter Ghost Hunter, a seeker (Ghunter) hunts down one or more hiders (Ghosts), who can hide in objects. The g

GGH uses Domoticz - typically used as Internet of Things software - to simulate back-end server-style information relay. The clients connect and send data to Domoticz using HTTPrequests.


## 1. Limitations

The game currently only supports one Ghunter and one Ghost. Domoticz is not the ideal server software for this type of game.
For more limitations, see the Issues tab at the Github repository.

## 2. Client installation

1. Download GhunterGhostHunterClient_vX.zip (Where X is the version number) from the releases page (GitHub) 
2. Unzip it somewhere on your desktop.
3. Launch ghunterGhostHunter.exe

## 3. Server installation

I do not recommend hosting your own server until the Domoticz username and password are no longer required, (or are encrypted) in order to log in to a server. Giving away your public IP and your domoticz username and password (which is required for clients to connect to your server) is a security risk. Anyone with these server details can log into your Domoticz application and potentially do harm to your server, your home network, and/or the device that's running the server.

1. Install [Domoticz](https://www.domoticz.com/)
2. Download the DomoticzServerBackup.db file
3. Launch Domoticz. 
4. A window should pop up asking for a username and password.
5. If you don't get the log in pop up but instead get an error, refresh the page or try again later.
6. Log in to Domoticz.  The default username is "DomoticzPublic" and the default password is "GhunterGhostHunter". You can edit this in your Domoticz settings. This is also the username and password which clients need to connect with your server.
7. Change your Domoticz default username and password. Not doing so is a huge security risk.
8. In Domoticz, select Setup, go to Settings, select Backup/Restore, and click "Restore Database"
9. Select the downloaded DomoticzServerBackup.db file and click "upload"
10. Wait until Domoticz finished installing the backup.
11. Port forward the Domoticz port. The default port is 8080. If you don't know how to port forward, look up your specific modem/router and add "How to port forward".
12. Share your public IPv4 address to whomever you want to have access to the server, together with your Domoticz username and password.

## 4. How to play
The gameplay in GGH is devided into two roles; Ghunter (The seeker) and Ghosts (The hiders)
The game is simple: Ghunter needs to catch all ghosts before the round timer reaches 0. The ghosts need to hide in an object and need to make sure not to get caught before the round timer reaches 0.

1. Launch the GGH Client (ghunterGhostHunter.exe).
2. Find a server to play on (Or host your own).
3. Click on "Play".
4. Enter the server details of the server you want to play on.
5. Select a role to play (Ghunter or ghost)

### 4.1. Ghosts
As a ghost, you need to hide from Ghunter and make sure not to get captured before the timer runs out. If you prevented yourself from getting captured once the timer reaches 0, you win!

1. Commit to the steps as written in "How To Play", but pick the "Ghost" role.
2. Wait until a player with the Ghunter role starts the round
3. You have 15 seconds to hide from Ghunter. If you have not hidden from him within this time, you lose the game.
4. If you found an object you would like to hide in, stand close enough to it (You know you'll be close enough when a message appears on the object informing you that you can hide in it) and press "E" to hide.
5. If you change your mind and want to change the object you want to hide in, press "E" again to unhide, go to a different object you would like to hide in, and repeat step 4.
4. Hope Ghunter doesn't catch you!

### 4.2. Ghunter
As Ghunter, you need to capture all the ghosts before the time runs out.

1. Commit to the steps as written in "How To Play", but pick the "Ghunter" role.
2. Wait for a Ghost to join the game.
3. Click the "Start Round" button to start the round.
4. Wait for 15 seconds while the ghosts get a chance to hide
5. Go close enough to an object you'd like to check for a ghost (You know you'll be close enough when a message appears on the object informing you that you check it), and press "E"
6. Repeat until the timer runs out or you've found all the ghosts


## 5. Contributing
This game's source code is available on [GitHub](https://github.com/RoJoJoey/GhunterGhostHunter).


Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## 6. License
[GNU General Public License v3.0](https://choosealicense.com/licenses/gpl-3.0/)
