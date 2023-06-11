using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Formularios;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Utils
{
    /// <summary>
    /// Classe responsável pelo carregamento das configurações do App.config para a a classe estática EmatriculaSetting.
    /// </summary>
    public class EmatriculaConfigurationSettings : IEmatriculaSettings
    {
        public bool IsComplete
        {
            get
            {
                bool _isComplete = bool.TryParse(ConfigurationManager.AppSettings["EmatriculaSettingsIsComplete"].ToString(),
                    out _isComplete) ? _isComplete : false;

                return _isComplete;
            }
        }

        public string Ceeja => ConfigurationManager.AppSettings["EmatriculaSettingsCeeja"].ToString();

        public string CeejaNome => ConfigurationManager.AppSettings["EmatriculaSettingsCeejaNome"].ToString();

        public Cidade CeejaCidade
        {
            get
            {
                short _CeejaCidadeId = short.TryParse(ConfigurationManager.AppSettings["EmatriculaSettingsCeejaCidadeId"].ToString(),
                    out _CeejaCidadeId) ? _CeejaCidadeId : (short)0;

                return DAO.CidadeDAO.Consultar(_CeejaCidadeId);
            }
        }

        public string DiretorioFotos => ConfigurationManager.AppSettings["EmatriculaSettingsDiretorioFotos"].ToString();

        public string DiretorioBarcode => ConfigurationManager.AppSettings["EmatriculaSettingsDiretorioBarcode"].ToString();

        public bool UIHabilitaMenuAtividadeExtra
        {
            get
            {
                bool _habilitarAtExtra = bool.TryParse(ConfigurationManager.AppSettings["EmatriculaSettingsUIHabilitaMenuAtividadeExtra"].ToString(),
                    out _habilitarAtExtra) ? _habilitarAtExtra : false;

                return _habilitarAtExtra;
            }
        }

        public bool UIHabilitaMenuImpressaoRaLote
        {
            get
            {
                bool _habilitarImpRaLote = bool.TryParse(ConfigurationManager.AppSettings["EmatriculaSettingsUIHabilitaMenuImpressaoRaLote"].ToString(),
                    out _habilitarImpRaLote) ? _habilitarImpRaLote : false;

                return _habilitarImpRaLote;
            }
        }

        public string UINomenclaturaModuloPassaporte => ConfigurationManager.AppSettings["EmatriculaSettingsUINomenclaturaModuloPassaporte"].ToString();

        public string ConnectionStringADO => ConfigurationManager.AppSettings["EmatriculaSettingsConnectionStringADO"].ToString();

        public string ConnectionStringEF => ConfigurationManager.AppSettings["EmatriculaSettingsConnectionStringEF"].ToString();

        public Modelo.Modelo Context => null;


        //Configuration config;

        //public EmatriculaConfigurationSettings()
        //{
        //    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //}

        /*
        public void LoadSettings()
        {
            EmatriculaSetting.IsComplete = true;

            EmatriculaSetting.ConnectionStringADO = GetEmatriculaSettingBase("EmatriculaSettingConnectionStringADO");

            EmatriculaSetting.ConnectionStringEF = GetEmatriculaSettingBase("EmatriculaSettingConnectionStringEF");

            EmatriculaSetting.Ceeja = GetEmatriculaSettingBase("EmatriculaSettingsCeeja");

            EmatriculaSetting.CeejaNome = GetEmatriculaSettingBase("EmatriculaSettingCeejaNome");

            short _CeejaCidadeId = short.TryParse(GetEmatriculaSettingBase("EmatriculaSettingCeejaCidadeId"), out _CeejaCidadeId) ? _CeejaCidadeId : (short)0;
            EmatriculaSetting.CeejaCidade = DAO.CidadeDAO.Consultar(_CeejaCidadeId);

            EmatriculaSetting.DiretorioFotos = GetEmatriculaSettingBase("EmatriculaSettingDiretorioFotos");

            EmatriculaSetting.DiretorioBarcode = GetEmatriculaSettingBase("EmatriculaSettingDiretorioBarcode");

            bool _UIHabilitaMenuAtividadeExtra = bool.TryParse(GetEmatriculaSettingBase("EmatriculaSettingUIHabilitaMenuAtividadeExtra"), out _UIHabilitaMenuAtividadeExtra) ? _UIHabilitaMenuAtividadeExtra : false;
            EmatriculaSetting.UIHabilitaMenuAtividadeExtra = _UIHabilitaMenuAtividadeExtra;

            bool _UIHabilitaMenuImpressaoRaLote = bool.TryParse(GetEmatriculaSettingBase("EmatriculaSettingUIHabilitaMenuImpressaoRaLote"), out _UIHabilitaMenuImpressaoRaLote) ? _UIHabilitaMenuImpressaoRaLote : false;
            EmatriculaSetting.UIHabilitaMenuImpressaoRaLote = _UIHabilitaMenuImpressaoRaLote;

            EmatriculaSetting.UINomenclaturaModuloPassaporte = GetEmatriculaSettingBase("EmatriculaSettingUINomenclaturaModuloPassaporte");
        }

        private string GetEmatriculaSettingBase(string key)
        {
            return config.AppSettings.Settings[key].Value;
        }

        public void SaveEmatriculaSettingBase(string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
        }

        private bool IsEmatriculaSettingsComplete()
        {
            if (config.AppSettings.Settings.Count > 0)
            {
                foreach (string key in config.AppSettings.Settings.AllKeys)
                {
                    if (string.IsNullOrEmpty(config.AppSettings.Settings[key].Value))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        */
    }
}
