using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Configurações
{
    public class CSconnexoes_txt
    {
        public string caminho_app = @"C:\E-mat\configuracoes.txt";
        public string caminho_fotos;
        public string caminho_barcodes;
        public string caminho_bd;
        public string modo;

        public string conf_video_webcam;
        public string conf_audio_webcam;

        public string stringsql;

        string cambd = "banco>";
        string camft = "fotos>";
        string cambc = "barcodes>";
        string web_conf = "webconf>";
        string modo_conf = "modo_conf>";

        List<string> list_caminhos = new List<string>();

        public void get_configuracoes()
        {
            int counter = 0;
            string line;
            if (File.Exists(caminho_app))
            {
                StreamReader read = new StreamReader(caminho_app);
                while ((line = read.ReadLine()) != null)
                {                    
                    if (line.Contains("fotos"))
                    {
                        
                        string[] caminho = line.Split('>');
                        caminho_fotos = caminho[1];
                    }
                    if (line.Contains("webconf"))
                    {
                        string[] caminho = line.Split('>');
                        conf_video_webcam = caminho[1];
                        conf_audio_webcam = caminho[2];
                    }
                    if (line.Contains("modo_conf"))
                    {
                        string[] caminho = line.Split('>');
                        modo = caminho[1];                        
                    }
                    if (line.Contains("barcodes"))
                    {
                        string[] caminho = line.Split('>');
                        caminho_barcodes = caminho[1];
                    }

                    counter++;
                }
                caminho_fotos = caminho_fotos + @"\";
                read.Close();                
            }           
        }

        public void atualiza_configuracoes()
        {
            if (File.Exists(caminho_app))
            {
                list_caminhos.Add(camft + caminho_fotos);
                list_caminhos.Add(web_conf + conf_video_webcam+">"+conf_audio_webcam);
                list_caminhos.Add(modo_conf + modo);

                File.WriteAllLines(caminho_app, list_caminhos);
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminho_app));

                using (FileStream fs = new FileStream(caminho_app, FileMode.Create))
                {
                    fs.Close();

                    list_caminhos.Add(camft + caminho_fotos);
                    list_caminhos.Add(web_conf + conf_video_webcam + ">" + conf_audio_webcam);
                    list_caminhos.Add(modo_conf + modo);

                    File.WriteAllLines(caminho_app, list_caminhos);
                } 
            }
        }

        public void define_cam_bd()
        {
            get_configuracoes();
            Conexoes.SqlConnectionString = caminho_bd;
        }

        public void GetSqlConnection()
        {
            stringsql = "Data Source=(LocalDB)\v11.0;AttachDbFilename=" + caminho_app + ";Integrated Security=True;Connect Timeout=30";
        }

       
    }
}
