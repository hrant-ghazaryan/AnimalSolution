USE [AnimalTranslationDB];
GO

DECLARE @json nvarchar(max);

SELECT @json = BulkColumn
FROM OPENROWSET(
    BULK N'D:\Programs\AnimalSolution\TranslationApi\Data\translations-seed.json',
    SINGLE_CLOB,
    CODEPAGE = '65001'
) AS SourceFile;

INSERT INTO dbo.Translations
(
    EnumType,
    EnumValue,
    LanguageCode,
    TranslatedText
)
SELECT
    Source.EnumType,
    Source.EnumValue,
    Source.LanguageCode,
    Source.TranslatedText
FROM OPENJSON(@json)
WITH
(
    EnumType       nvarchar(100)  '$.EnumType',
    EnumValue      nvarchar(100)  '$.EnumValue',
    LanguageCode   nvarchar(10)   '$.LanguageCode',
    TranslatedText nvarchar(1000) '$.TranslatedText'
) AS Source
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.Translations AS Existing
    WHERE Existing.EnumType = Source.EnumType
      AND Existing.EnumValue = Source.EnumValue
      AND Existing.LanguageCode = Source.LanguageCode
);
GO

SELECT
    EnumType,
    EnumValue,
    LanguageCode,
    TranslatedText
FROM dbo.Translations
ORDER BY EnumType, EnumValue, LanguageCode;