using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using ecommerceED1_2.Models;

namespace ecommerceED1_2.Utils
{
    public class reader
    {
        public reader(string ruta)
        {
            string[] lineasCsv = File.ReadAllLines(ruta);
            readerLineByLine(lineasCsv);

        }

        bool isFirstLine = true;

        public void readerLineByLine(string[] lineasCsv)
        {
            foreach (var linea in lineasCsv)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                var lineaDeDatos = linea.Replace("\"", "!");
                var lineaModificada = Regex.Replace(lineaDeDatos, @",(?=[^!]*!([^!]*![^!]*!)*[^!]*$)", " ")
                    .Replace("!", "\"")
                    .Replace("$", "")
                    .Split(',');
                double precioConvertido = Convert.ToDouble(lineaModificada[4].Replace(".", ","));

                Farmacos farmaco = new Farmacos
                {
                    id = int.Parse(lineaModificada[0]),
                    nombreFarmaco = lineaModificada[1],
                    descripcionFarmaco = lineaModificada[2],
                    casaProductora = lineaModificada[3],
                    precio = Convert.ToDouble(lineaModificada[4].Replace(".", ",")),
                    existencia = int.Parse(lineaModificada[5])
                };

                Storage.Instance.listaFarmacos.Add(farmaco);


            }
        }
    }
}