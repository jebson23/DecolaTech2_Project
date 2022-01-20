namespace DIO.Series.Data.Queries
{
    public class SeriesQuerySql
    {
        public const string FindSeries = @"
SELECT
	A.ID,
    A.GENERO,
    A.TITULO,
    A.DESCRICAO,
    A.ANO,
    A.DELETEDAT
FROM
	SERIES AS A
";

        public const string InsertSeries = @"
INSERT INTO SERIES
(GENERO, TITULO, DESCRICAO, ANO)
VALUES (@Genero, @Titulo, @Descricao, @Ano);
SELECT LAST_INSERT_ID() as '@Id'
";

        public const string UpdateSeries = @"
UPDATE SERIES
SET GENERO = @Genero, TITULO = @Titulo, DESCRICAO = @Descricao, ANO = @Ano
WHERE ID = @Id
";

        public const string DeleteSeries = @"
UPDATE SERIES
SET DELETEDAT = CURRENT_TIMESTAMP
WHERE ID = @Id
";

        public const string FindSeriesId = @"
SELECT
        A.ID,
    A.GENERO,
    A.TITULO,
    A.DESCRICAO,
    A.ANO,
    A.DELETEDAT
FROM
        SERIES AS A
WHERE A.ID = @Id
";
    }
}