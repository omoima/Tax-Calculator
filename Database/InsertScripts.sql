-- Tax free incomes
INSERT INTO age_threshold (Age, TaxFree) VALUES 
(65, 95750),
(75, 148217),
(-1, 165689); -- -1 indicates no limit to age

-- Tax Rates
INSERT INTO Brackets (Threshold, Percent) VALUES
(237100, 0.18),
(370500, 0.26),
(512800, 0.31),
(673000, 0.36),
(857900, 0.39),
(1817000, 0.41),
(-1, 0.45); -- -1 means no limit, so all income above 1817000 is taxed at 0.45s