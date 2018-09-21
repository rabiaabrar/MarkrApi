-------------- Markr - marking as a service --------------

Approach:
I have used ASP.net Web Api 2 for the api, SQL Server for data storage and Entity Framework for Data Access. 
For unit testing I have used Visual Studio Unit Testing Framework.

Tools required:
You need Visual Studio 2017 and Sql Server 2017 to run this solution.

Steps to build & run:
-> Open "Create DB MarkrAPI.sql" in SQL Server Management studio and run it to create the database.
-> Open "MarkrApi.sln" in Visual Studio. 
-> If SQL Server is not on localhost, then update connection string named "MarkrApiEntities" in following files to point to correct SQL Server:
	1) Web.Config in MarkrApi
	2) App.Config in MarkrApiDataAccess
	3) App.Config in MarkrApi.Tests
-> Press ctrl+F5 to build and run. By default it will open endpoint http://localhost:51848/results/9863/aggregate in browser.
-> The endpoint to POST Test Results XML is http://localhost:51848/import.