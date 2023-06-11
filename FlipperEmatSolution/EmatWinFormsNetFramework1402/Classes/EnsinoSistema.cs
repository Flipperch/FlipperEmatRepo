using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Enumeradores;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class EnsinoSistema
    {
        private Enumeradores.Ensino ensino;
        private List<Disciplina> listaDisciplinas;

        public EnsinoSistema()
        {
            listaDisciplinas = new List<Disciplina>();
        }
        public List<Disciplina> ListaDisciplinas { get => listaDisciplinas; set => listaDisciplinas = value; }
        public Enumeradores.Ensino Ensino { get => ensino; set => ensino = value; }
        public static EnsinoSistema ensinoFundamental()
        {
            EnsinoSistema retorno = new EnsinoSistema();
            retorno.Ensino = Enumeradores.Ensino.FUNDAMENTAL;
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(1)); //PORTUGUÊS
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(2)); //MATEMÁTICA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(4)); //HISTÓRIA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(7)); //INGLÊS            
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(8)); //ARTE
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(11)); //CIENCIAS
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(12)); //GEOGRAFIA
            return retorno;                                                 
        }                                                                   
        public static EnsinoSistema ensinoMedio()
        {           
            EnsinoSistema retorno = new EnsinoSistema();
            retorno.Ensino = Enumeradores.Ensino.MÉDIO;
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(1)); //PORTUGUÊS
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(2)); //MATEMÁTICA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(3)); //QUÍMICA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(4)); //HISTÓRIA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(5)); //FÍSICA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(6)); //BIOLOGIA
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(7)); //INGLÊS            
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(8)); //ARTE            
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(9)); //FILOSOFIA            
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(10)); //SOCIOLOGIA            
            retorno.ListaDisciplinas.Add(DAO.DisciplinaDAO.Consultar(12)); //GEOGRAFIA
            return retorno;
        }

        
    }
}
