# Booklot Library Management System

<p align="center">
  <a href="https://github.com/PhilipU13/Library-Management-System">
    <img src="LibraryManagementSystem/Resources/final-logo.png" width="250" alt="Booklot Library logo">
  </a>
</p>

## Links:

Documentation: [https://docs.google.com/document](https://docs.google.com/document/d/1Ev4pP_uHvwmtyXkHVHUa9ffxdd-uWtnLswkc9_eqtr0/edit?usp=sharing)

Video demonstration: [https://docs.google.com/video-demonstration](https://drive.google.com/file/d/1Kr3BkNZ8A4V00AIdteiOqy9-G1xdnduZ/view?usp=sharing)

sql-source-control repository: https://github.com/madchavez/sql-source-control

Source Control plugin download: https://www.devart.com/dbforge/sql/source-control/download.html

## Instructions to make this project work: 

* The SQL Database must be cloned from the sql-source-control repository (check in links).
  * Clone the repository to a local folder using cmd
    * Command: </br> <kbd>git -clone https://github.com/madchavez/sql-source-control.git</kbd>
  * Download the Source Control plugin for SSMS (check in links).
  * Open SSMS and create a database and name it ***"LibDat"***
  * Link your local database to the repo database.
    * Right-click on the database and under **"Source Control"** click on **"Link Database to Source Control"**
    * Look for **"Source control repository"** and click on the plus sign next to the drop-down box.
    * Choose Git as the Source control system and under the **"Repository folder"** navigate to your local folder where you cloned the remote repository. 
    * Test connection and close the **"Source control repository"** window.
    * Click on **"Dedicated"** for the database development model then click **"Link"**
    * Click on the checkbox next to Remote changes and then click the **"Get Latest"** button.
  
* The value of globalServer in Program.cs must be modified into the name of your local server.
  * <kbd>public static string globalServer = ***"MAJO-PC"***;</kbd>
  * <kbd>public static string globalServer = ***"DESKTOP-9MBNT14"***;</kbd>

## Creators:

Section: **BCS22**
  * Chavez, Miguel Antonio
  * Dasal, Estelle Marie
  * Gamit, Mary Josephine
  * Uriarte, Philip Ronin

## Other notes:

* Application created with Visual Studio 2022
* Database created with SQL Server Management Studio
 
