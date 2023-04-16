CREATE TABLE age_threshold (
    AgeID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Age INT NOT NULL, -- Maximum age the value applies to
    TaxFree FLOAT NOT NULL -- Amount of tax free income for age group
);

CREATE TABLE Brackets (
    BracketID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Threshold FLOAT NOT NULL, -- Max amount of income at the tax rate
    Percent FLOAT NOT NULL -- Tax rate as decimal (i.e. 0.10 for 10%)
);

CREATE TABLE Deduction (
    DeductionID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    DeductionDescription VARCHAR(255) NOT NULL, -- Type of deduction, i.e. Medical Aid
    DeductionRate FLOAT NOT NULL, -- Rate of deduction as decimal
    DeductionMax FLOAT NOT NULL -- Maximum monetary deduction
);