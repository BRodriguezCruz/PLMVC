using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Automovil
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AutomotrizBRodriguezContext bd = new DL.AutomotrizBRodriguezContext())
                {
                    List<ML.Automovil> autos = bd.Automovils.Select(a => new ML.Automovil
                    {
                        IdAutomovil = a.IdAutomovil,
                        Marca = a.Marca,
                        Modelo = a.Modelo,
                        Generacion = a.Generacion,
                        Color = a.Color,
                        FechaLanzamiento = (a.FechaLanzamiento).ToString(),
                        Agencia = new ML.Agencia
                        {
                            IdAgencia = a.IdAgencia ?? 0,
                            //Nombre = a.IdAgenciaNavigation.Nombre
                        }
                    }).ToList();

                    if (autos.Count != 0)
                    {
                        result.Objects = autos.Cast<object>().ToList(); //castea de tipo ML AUTOMOVIL A TIPO OBJECT
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Sin registro de autos en la DB.";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AutomotrizBRodriguezContext db = new DL.AutomotrizBRodriguezContext())
                {
                    var auto = db.Automovils.Where(a => a.IdAutomovil == id).FirstOrDefault();

                    if (auto != null)
                    {
                        result.Correct = true;
                        result.Object = auto;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Registro no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result AddAuto(ML.Automovil auto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AutomotrizBRodriguezContext db = new DL.AutomotrizBRodriguezContext())
                {
                    DL.Automovil newAuto = new DL.Automovil
                    {
                        Marca = auto.Marca,
                        Modelo = auto.Modelo,
                        Generacion = auto.Generacion,
                        Color = auto.Color,
                        FechaLanzamiento = DateTime.Parse(auto.FechaLanzamiento),
                        IdAgencia = auto.Agencia.IdAgencia
                    };

                    db.Automovils.Add(newAuto);

                    int success = db.SaveChanges();

                    if(success != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result UpdateAuto(ML.Automovil auto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AutomotrizBRodriguezContext db = new DL.AutomotrizBRodriguezContext())
                {
                    var oldAuto = db.Automovils.Find(auto.IdAutomovil);

                    if (oldAuto == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Registro no encontrado.";
                    }
                    else
                    {
                        oldAuto.Marca = auto.Marca;
                        oldAuto.Modelo = auto.Modelo;
                        oldAuto.Generacion = auto.Generacion;
                        oldAuto.Color = auto.Color;
                        oldAuto.FechaLanzamiento = DateTime.Parse(auto.FechaLanzamiento);
                        oldAuto.IdAgencia = auto.Agencia.IdAgencia;

                        db.Automovils.Update(oldAuto);

                        int success = db.SaveChanges();

                        if (success != 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static ML.Result DeleteAuto(int idAuto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AutomotrizBRodriguezContext db = new DL.AutomotrizBRodriguezContext())
                {
                    var auto = db.Automovils.Find(idAuto);

                    if (auto != null)
                    {
                        db.Automovils.Remove(auto);

                        int success = db.SaveChanges();

                        result.Correct = success != 0;
                    }
                    else
                    {
                        result.ErrorMessage = "Registro no encontrado";
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
