-- Tax free incomes
INSERT INTO age_threshold (Age, TaxFree) VALUES 
(65, 95750),
(75, 148217),
(-1, 165689); -- -1 indicates no limit to age

-- Tax Rates
INSERT INTO Brackets (Threshold, Minimum, Percent) VALUES
(237100, 0, 0.18),
(370500, 42678, 0.26),
(512800, 77362, 0.31),
(673000, 121475, 0.36),
(857900, 179147, 0.39),
(1817000, 251258, 0.41),
(-1, 644489, 0.45); -- -1 means no limit, so all income above 1817000 is taxed at 0.45s