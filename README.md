# Links:

To-Do list: [https://github.com/PhilipU13/Library-Management-System](https://github.com/PhilipU13/Library-Management-System/projects/1?fullscreen=true)

Documentation: [https://docs.google.com/document](https://docs.google.com/document/d/1Ev4pP_uHvwmtyXkHVHUa9ffxdd-uWtnLswkc9_eqtr0/edit?usp=sharing)

Video Demonstration: TBD

# Notes To make this project work: 

* An SQL database must be created locally using SSMS
  * Create a **DATABASE** named *libraryData* 
  * Create a **TABLE** named *booksData* with the column names and data type in order: BookID int, Title varchar(50), Author varchar(50), Genre varchar(50), Edition varchar(50), Publication varchar(50), Status varchar(50)
  * Create a **TABLE** named *loginData* with the column names and data type in order: username varchar(50), password varchar(50)
  
* The code on the Forms must be modified
  * On all lines that are <kbd>con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");</kbd> change the value of "Data Source" <kbd>Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;</kbd> to the name of your local server
  * Example: From <kbd>Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;</kbd> to <kbd>Data Source=MAJO-PC\\SQLEXPRESS;</kbd>
  
# Other notes:

  * The steps above won't be needed if a remote repository could be created
  
