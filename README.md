# Links:

To-Do list: https://github.com/PhilipU13/Library-Management-System/projects/1?fullscreen=true <br />

# Notes To make this project work: 

* An SQL database must be created locally <br />
  * Create a database named libraryData 
  * Create a table named booksData with the column names and data type in order: BookID int, Title varchar(50), Author varchar(50), Edition varchar(50), Publication varchar(50), Status varchar(50) <br />
  * Create a table named loginData with the column names and data type in order: username varchar(50), password varchar(50) <br />
  
* The code must be modified <br />
  * On all lines that are <kbd>con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");</kbd> change the value of "Data Source" <kbd>Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;</kbd> to the name of your local server <br />
  * Example: From <kbd>Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;</kbd> to <kbd>Data Source=MAJO-PC\\SQLEXPRESS;</kbd> <br />
  
# Other notes:

  * The steps above won't be needed if a remote repository could be created <br />
  * Not yet sure if publication date will be included to the columns in booksData and what data type it would be since it's a date <br />
