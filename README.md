# Links:

To-Do list: [https://github.com/PhilipU13/Library-Management-System](https://github.com/PhilipU13/Library-Management-System/projects/1?fullscreen=true)

Documentation: [https://docs.google.com/document](https://docs.google.com/document/d/1Ev4pP_uHvwmtyXkHVHUa9ffxdd-uWtnLswkc9_eqtr0/edit?usp=sharing)

Video Demonstration: TBD

# Notes To make this project work: 

* An SQL database must be created locally using SSMS
  * Create a **DATABASE** named *libraryData* 
    * Code: <kbd>CREATE DATABASE libraryData;</kbd>
  * Create a **TABLE** named *booksData* with the following column names and data type in order:
    * BookID INT
    * Title VARCHAR(50)
    * Author VARCHAR(50)
    * Genre VARCHAR(50)
    * Edition VARCHAR(50)
    * Publication VARCHAR(50)
    * Status VARCHAR(50)
    * DateBorrowed DATE
    * DateToReturn DATE </br>
    * Code: <kbd>CREATE TABLE booksData (BookID INT, Title VARCHAR(50), Author VARCHAR(50), Edition VARCHAR(50), Publication VARCHAR(50), Status VARCHAR(50), DateBorrowed DATE, DateToReturn DATE);</kbd>
  * Create a **TABLE** named *loginData* with the following column names and data type in order:
    * username VARCHAR(50)
    * password VARCHAR(50)
    * Code: <kbd>CREATE TABLE booksData (username VARCHAR(50), password VARCHAR(50));</kbd>
  
* The value of globalServer in Program.cs must be modified into the name of your local server.
  * <kbd>public static string globalServer = ***"MAJO-PC"***;</kbd>
  * <kbd>public static string globalServer = ***"DESKTOP-9MBNT14"***;</kbd>
 
# Other notes:

  * The steps above won't be needed if a remote repository could be created
  
