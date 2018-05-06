CREATE OR ALTER PROCEDURE mdsGetScannerSymbolsOrderByOptionVolume 
	  @CurrentTimestamp AS DATETIME
	, @MaxTimeOffset AS TIME
AS
BEGIN

SET NOCOUNT ON;

SELECT s.Timestamp
	,l1.Symbol
	,l1.CallVolume + l1.PutVolume AS OptionVolume
FROM (
	SELECT *
		,ROW_NUMBER() OVER (
			PARTITION BY ParameterId ORDER BY Timestamp DESC
			) AS Rn
	FROM scanners
	WHERE Timestamp <= @CurrentTimestamp
		AND Timestamp >= @CurrentTimestamp - CAST(@MaxTimeOffset AS DATETIME)
	) s
INNER JOIN ScannerRows sr ON sr.ScannerId = s.Id
CROSS APPLY (
	SELECT TOP 1 *
	FROM Level1MarketDatas
	WHERE Symbol = sr.Symbol
		AND Timestamp <= s.Timestamp
	ORDER BY Timestamp DESC
	) l1
WHERE Rn = 1
ORDER BY OptionVolume DESC

END