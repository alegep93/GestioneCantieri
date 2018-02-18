ALTER TABLE tblClienti ALTER COLUMN tel1 nvarchar(50);
ALTER TABLE tblClienti ALTER COLUMN cell1 nvarchar(50);

ALTER TABLE TblCantieri ADD UNIQUE (CodCant);
ALTER TABLE TblCantieri ALTER COLUMN Numero int NOT NULL;