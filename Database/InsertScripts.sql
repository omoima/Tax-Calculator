-- Tax free incomes
INSERT INTO age_threshold (Age, TaxFree) VALUES 
(65, 95750),
(75, 148217),
(2147483647, 165689); -- INT MAX

-- Tax Rates
INSERT INTO Brackets (Threshold, TaxRate) VALUES
(237100, 0.18),
(370500, 0.26),
(512800, 0.31),
(673000, 0.36),
(857900, 0.39),
(1817000, 0.41),
(99999999999999999999999999999999, 0.45); -- 10^(38-5) - 1

INSERT INTO Deductions (DeductionDescription, DeductionRateMax, DeductionAmountMax, DeductionRate) VALUES
('Donations', 0.10, -1, 1),
('Retirement fund', 0.275, 350000, 1)