using System;
using System.Text;
using System.Collections.Generic;

namespace EmatWinFormsNetFramework13032.Digitais.Entity
{
    public class Usuario
    {
        #region Propriedades

        #region NroUsuario
        private string _nroUsuario;
        public string NroUsuario
        {
            get { return _nroUsuario; }
            set { _nroUsuario = value; }
        }
        #endregion

        #region TemplateA
        private byte[] _templateA;
        public byte[] TemplateA
        {
            get { return _templateA; }
            set { _templateA = value; }
        }
        #endregion

        #region TemplateB
        private byte[]  _templateB;
        public byte[]  TemplateB
        {
            get { return _templateB; }
            set { _templateB = value; }
        }
        #endregion

        #region TemplateFinal
        private byte[] _templateFinal;
        public byte[] TemplateFinal
        {
            get { return _templateFinal; }
            set { _templateFinal = value; }
        }
        #endregion

        #endregion

        #region Métodos

        #region ToString
        public override string ToString()
        {
            bool temTemplate = false;
            string TemplateSN = string.Empty;

            if (this.TemplateA != null || this.TemplateB != null || this.TemplateFinal != null)
            {
                temTemplate = true;    
            }

            if (temTemplate)
            {
                TemplateSN = " - Template : SIM";
            }
            else
            {
                TemplateSN = " - Template : não";
            }

            //return base.ToString();
            return "Nro:" + NroUsuario + TemplateSN;
        }
        #endregion

        #region Usuario
        //Constructor
        public Usuario(string numUsuario)
        {
            this.NroUsuario = numUsuario;
        }
        #endregion

        #region Usuario
        //Constructor
        public Usuario()
        {

        }
        #endregion

        #endregion
    }
}
