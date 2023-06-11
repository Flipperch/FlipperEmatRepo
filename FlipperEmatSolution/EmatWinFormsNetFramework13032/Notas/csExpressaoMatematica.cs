using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework13032.Notas
{
    class csExpressaoMatematica
    {
         public double calcular(string expressao_matematica)
         {
             double resultado = 0;

             int qtd_parenteses = 0; //expressao_matematica.Count(f => f == '(');
             foreach (char c in expressao_matematica)
                 if(c == '(') qtd_parenteses++; 

             

             for (int i = qtd_parenteses; i > 0;i--)
             {
                 int a = expressao_matematica.IndexOf('(', 0, qtd_parenteses);
             }
             
             return resultado;
       }

       

       private string somar(string expressao_matematica)
       {
           string retorno;
           double soma = 0;

           string formula = expressao_matematica;
           double numero = 0;
           
           formula = formula.Replace('(', '|');
           formula = formula.Replace(')', '|');

           string[] array = formula.Split('|');
           
           for(int i = 0; i < array.Length; i++)
           {
               if(array[i].Contains('+'))
               {
                   string[] array1 = array[i].Split('+');

                   array[i] = (Convert.ToDouble(array1[0]) + Convert.ToDouble(array1[1])).ToString();
               }
               else
               {
                   
               }
           }

           

           return retorno = soma.ToString();
       }
    }
}
