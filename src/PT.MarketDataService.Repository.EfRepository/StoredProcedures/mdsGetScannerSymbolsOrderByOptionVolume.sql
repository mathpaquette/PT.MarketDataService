CREATE OR ALTER PROCEDURE mdsGetScannerSymbolsOrderByOptionVolume 
	  @CurrentTimestamp AS DATETIME
	, @MaxTimeOffset AS TIME
AS
BEGIN

SET NOCOUNT ON;

DECLARE @MaxTimeOffsetTimestamp AS DATETIME = @CurrentTimestamp - CAST(@MaxTimeOffset AS DATETIME)

SELECT sr.Symbol
	,l1.CallVolume + l1.PutVolume AS OptionVolume
FROM (
	SELECT *
		,ROW_NUMBER() OVER (
			PARTITION BY ParameterId ORDER BY Timestamp DESC
			) AS Rn
	FROM scanners
	WHERE Timestamp <= @CurrentTimestamp
		AND Timestamp >= @MaxTimeOffsetTimestamp
	) s
INNER JOIN ScannerRows sr ON sr.ScannerId = s.Id
OUTER APPLY (
	SELECT TOP 1 *
	FROM Level1MarketDatas
	WHERE Level1MarketDatas.Symbol = sr.Symbol
		AND Level1MarketDatas.Timestamp <= s.Timestamp
		AND Level1MarketDatas.Timestamp >= @MaxTimeOffsetTimestamp
	ORDER BY Level1MarketDatas.Timestamp DESC
	) AS l1
WHERE Rn = 1
ORDER BY OptionVolume DESC

END