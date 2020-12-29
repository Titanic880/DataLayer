NOTE: All of the Database interaction passes through the SQL Sub Folder, therefore for it to work properly it
needs access to a database, Make sure to implement the <Entry.GetConnection()> for accessing the connection check boolean

ErrorCheck_UnitTesting - Unit Testing for the Entirity of the ERRORCHECK sub Folder

Main_UnitTesting - Unit Testing for the main Features and Front end interfacing

SQL_UnitTesting - Unit testing for the Primary Sql Interface
NOTE: Is not actually an interface class,
but instead a datalayer that should be the only way for things within this solution to access a database,
if cases of db connection are outside of class and not documented as intentional,
then it is recommended to mention it as an issue on the github repository.