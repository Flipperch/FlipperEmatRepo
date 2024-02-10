CREATE TABLE [dbo].[TB_BKP_MEDIAS_SEM_ATENDIMENTO] (
    [ID_MEDIA]             INT            NOT NULL,
    [ID_ATENDIMENTO]       INT            NULL,
    [ID_DISCIPLINA]        INT            NULL,
    [N_MAT]                INT            NULL,
    [ENSINO]               INT            NULL,
    [INSTITUICAO]          NVARCHAR (100) NULL,
    [MUNICIPIO]            NVARCHAR (100) NULL,
    [UF]                   NVARCHAR (50)  NULL,
    [MEDIA]                NVARCHAR (100) NULL,
    [DAT_INI]              DATE           NULL,
    [DAT_FIN]              DATE           NULL,
    [USU_LANC_MEDIA]       INT            NULL,
    [USU_MOD_MEDIA]        INT            NULL,
    [DAT_MOD_MEDIA]        DATETIME       NULL,
    [COD_INSTITUICAO]      INT            NULL,
    [COD_ENSINO_ALUNO]     INT            NULL,
    [N_MAT_]               INT            NOT NULL,
    [COD_ENSINO_ALUNO_]    INT            NULL,
    [COD_ENSINO]           TINYINT        NULL,
    [COD_DISCIPLINA_ALUNO] INT            NOT NULL,
    [COD_DISCIPLINA]       TINYINT        NULL
);

