-- Tax free incomes
INSERT INTO age_threshold (Age, TaxFree) VALUES 
(1, 95750),
(65, 148217),
(75, 165689);

-- Tax Rates
INSERT INTO Brackets (Threshold, TaxRate) VALUES
(1, 0.18),
(237100, 0.26),
(370500, 0.31),
(512800, 0.36),
(673000, 0.39),
(857900, 0.41),
(1817000, 0.45);	

INSERT INTO Deductions (DeductionDescription, DeductionRateMax, DeductionAmountMax, DeductionRate) VALUES
('Donations', 0.10, -1, 1),
('Retirement fund', 0.275, 350000, 1)