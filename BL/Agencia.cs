using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Agencia
    {
       public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result(); 
			try
			{
                using (AutomotrizBRodriguezContext db = new AutomotrizBRodriguezContext())
                {
                    var agencias = db.Agencia.Select(a => new ML.Agencia
                    {
                        IdAgencia = a.IdAgencia,
                        Nombre = a.Nombre,
                    }).ToList();

                    if (agencias.Count >= 0)
                    {
                        result.Objects = agencias.Cast<object>().ToList();
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Sin registros en la BD";
                        result.Correct = false;
                    }
                }
			}
			catch (Exception e)
			{
                result.ErrorMessage= e.Message;
                result.Correct = false;
                result.Ex = e;
			}
            return result;
        }
    }
}
