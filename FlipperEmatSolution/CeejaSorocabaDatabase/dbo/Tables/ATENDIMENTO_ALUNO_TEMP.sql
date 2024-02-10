CREATE TABLE [dbo].[ATENDIMENTO_ALUNO_TEMP] (
    [CODIGO]                    INT          IDENTITY (1, 1) NOT NULL,
    [COD_DISCIPLINA_ALUNO]      INT          NOT NULL,
    [COD_ATENDIMENTO]           SMALLINT     NOT NULL,
    [COD_PROFESSOR]             SMALLINT     NOT NULL,
    [DT_ATENDIMENTO]            DATETIME     NOT NULL,
    [COD_PROFESSOR_MODIFICACAO] SMALLINT     NULL,
    [DT_MODIFICACAO]            DATETIME     NULL,
    [MODULO]                    VARCHAR (20) NULL,
    [NOTA]                      FLOAT (53)   NULL,
    [CODIGO_MEDIA]              INT          NULL,
    [COD_INSTITUICAO]           INT          NULL,
    [VALOR]                     VARCHAR (20) NULL,
    [DT_MEDIA]                  DATE         NULL,
    [COD_USUARIO]               SMALLINT     NULL,
    [COD_USUARIO_MODIFICACAO]   SMALLINT     NULL,
    [DT_MODIFICACAO_MEDIA]      DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([CODIGO] ASC)
);

