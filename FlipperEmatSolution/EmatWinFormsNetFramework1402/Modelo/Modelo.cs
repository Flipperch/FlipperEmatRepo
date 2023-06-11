namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EmatWinFormsNetFramework1402.Utils;

    public partial class Modelo : DbContext
    {
        //TODO:: Verificar se colocando o -setting no construtor do Modelo EF resolve... boas práticas ?... Implementar Context, Factory, Etc...
        public Modelo(IEmatriculaSettings _settings) : base(_settings.ConnectionStringEF)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<ALUNO> ALUNO { get; set; }
        public virtual DbSet<AREA> AREA { get; set; }
        public virtual DbSet<ATENDIMENTO> ATENDIMENTO { get; set; }
        public virtual DbSet<ATENDIMENTO_ALUNO> ATENDIMENTO_ALUNO { get; set; }
        public virtual DbSet<ATIVIDADE_EXTRA> ATIVIDADE_EXTRA { get; set; }
        public virtual DbSet<CARTAO_ACESSO> CARTAO_ACESSO { get; set; }
        public virtual DbSet<CIDADE> CIDADE { get; set; }
        public virtual DbSet<DEFICIENCIA> DEFICIENCIA { get; set; }
        public virtual DbSet<DISCIPLINA> DISCIPLINA { get; set; }
        public virtual DbSet<DISCIPLINA_ALUNO> DISCIPLINA_ALUNO { get; set; }
        public virtual DbSet<EMPREGO_ALUNO> EMPREGO_ALUNO { get; set; }
        public virtual DbSet<ENDERECO_ALUNO> ENDERECO_ALUNO { get; set; }
        public virtual DbSet<ENSINO_ALUNO> ENSINO_ALUNO { get; set; }
        public virtual DbSet<HISTORICO_ESCOLAR> HISTORICO_ESCOLAR { get; set; }
        public virtual DbSet<LOCAL_NASCIMENTO> LOCAL_NASCIMENTO { get; set; }
        public virtual DbSet<LOG_ACESSO> LOG_ACESSO { get; set; }
        public virtual DbSet<LOG_CATRACA> LOG_CATRACA { get; set; }
        public virtual DbSet<MOVIMENTACAO> MOVIMENTACAO { get; set; }
        public virtual DbSet<NOTA> NOTA { get; set; }
        public virtual DbSet<PAIS> PAIS { get; set; }
        public virtual DbSet<PROFESSOR> PROFESSOR { get; set; }
        public virtual DbSet<REMATRICULA> REMATRICULA { get; set; }
        public virtual DbSet<UF> UF { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<DEFICIENCIA_ALUNO> DEFICIENCIA_ALUNO { get; set; }
        public virtual DbSet<DEFICIENCIA_USUARIO> DEFICIENCIA_USUARIO { get; set; }
        public virtual DbSet<ERROR_LOG> ERROR_LOG { get; set; }
        public virtual DbSet<MEDIA> MEDIA { get; set; }
        public virtual DbSet<PRINT_LOG> PRINT_LOG { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ALUNO>()
                .Property(e => e.CPF)
                .IsUnicode(false);

            modelBuilder.Entity<ALUNO>()
                .Property(e => e.NOME_PAI)
                .IsUnicode(false);

            modelBuilder.Entity<ALUNO>()
                .HasMany(e => e.DEFICIENCIA_ALUNO)
                .WithRequired(e => e.ALUNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ALUNO>()
                .HasOptional(e => e.ENDERECO_ALUNO)
                .WithRequired(e => e.ALUNO);

            modelBuilder.Entity<ALUNO>()
                .HasMany(e => e.ENSINO_ALUNO)
                .WithRequired(e => e.ALUNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ALUNO>()
                .HasOptional(e => e.EMPREGO_ALUNO)
                .WithRequired(e => e.ALUNO);

            modelBuilder.Entity<ALUNO>()
                .HasOptional(e => e.LOCAL_NASCIMENTO)
                .WithRequired(e => e.ALUNO);

            modelBuilder.Entity<AREA>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<AREA>()
                .HasMany(e => e.PROFESSOR)
                .WithMany(e => e.AREA)
                .Map(m => m.ToTable("AREA_PROFESSOR").MapLeftKey("COD_AREA").MapRightKey("COD_PROFESSOR"));

            modelBuilder.Entity<AREA>()
                .HasMany(e => e.DISCIPLINA)
                .WithMany(e => e.AREA)
                .Map(m => m.ToTable("AREA_DISCIPLINA").MapLeftKey("COD_AREA").MapRightKey("COD_DISCIPLINA"));

            modelBuilder.Entity<ATENDIMENTO>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<ATENDIMENTO>()
                .HasMany(e => e.ATENDIMENTO_ALUNO)
                .WithRequired(e => e.ATENDIMENTO)
                .HasForeignKey(e => e.COD_ATENDIMENTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ATENDIMENTO_ALUNO>()
                .Property(e => e.MODULO)
                .IsUnicode(false);

            modelBuilder.Entity<ATENDIMENTO_ALUNO>()
                .HasMany(e => e.MEDIA)
                .WithOptional(e => e.ATENDIMENTO_ALUNO)
                .HasForeignKey(e => e.COD_ATENDIMENTO_ALUNO);

            modelBuilder.Entity<ATENDIMENTO_ALUNO>()
                .HasOptional(e => e.NOTA)
                .WithRequired(e => e.ATENDIMENTO_ALUNO);

            modelBuilder.Entity<CARTAO_ACESSO>()
                .HasMany(e => e.LOG_CATRACA)
                .WithRequired(e => e.CARTAO_ACESSO)
                .HasForeignKey(e => e.COD_CARTAO_ACESSO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CIDADE>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<CIDADE>()
                .HasMany(e => e.HISTORICO_ESCOLAR)
                .WithOptional(e => e.CIDADE)
                .HasForeignKey(e => e.COD_CIDADE_ANTERIOR);

            modelBuilder.Entity<CIDADE>()
                .HasMany(e => e.ENDERECO_ALUNO)
                .WithRequired(e => e.CIDADE)
                .HasForeignKey(e => e.COD_CIDADE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CIDADE>()
                .HasMany(e => e.EMPREGO_ALUNO)
                .WithOptional(e => e.CIDADE)
                .HasForeignKey(e => e.COD_CIDADE);

            modelBuilder.Entity<CIDADE>()
                .HasMany(e => e.LOCAL_NASCIMENTO)
                .WithOptional(e => e.CIDADE)
                .HasForeignKey(e => e.COD_CIDADE);

            modelBuilder.Entity<DEFICIENCIA>()
                .HasMany(e => e.DEFICIENCIA_ALUNO)
                .WithRequired(e => e.DEFICIENCIA)
                .HasForeignKey(e => e.COD_DEFICIENCIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DEFICIENCIA>()
                .HasMany(e => e.DEFICIENCIA_USUARIO)
                .WithRequired(e => e.DEFICIENCIA)
                .HasForeignKey(e => e.COD_DEFICIENCIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINA>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPLINA>()
                .Property(e => e.NOME_HISTORICO)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPLINA>()
                .Property(e => e.HORARIO)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPLINA>()
                .Property(e => e.CAPACIDADE)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPLINA>()
                .HasMany(e => e.ATENDIMENTO)
                .WithRequired(e => e.DISCIPLINA)
                .HasForeignKey(e => e.COD_DISCIPLINA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINA>()
                .HasMany(e => e.DISCIPLINA_ALUNO)
                .WithOptional(e => e.DISCIPLINA)
                .HasForeignKey(e => e.COD_DISCIPLINA);

            modelBuilder.Entity<DISCIPLINA>()
                .HasMany(e => e.PROFESSOR)
                .WithRequired(e => e.DISCIPLINA)
                .HasForeignKey(e => e.COD_DISCIPLINA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINA_ALUNO>()
                .HasMany(e => e.ATENDIMENTO_ALUNO)
                .WithRequired(e => e.DISCIPLINA_ALUNO)
                .HasForeignKey(e => e.COD_DISCIPLINA_ALUNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINA_ALUNO>()
                .HasMany(e => e.MEDIA)
                .WithRequired(e => e.DISCIPLINA_ALUNO)
                .HasForeignKey(e => e.COD_DISCIPLINA_ALUNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMPREGO_ALUNO>()
                .Property(e => e.NOME_EMPRESA)
                .IsUnicode(false);

            modelBuilder.Entity<EMPREGO_ALUNO>()
                .Property(e => e.TELEFONE)
                .IsUnicode(false);

            modelBuilder.Entity<ENDERECO_ALUNO>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<ENDERECO_ALUNO>()
                .Property(e => e.LOGRADOURO)
                .IsUnicode(false);

            modelBuilder.Entity<ENDERECO_ALUNO>()
                .Property(e => e.BAIRRO)
                .IsUnicode(false);

            modelBuilder.Entity<ENDERECO_ALUNO>()
                .Property(e => e.COMPLEMENTO)
                .IsUnicode(false);

            modelBuilder.Entity<ENSINO_ALUNO>()
                .HasOptional(e => e.ATIVIDADE_EXTRA)
                .WithRequired(e => e.ENSINO_ALUNO);

            modelBuilder.Entity<ENSINO_ALUNO>()
                .HasMany(e => e.DISCIPLINA_ALUNO)
                .WithOptional(e => e.ENSINO_ALUNO)
                .HasForeignKey(e => e.COD_ENSINO_ALUNO);

            modelBuilder.Entity<ENSINO_ALUNO>()
                .HasOptional(e => e.HISTORICO_ESCOLAR)
                .WithRequired(e => e.ENSINO_ALUNO);

            modelBuilder.Entity<ENSINO_ALUNO>()
                .HasMany(e => e.MOVIMENTACAO)
                .WithRequired(e => e.ENSINO_ALUNO)
                .HasForeignKey(e => e.COD_ENSINO_ALUNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ENSINO_ALUNO>()
                .HasMany(e => e.REMATRICULA)
                .WithRequired(e => e.ENSINO_ALUNO)
                .HasForeignKey(e => e.COD_ENSINO_ALUNO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.OBS)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.LIVRO)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.PAGINA)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.TERMO)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.SERIE_ANTERIOR)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.INSTITUICAO_ANTERIOR)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.FUNDAMENTACAO)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORICO_ESCOLAR>()
                .Property(e => e.GDAE)
                .IsUnicode(false);

            modelBuilder.Entity<MOVIMENTACAO>()
                .Property(e => e.MOTIVO)
                .IsUnicode(false);

            modelBuilder.Entity<PAIS>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<PAIS>()
                .HasMany(e => e.UF)
                .WithRequired(e => e.PAIS)
                .HasForeignKey(e => e.COD_PAIS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROFESSOR>()
                .HasMany(e => e.ATENDIMENTO_ALUNO)
                .WithRequired(e => e.PROFESSOR)
                .HasForeignKey(e => e.COD_PROFESSOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROFESSOR>()
                .HasMany(e => e.ATENDIMENTO_ALUNO1)
                .WithOptional(e => e.PROFESSOR1)
                .HasForeignKey(e => e.COD_PROFESSOR_MODIFICACAO);

            modelBuilder.Entity<UF>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<UF>()
                .Property(e => e.SIGLA)
                .IsUnicode(false);

            modelBuilder.Entity<UF>()
                .HasMany(e => e.CIDADE)
                .WithRequired(e => e.UF)
                .HasForeignKey(e => e.COD_UF)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NOME_ACESSO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.SENHA)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.RG)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.ALUNO)
                .WithOptional(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.ATIVIDADE_EXTRA)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.HISTORICO_ESCOLAR)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO_DIRETOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.HISTORICO_ESCOLAR1)
                .WithRequired(e => e.USUARIO1)
                .HasForeignKey(e => e.COD_USUARIO_SECRETARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.HISTORICO_ESCOLAR2)
                .WithRequired(e => e.USUARIO2)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.LOG_ACESSO)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.MOVIMENTACAO)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasOptional(e => e.PROFESSOR)
                .WithRequired(e => e.USUARIO);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.REMATRICULA)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.DEFICIENCIA_USUARIO)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.MEDIA)
                .WithRequired(e => e.USUARIO)
                .HasForeignKey(e => e.COD_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.MEDIA1)
                .WithOptional(e => e.USUARIO1)
                .HasForeignKey(e => e.COD_USUARIO_MODIFICACAO);

            modelBuilder.Entity<DEFICIENCIA_ALUNO>()
                .Property(e => e.OBS)
                .IsUnicode(false);

            modelBuilder.Entity<DEFICIENCIA_USUARIO>()
                .Property(e => e.OBS)
                .IsUnicode(false);

            modelBuilder.Entity<ERROR_LOG>()
                .Property(e => e.THREAD)
                .IsUnicode(false);

            modelBuilder.Entity<ERROR_LOG>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<ERROR_LOG>()
                .Property(e => e.SOURCE)
                .IsUnicode(false);

            modelBuilder.Entity<ERROR_LOG>()
                .Property(e => e.MESSAGE)
                .IsUnicode(false);

            modelBuilder.Entity<ERROR_LOG>()
                .Property(e => e.EXCEPTION)
                .IsUnicode(false);

            modelBuilder.Entity<MEDIA>()
                .Property(e => e.VALOR)
                .IsUnicode(false);

            modelBuilder.Entity<MEDIA>()
                .Property(e => e.INSTITUICAO)
                .IsUnicode(false);

            modelBuilder.Entity<PRINT_LOG>()
                .Property(e => e.REPORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<PRINT_LOG>()
                .Property(e => e.PARAMETERS)
                .IsUnicode(false);
        }
    }
}
