﻿CREATE TABLE [dbo].[ATIVIDADE_EXTRA] (
    [COD_ENSINO_ALUNO]   INT      NOT NULL,
    [COD_USUARIO]        SMALLINT NOT NULL,
    [DT_ATIVIDADE_EXTRA] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([COD_ENSINO_ALUNO] ASC),
    CONSTRAINT [FK_ENSINO_ALUNO_ATIVIDADE_EXTRA] FOREIGN KEY ([COD_ENSINO_ALUNO]) REFERENCES [dbo].[ENSINO_ALUNO] ([CODIGO]),
    CONSTRAINT [FK_USUARIO_ATIVIDADE_EXTRA] FOREIGN KEY ([COD_USUARIO]) REFERENCES [dbo].[USUARIO] ([CODIGO])
);

