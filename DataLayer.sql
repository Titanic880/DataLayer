/* For security reasons the login is created disabled and with a random password. */
USE [master]
GO
/****** Object:  Login [DataLayer_Admin]    Script Date: 2020-12-28 2:25:51 PM ******/
CREATE LOGIN [DataLayer_Admin] WITH PASSWORD=N'cWn4y1oisN40FqazsQmqoyvkcZ9ftRCq/BWxvixYmoc=', DEFAULT_DATABASE=[DataLayer], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [DataLayer_Admin] DISABLE
GO

create Database DataLayer;
go
use DataLayer;
go

Create Table TestCheck([id] int);
go

Create Table ScalarTest(
[id] int
);

/*
REMOVAL FUNCTIONS, DELETES ALL EXISTING DB DATA
use master;
go

Drop Table ScalarTest;
go
Drop Database DataLayer;
go
*/
