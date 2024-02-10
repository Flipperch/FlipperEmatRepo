using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Models.ViewModels;
using TestsWebApplication.Repository;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Pages.Alunos
{
    public class DetalhesModel : PageModel
    {
        //TODO: Implementar IAlunoServices afim de integrar diferentes repositorios.
        private readonly IAlunoRepository _alunoRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public DetalhesModel(IAlunoRepository alunoRepository, IEnderecoRepository enderecoRepository)
        {
            _alunoRepository = alunoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public Aluno Aluno { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public LocalNascimentoViewModel LocalNascimento { get; set; }
        public EnderecoViewModel EnderecoAluno { get; set; }

        //public async Task OnGetAsync(int id)
        //{
        //    Aluno = await _alunoRepository.GetAluno(id);
        //}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //DisciplinaAluno = await _disciplinaAlunoRepository.GetDisciplinaAluno((int)id);

            //Aluno e Matrículas
            Aluno = await _alunoRepository.GetAlunoMatriculas((int)id);

            if (Aluno == null)
            {
                return NotFound();
            }

            //Local Nascimento
            if (Aluno.NascimentoCidadeId != null)
            {
                var nascimentoCidade = await _enderecoRepository.GetCidade((int)Aluno.NascimentoCidadeId);
                var nascimentoUf = await _enderecoRepository.GetUf(nascimentoCidade.UfId);
                var nascimentoPais = await _enderecoRepository.GetPais(nascimentoUf.PaisId);

                LocalNascimento = new LocalNascimentoViewModel
                {
                    CidadeNome = nascimentoCidade.CidadeNome ?? string.Empty,
                    UfNome = nascimentoUf.UfNome ?? string.Empty,
                    PaisNome = nascimentoPais.PaisNome ?? string.Empty
                };
            }
            
            //Endereço
            Aluno.EnderecoAluno = await _enderecoRepository.GetEnderecoAluno(Aluno.MatriculaCeeja) ?? null;

            if (Aluno.EnderecoAluno != null)
            {
                EnderecoAluno = new EnderecoViewModel
                {
                    MatriculaCeeja = Aluno.MatriculaCeeja.ToString(),
                    Cep = Aluno.EnderecoAluno.Cep ?? string.Empty,
                    Logradouro = Aluno.EnderecoAluno.Logradouro ?? string.Empty,
                    Numero = Aluno.EnderecoAluno.Numero.ToString() ?? string.Empty,
                    Bairro = Aluno.EnderecoAluno.Bairro ?? string.Empty,
                    Complemento = Aluno.EnderecoAluno.Complemento ?? string.Empty,
                };

                var cidade = await _enderecoRepository.GetCidade(Aluno.EnderecoAluno.CidadeId);

                if (cidade != null)
                {
                    EnderecoAluno.CidadeNome = cidade.CidadeNome;

                    var uf = await _enderecoRepository.GetUf(cidade.UfId);

                    EnderecoAluno.UfNome = uf.UfNome ?? string.Empty;
                }                
            }

            //var enderecoAluno = await _enderecoRepository.GetEnderecoAluno(Aluno.MatriculaCeeja);

            //if (enderecoAluno != null)
            //{
            //    Aluno.EnderecoAluno = enderecoAluno;
            //}


            return Page();
        }
    }
}
