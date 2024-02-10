﻿CREATE TABLE [dbo].[HISTORICO_ESCOLAR] (
    [COD_ENSINO_ALUNO]       INT           NOT NULL,
    [OBS]                    VARCHAR (MAX) NULL,
    [COD_USUARIO_DIRETOR]    SMALLINT      NOT NULL,
    [COD_USUARIO_SECRETARIO] SMALLINT      NOT NULL,
    [COD_USUARIO]            SMALLINT      NOT NULL,
    [DT_LIVRO]               DATE          NULL,
    [LIVRO]                  VARCHAR (MAX) NULL,
    [PAGINA]                 VARCHAR (MAX) NULL,
    [TERMO]                  VARCHAR (MAX) NULL,
    [DT_DOCUMENTO]           DATE          NULL,
    [DT_CONCLUSAO]           DATE          NULL,
    [SERIE_ANTERIOR]         VARCHAR (MAX) NULL,
    [INSTITUICAO_ANTERIOR]   VARCHAR (MAX) NULL,
    [ANO_ANTERIOR]           INT           NULL,
    [COD_CIDADE_ANTERIOR]    SMALLINT      NULL,
    [FUNDAMENTACAO]          VARCHAR (MAX) NULL,
    [GDAE]                   VARCHAR (MAX) NULL,
    [SEGUNDA_VIA]            BIT           NULL,
    CONSTRAINT [PK_HISTORICO_ESCOLAR] PRIMARY KEY CLUSTERED ([COD_ENSINO_ALUNO] ASC),
    CONSTRAINT [FK_CIDADE_ANTERIOR_HISTORICO_ESCOLAR] FOREIGN KEY ([COD_CIDADE_ANTERIOR]) REFERENCES [dbo].[CIDADE] ([CODIGO]),
    CONSTRAINT [FK_DIRETOR_HISTORICO_ESCOLAR] FOREIGN KEY ([COD_USUARIO_DIRETOR]) REFERENCES [dbo].[USUARIO] ([CODIGO]),
    CONSTRAINT [FK_ENSINO_ALUNO_HISTORICO_ESCOLAR] FOREIGN KEY ([COD_ENSINO_ALUNO]) REFERENCES [dbo].[ENSINO_ALUNO] ([CODIGO]),
    CONSTRAINT [FK_SECRETARIO_HISTORICO_ESCOLAR] FOREIGN KEY ([COD_USUARIO_SECRETARIO]) REFERENCES [dbo].[USUARIO] ([CODIGO]),
    CONSTRAINT [FK_USUARIO_HISTORICO_ESCOLAR] FOREIGN KEY ([COD_USUARIO]) REFERENCES [dbo].[USUARIO] ([CODIGO])
);

