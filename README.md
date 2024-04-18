**My Game, a puzzle game to rival Candy Crush**

As a side project for the last couple years, I've been making my own game. It is a puzzle game for mobile, PC, VR, and console. I am doing all of the design, art, programming, etc. for the project. I'm not quite ready to show what it is, I'll be revealing it later this year.

Sample #1 - Find Islands
FindIslands.cs

This code is part of my AI solver code that internally plays my game to verify that it is solvable by the player.

Sample #2 - Event Manager
EventManager.cs
DataEvent.cs

The EventManager is used to send messages between systems. One part of the code can send a message through the EventManager, and another can listen for that kind of message without knowing anything about the sender. This makes it so one kind of message can come from multiple sources without the destinations needing to know about each source. The DataEvent is one kind of message that is sent.

Sample #3 - Object Pool
ObjectPool.cs
GameObjectPool.cs

There are many kinds of objects in my game that are generated and then later not needed, then needed again. I wanted to have an object pool help me create these and manage them when they are not currently needed, to avoid the overhead of frequently creating and destroying these objects. So I asked Chat GPT to write an object pool class for me. I asked for the class a few times, being more specific with some different features that I wanted it to have, and then brought the final result into my project. Over time I have re-written about half of the code that was generated and added some more functionality into it. I find that AI generated code is a good starting point to build off of. I wrote the GameObjectPool to extend the ObjectPool class so it could be used with GameObjects.

Sample #4 - Design Pattern for Application Code Structure
(No code for this one, but a design pattern plan described here.)

Aside from the code samples, something that I'm really happy with in the project is the design pattern that I developed for the application structure. Unity does not have a mandated application structure, it is very open to however you want to structure the app's code. This is great for flexibility, but unfortunately, it can easily lead to code that is tightly coupled (hard to update), and inefficient. So over the course of the last few projects I have been developing this code structure to use in Unity to keep the code clean and decoupled.

The basic idea of the design pattern is to keep things separate. The application code is kept separate from the features code, and each feature's code is separate from other features. Here are some more details:
· A bridge class ties a feature to the application code. It acts as the middleman between the two so that they don't have to know about each other.
· The bridges don't interact with other bridges. This keeps each feature separate.
· The bridges can have a view to display information to the user.
· The views can get input from the user, which is passed to the bridge, which tells the application what to do.

Here is an example. The application sends a message out that it started. The DataManagerBridge gets this message and tells the DataManager to load the saved data. It loads the data and gives it to the bridge, which in turn gives it to the application. The application sends out a message that the data is loaded. The game bridge gets this message and accesses the data of the last saved game to load it for display. Once I am ready to port this to consoles, swapping out the code in the data manager to load the saved data from the console's memory will be simple, and no other part of the process will need to change.

Recently I learned about a book that Unity published that gives suggestions on code structure and they propose an application structure similar to the one that I came to. They call their structure Model View Presenter (MVP), and you can read about it [in this book](https://unity.com/resources/level-up-your-code-with-game-programming-patterns) on page 88.
<br>
<br>
<br>
**WinReality**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/54b04a38-ce6f-4860-a9ea-a31edede4425" width="217" height="113" />
<img src="https://github.com/elliselkins/CodeSample/assets/433933/696b10a0-4b96-4bd0-8653-e76ea661c07c" width="188" height="126" />
<img src="https://github.com/elliselkins/CodeSample/assets/433933/543aeceb-119a-4b0b-9730-15d2cd556ef8" width="217" height="113" />

WinReailty is a baseball training app in VR ([link](https://www.meta.com/experiences/3172399986210688/)). I was a Technical Designer there. I helped with some of the programming and design for a lot of different parts of the app from UI to field physics. Along with the training parts of the app we wanted an exciting experience for the new users to have right when they got into the app. So I helped to design this onboarding experience.

After the plan was made I made a prototype of the experience. For the prototype I added a moment at the beginning where the user is starts out in darkness and they get a bat and floating ball. Once the ball is hit the field appears. I added a coach voice over that welcomes the user into the experience, teaches them what they need to do, but most importantly gives them relevant feedback based on the user's swing as to how to improve.

After the prototype I was the creative director of this effort and gave tasks to artists and developers to get it made in the app.
<br>
<br>
<br>
**Vingo**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/60734720-6940-4bd6-b949-38809ed8d280" width="316" height="249" />

At the company Virtual Training World we made the exercise game Vingo for iOS. Connect to exercise equipment and see your avatar move as you exercise.

I programmed the connection to the Ant+ devices (similar to Bluetooth, and later helped with the Bluetooth connection). I programmed the bikes and runners to follow the waypoints on the roads. I was also a technical lead on the project.
<br>
<br>
<br>
**GlobalSim Trainer for VR and Simulation Pod**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/2f15bef5-b8d7-42e7-aac6-86a490f58df0" width="316" height="179" />

GlobalSim makes simulators for heavy machinery training, from cranes to front loaders to RORO (roll on roll off car shipments). The project is in VR and in simulation pods with motion bases to move the user while operating.

For VR I programmed the teleporting system. I made it so the user could target a spot to teleport to and adjust their look angle 360° to anything they wanted. Another cool feature was that in some of the environments, there were catwalks (high walkways in warehouses or on large cranes). The user could teleport to these catwalks to look down on the scene. The user could also teleport into different machines to change which machine they were operating.

Across the app, I helped to program the machines to make them operate as they do in real life. The project is networked to work on multiple computers for multi-monitor display, and so that many students can be connected to one teacher's machine. I added an avatar for VR users so that they could be seen by other users. The head movement and gaze direction of the avatar came from the VR user's headset. The VR user's camera could also be synced across the network to be seen on different monitors.

One of the largest tasks I had was to program self-driving vehicles (sedan, SUV, and semi-truck) that could be used for various training purposes. Since we wanted the simulation to be very interactable the vehicles were simulated to have an engine, ignition, pedals, a steering wheel, turn signals, etc. The AI driver that I programmed drives the car by controlling the ignition, steering wheel, and pedals and it uses trigger colliders to watch the road in front of the vehicle. Waypoints were given to the car to drive to with speed limits. Each waypoint could have additional instructions such as drive through, stop, or wait for a container to be loaded onto the back of the truck's trailer. The vehicles stopped for pedestrians or other vehicles in front of them and then continued on their way once the path was clear.

I was the technical lead on some of the projects at [GlobalSim](https://www.globalsim.com/).
<br>
<br>
<br>
**Fairly Certain Doom VR**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/445726fe-5906-4ad5-99fa-dfc4d14f144b" width="317" height="178" />

As a contractor, I was brought onto this project when it was about 85% complete to finish programming it, fix bugs, and publish it onto the Steam game store. The company was Cosmic Pictures, a film studio, and since VR was very new at the time they wanted to try making something in VR.

Fairly Certain Doom is a short interactive narrative experience. It was meant to be a whimsical heart-pounding experience that included many brushes with death. It can be seen on Steam [here](https://store.steampowered.com/app/790170/Fairly_Certain_Doom/).
<br>
<br>
<br>
**Home Customization VR Prototype**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/3fcbd097-0a99-49d4-b0ae-c023cdfa09eb" width="317" height="210" />

Another project I worked on as a contractor was a VR app for users to be able to walk around inside a home that they could have built for them. I worked directly with a client to make this app, so I was the solo dev and the project manager. The user was able to teleport anywhere inside or around the house to get a look. They could toggle on and off furniture in different rooms to see how it looked. The best part was the feature that allowed the user to select walls, floors, countertops, and more and change the materials to see what different options would look like. I don't have any footage of this project, but this was a mock up version of the project and it looked similar to this.
<br>
<br>
<br>
**Alf Engen's Take Flight**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/83e84a5f-507a-4739-8828-b227550b7c58" width="260" height="146" />
<img src="https://github.com/elliselkins/CodeSample/assets/433933/9f9bd4a2-b655-4041-ac66-a96a49ce2821" width="260" height="146" />

In 2014 at the company Unrivaled, I started working with Unity. My favorite project that I worked on was Alf Engen's Take Flight, a ski jump simulation game. I was the only full-time dev on the project, with some code from the project manager.

To participate the user stands on a platform with an Arduino underneath to read the angle the player is leaning. The game is projected onto the wall in front of the player. There is a real fan that increases in speed to blow wind in the player's face as they go down the ramp. An SLR camera is connected and takes the player's picture while they're playing, which is then posted to Facebook with their score. You can see the project in action in [this video](https://vimeo.com/110925626).
<br>
<br>
<br>
**Studio J**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/8e8cde78-6431-40da-b8e1-ac589b9d45fa" width="312" height="140" />

At Rain Interactive I worked on an online scrapbooking application called Studio J for the company Close To My Heart, which made physical scrapbooking materials. This application used the digital versions of their physical papers, stickers, stamps, etc. They had many different physical sticker fonts that you could use on your paper projects. One of my larger tasks on the project was creating a tool to be able to type with these sticker fonts. I created two tools for this. One for the user to be able to use to type with, and the other for someone to be able to set all of the properties to create the sticker font. More details in [this video](https://vimeo.com/81075538).
<br>
<br>
<br>
**Playground Sessions**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/b504d39b-1e07-442e-87eb-7fe1171f5985" width="309" height="206" />

Another project at Rain Interactive was Playground Sessions ([link](https://www.playgroundsessions.com/)). I programmed many things for the app, such as rendering triplets on the musical staff. One of the bigger things I did was to rewrite the video player's mark syncing system. You could set a mark at a timestamp in the video and it would send out an event when that mark was reached. Its accuracy was within 200 milliseconds, but that was noticeable off when trying to sync the sheet music to the video. I got it down to within 40 milliseconds, and most of the time it was within 20.
<br>
<br>
<br>
**Stats Teaching with M&Ms**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/84aa37ce-fe3a-4a0b-a919-7f18d7519432" width="308" height="221" />

My career really started at my favorite college job, at the Center for Instructional Design. Any professor on campus could come there and request something to be made for them, like a video, website, or application. I started out as a video editor, but since I had been taking programming classes I quickly moved into making applications.

My favorite project that I worked on here was for a stats professor who had been teaching stats principles with M&Ms. One co-worker and I recreated their lessons in a Flash app where the user could manipulate the M&Ms to learn these principles. I was the programmer for the project.
<br>
<br>
<br>
**Portfolio Website**

<img src="https://github.com/elliselkins/CodeSample/assets/433933/47a22ae6-e81e-4b2d-9253-93277d1b2cf0" width="309" height="225" />

You can see more details about these projects and others on my portfolio website: [elliselkins.com](http://elliselkins.com/)
