-- Backfills UserProfiles.Country from the area code in PhoneNumber.
-- Idempotent: only touches rows where Country is still unset, safe to re-run.

CREATE TABLE #AreaCodeCountry (
    AreaCode CHAR(3)       NOT NULL PRIMARY KEY,
    Country  NVARCHAR(100) NOT NULL
);

-- In a real system this would come from BULK INSERT off a reference CSV
-- rather than a handful of rows typed inline.
INSERT INTO #AreaCodeCountry (AreaCode, Country)
VALUES
    ('781', 'United States'),
    ('416', 'Canada'),
    ('020', 'United Kingdom');

-- Backfill in batches so no single statement holds a table-wide lock
-- or grows the transaction log for the whole run.
DECLARE @BatchSize   INT = 5000;
DECLARE @RowsUpdated INT = 1;

WHILE @RowsUpdated > 0
BEGIN
    UPDATE TOP (@BatchSize) up
    SET up.Country = ac.Country
    FROM dbo.UserProfiles up
    INNER JOIN #AreaCodeCountry ac
        ON ac.AreaCode = LEFT(up.PhoneNumber, 3)
    WHERE up.Country IS NULL;

    SET @RowsUpdated = @@ROWCOUNT;

    WAITFOR DELAY '00:00:00.250';
END

DROP TABLE #AreaCodeCountry;
