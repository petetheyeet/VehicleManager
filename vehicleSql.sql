--create our new database

CREATE DATABASE VehicleDb

--create management schema

USE VehicleDb
CREATE SCHEMA Management

--create application user

CREATE LOGIN joshua WITH PASSWORD = '', DEFAULT_DATABASE = VehicleDb;
CREATE USER joshua FROM LOGIN joshua;
USE VehicleDb;
GRANT EXECUTE to joshua;
GRANT SELECT to joshua;
GRANT ALTER to joshua;
GRANT INSERT to joshua;
GRANT UPDATE to joshua;
GRANT DELETE to joshua;

--create Vehicles table

USE VehicleDb
CREATE TABLE VehicleDb.Management.Vehicles (
    Vehicle_ID INT IDENTITY(1,1) PRIMARY KEY ,
    Owner_FName VARCHAR(255),
    Owner_LName VARCHAR(255),
    Owner_Phone VARCHAR(50),
    Owner_Unit VARCHAR(20),
    Owner_Apartment VARCHAR (20),
    Vehicle_Make VARCHAR(255),
    Vehicle_Model VARCHAR(255),
    Vehicle_Color VARCHAR(255),
    Vehicle_Registration VARCHAR(255),
    Registration_Date DATETIME,
    Audit_User VARCHAR(50),
    Audit_Time DATETIME
)

--create stored procedure for write vehicle

CREATE PROC Write_New_Vehicle
    (@Owner_FName VARCHAR(255), @Owner_LName VARCHAR(255), @Owner_Phone VARCHAR(255),
    @Owner_Unit VARCHAR(20), @Owner_Apartment VARCHAR(20), @Vehicle_Make VARCHAR(255),
    @Vehicle_Model VARCHAR(255), @Vehicle_Color VARCHAR(255), @Vehicle_Registration VARCHAR(255), @Registration_Date DATETIME,
    @Audit_User VARCHAR(50), @Audit_Time DATETIME)
AS
BEGIN
    INSERT INTO Management.Vehicles
    (Owner_FName, Owner_LName, Owner_Phone, Owner_Unit, Owner_Apartment, Vehicle_Make,
     Vehicle_Model, Vehicle_Color, Vehicle_Registration, Registration_Date, Audit_User, Audit_Time)
    VALUES (@Owner_FName, @Owner_LName, @Owner_Phone, @Owner_Unit,
            @Owner_Apartment, @Vehicle_Make, @Vehicle_Model, @Vehicle_Color,
            @Vehicle_Registration, @Registration_Date,
            @Audit_User, @Audit_Time)
END

--alter vehicle

CREATE PROC Alter_Vehicle
    (@Vehicle_ID INT, @Owner_FName VARCHAR(255), @Owner_LName VARCHAR(255), @Owner_Phone VARCHAR(255),
    @Owner_Unit VARCHAR(20), @Owner_Apartment VARCHAR(20), @Vehicle_Make VARCHAR(255),
    @Vehicle_Model VARCHAR(255), @Vehicle_Color VARCHAR(255), @Vehicle_Registration VARCHAR(255),
    @Audit_User VARCHAR(50), @Audit_Time DATETIME)
AS
BEGIN
    UPDATE Management.Vehicles
    SET Owner_FName = @Owner_FName, Owner_Lname = @Owner_LName, Owner_Phone = @Owner_Phone,
        Owner_Unit = @Owner_Unit, Owner_Apartment = @Owner_Apartment, Vehicle_Make = @Vehicle_Make,
        Vehicle_Model = @Vehicle_Model, Vehicle_Color = @Vehicle_Color, Vehicle_Registration = @Vehicle_Registration,
        Audit_User = @Audit_User, Audit_Time = @Audit_Time
    WHERE Vehicle_ID = @Vehicle_ID
END

--create stored procedure for get all vehicles

CREATE PROC Get_All_Vehicles
AS
BEGIN
    SELECT Vehicle_ID, Owner_FName, Owner_LName, Owner_Phone, Owner_Unit, Owner_Apartment,
           Vehicle_Make, Vehicle_Model, Vehicle_Color, Vehicle_Registration, Registration_Date
    FROM Management.Vehicles
END

--create stored procedure for searching on reg

CREATE PROC Search_By_Reg (@Reg_Query VARCHAR(255))
AS
BEGIN
    SELECT Vehicle_ID, Owner_FName, Owner_LName, Owner_Phone, Owner_Unit, Owner_Apartment,
           Vehicle_Make, Vehicle_Model, Vehicle_Color, Vehicle_Registration, Registration_Date
    FROM Management.Vehicles
    WHERE Vehicle_Registration like  '%' + @Reg_Query + '%'
END

--create stored procedure for get vehicle by pk

CREATE PROC Get_Vehicle (@Vehicle_ID INT)
AS
BEGIN
    SELECT Vehicle_ID, Owner_FName, Owner_LName, Owner_Phone, Owner_Unit, Owner_Apartment,
           Vehicle_Make, Vehicle_Model, Vehicle_Color, Vehicle_Registration, Registration_Date
    FROM Management.Vehicles
    WHERE Vehicle_ID = @Vehicle_ID
END

--delete vehicle

CREATE PROC Delete_Vehicle (@Vehicle_ID INT)
AS
BEGIN
    DELETE FROM Management.Vehicles WHERE Vehicle_ID = @Vehicle_ID
END