using EmatWinFormsNetFramework1402.Classes;

namespace EmatWinFormsNetFramework1402.Utils
{
    public interface IEmatriculaSettings
    {
        bool IsComplete { get; }

        string Ceeja { get; }

        string CeejaNome { get; }

        Cidade CeejaCidade { get; }

        string DiretorioFotos { get; }

        string DiretorioBarcode { get; }

        bool UIHabilitaMenuAtividadeExtra { get; }

        bool UIHabilitaMenuImpressaoRaLote { get; }

        string UINomenclaturaModuloPassaporte { get; }

        string ConnectionStringADO { get; }

        string ConnectionStringEF { get; }

        Modelo.Modelo Context { get; }
    }
}