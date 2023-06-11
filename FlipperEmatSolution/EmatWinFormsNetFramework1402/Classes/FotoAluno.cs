using EmatWinFormsNetFramework1402.Properties;
using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    /// <summary>
    /// Classe Foto do Aluno: Representa a foto do aluno e contem as informações de cod da foto e caminho da imagem que fica grava no servidor bem como a imagem.
    /// </summary>
    public class FotoAluno
    {
        //TODO:: Porque não colocar no construtor e chamar
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IEmatriculaSettings _settings;

        private string caminho;
        private Image foto;
        public FotoAluno(int nDeMatricula, IEmatriculaSettings settings)
        {
            _settings = settings;

            Image foto = Resources.sem_foto;

            if (_settings.DiretorioFotos != null)
            {
                if (File.Exists(_settings.DiretorioFotos + @"\" + nDeMatricula + ".png"))
                    caminho = _settings.DiretorioFotos + @"\" + nDeMatricula + ".png";
                else if (File.Exists(_settings.DiretorioFotos + @"\" + nDeMatricula + ".jpg"))
                    caminho = _settings.DiretorioFotos + @"\" + nDeMatricula + ".jpg";
                else
                    return;
                try
                {
                    System.Threading.Thread.Sleep(1 * 100);
                    FileStream stream = new FileStream(caminho, FileMode.Open, FileAccess.Read);
                    foto = Image.FromStream(stream);
                    stream.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }

                this.Caminho = caminho;
                this.Foto = foto;
            }
        }

        public string Caminho { get => caminho; set => caminho = value; }
        public Image Foto { get => foto; set => foto = value; }
    }
}
