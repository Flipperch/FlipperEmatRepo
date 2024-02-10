using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Aluno
    {
        [Display(Name = "Matrícula")]
        public int MatriculaCeeja { get; set; }
        public DateTime DataMatriculaCeeja { get; set; }
        public string Cpf { get; set; }

        [Display(Name = "RA")]
        public string? Ra { get; set; }

        [Display(Name = "RG")]
        public string? Rg { get; set; }
        public string UfRg { get; set; }
        public string OrgaoRg { get; set; }
        public string DataRg { get; set; }

        [Display(Name = "Nome")]
        public string? Nome { get; set; }
        public string DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public CorOrigemEtnica CorOrigemEtnica { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string TermoMatricula { get; set; } //TODO: Verificar o campo TermoMatricula, pois fica na tabela ALUNO.
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public bool Concluinte { get; set; }
        public string ObsPassaporte { get; set; } //TODO: Verificar o campo ObsPassaporte, pois fica na tabela ALUNO.
        public bool ApresentouCertidao { get; set; }
        public bool ApresentouHistorico { get; set; }
        public string NomeSocial { get; set; }
        public int UsuarioId { get; set; }
        public string DigRa { get; set; }
        public string UfRa { get; set; }

        public int? NascimentoCidadeId { get; set; }

        public EnderecoAluno? EnderecoAluno { get; set; }


        [Display(Name = "Matrículas")]
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }

    public enum Sexo
    {
        MASCULINO = 1,
        FEMININO = 2
    }

    public enum EstadoCivil
    {
        [Description("SOLTEIRO(A)")]
        SOLTEIRO = 1,
        [Description("CASADO(A)")]
        CASADO = 2,
        [Description("SEPARADO(A)")]
        SEPARADO = 3,
        [Description("VIÚVO(A)")]
        VIUVO = 4,
        [Description("UNIÃO ESTÁVEL")]
        UNIAO = 5,
        [Description("DIVORCIADO(A)")]
        DIVORCIADO = 6
    }

    public enum CorOrigemEtnica
    {
        [Description("BRANCA")]
        BRANCA = 1,
        [Description("NEGRA")]
        NEGRA = 2,
        [Description("AMARELA")]
        AMARELA = 3,
        [Description("PARDA")]
        PARDA = 4,
        [Description("INDÍGINA")]
        INDÍGINA = 5,
        [Description("NÃO INFORMADO")]
        NÃO = 6,
    }



}
