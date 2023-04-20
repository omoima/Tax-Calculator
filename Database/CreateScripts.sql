USE master;
GO

DROP DATABASE IF EXISTS Tax;
GO

CREATE DATABASE Tax;
GO

USE Tax;
GO

CREATE TABLE age_threshold (
    AgeID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Age INT NOT NULL, -- Maximum age the value applies to
    TaxFree DECIMAL(38, 5) NOT NULL -- Amount of tax free income for age group
);
GO

CREATE TABLE Brackets (
    BracketID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Threshold DECIMAL(38, 5) NOT NULL, -- Max amount of income at the tax rate
    TaxRate DECIMAL(10, 5) NOT NULL -- Tax rate as decimal (i.e. 0.10 for 10%)
);
GO

CREATE TABLE Deduction (
    DeductionID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    DeductionDescription VARCHAR(255) NOT NULL, -- Type of deduction, i.e. Medical Aid
    DeductionRateMax DECIMAL(10, 5) NOT NULL, -- Rate of deduction as decimal
    DeductionAmountMax DECIMAL(38, 5) NOT NULL, -- Maximum monetary deduction
    DeductionRate DECIMAL(10, 5) NOT NULL -- Amount of lump sum applicable for deduction
);
GO