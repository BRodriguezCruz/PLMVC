using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace PLMVC.Controllers
{
    public class AutomovilController : Controller
    {
        private readonly HttpClient _httpClient;
        public AutomovilController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region METODOS GET ALL SYNCRONO/ASYNCRONO

        //public IActionResult GetAll()
        //{
        //    ML.Automovil auto = new ML.Automovil();
        //    auto.Autos = new List<object>();

        //    _httpClient.BaseAddress = new Uri("http://localhost:5104/api/"); //ejemplo de url en sl: http://localhost:5104/api/Autos/getListaAutos

        //    var response = _httpClient.GetAsync("Autos/getListaAutos");

        //    response.Wait();

        //    var respuesta = response.Result;

        //    if (respuesta.IsSuccessStatusCode)
        //    {
        //        var leerTarea = respuesta.Content.ReadAsAsync<ML.Result>();
        //        leerTarea.Wait();

        //        foreach (var resultAutos in leerTarea.Result.Objects)
        //        {
        //            ML.Automovil autoList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Automovil>(resultAutos.ToString());
        //            auto.Autos.Add(autoList);
        //        }
        //    }
        //    return View(auto);
        //}

        public async Task<ActionResult> GetAll()
        {
            ML.Automovil auto = new ML.Automovil();
            auto.Autos = new List<object>();

            _httpClient.BaseAddress = new Uri("http://localhost:5104/api/");

            try
            {
                var response = await _httpClient.GetAsync("Autos/getListaAutos");
                response.EnsureSuccessStatusCode();

                var resultado = await response.Content.ReadAsAsync<ML.Result>();
                foreach (var resultAutos in resultado.Objects)
                {
                    var autoList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Automovil>(resultAutos.ToString());
                    auto.Autos.Add(autoList);
                }
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message, e);
            }

            return View(auto);
        }
        #endregion

        #region METODOS GETBYID SINC/ASYN

        public async Task<ActionResult> Form(int? idAuto)
        {
            ML.Automovil automovil = new ML.Automovil();

            ML.Result result = BL.Agencia.GetAll();

            try
            {
                if (idAuto > 0)
                {
                    _httpClient.BaseAddress = new Uri("http://localhost:5104/api/");

                    var response = await _httpClient.GetAsync("Autos/getAuto/" + idAuto);
                    response.EnsureSuccessStatusCode();

                    var resultado = await response.Content.ReadAsAsync<ML.Result>();

                    JObject diccionarioAux = JObject.Parse(resultado.Object.ToString()); //parseamos a diccionario de datos JSON para acceder al "IdAgencia" que se pierde
                                                                                         //al deserializar en tipo ML.Automovil

                    automovil = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Automovil>(resultado.Object.ToString());

                    if (automovil != null) 
                    {
                        automovil.Agencia = new ML.Agencia();

                        automovil.Agencia.IdAgencia = diccionarioAux["idAgencia"] != null ? diccionarioAux["idAgencia"].ToObject<int>() : 0;
                        automovil.Agencia.Agencias = result.Objects; //mandamos catalogo de agencias para el desplegar drop down list
                    }
                    else
                    {
                        throw new Exception("Error al obtener datos del automovil");
                    }       
                }
                else
                {
                    automovil.Agencia = new ML.Agencia();
                    automovil.Agencia.Agencias = result.Objects; //mandamos catalogo de agencias para el desplegar drop down list
                }
                return View(automovil);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message, e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }   
        }


        #endregion

        #region METODOS ADD-UPDATE SYNC/ASYNC

        [HttpPost]
        public async Task<ActionResult> Form(ML.Automovil auto)
        {
            try
            {
                auto.Autos = new List<object>();
                auto.Agencia.Agencias = new List<object>();
                auto.Agencia.Nombre = "";

                if (auto.IdAutomovil == 0)
                {
                    _httpClient.BaseAddress = new Uri("http://localhost:5104/api/");

                    var response = await _httpClient.PostAsJsonAsync("Autos/agregarAuto", auto);
                    response.EnsureSuccessStatusCode();

                    var resultado = await response.Content.ReadAsAsync<ML.Result>();

                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Agregado correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al agregar el registro";
                    }

                }
                else
                {
                    _httpClient.BaseAddress = new Uri("http://localhost:5104/api/");
                    var response = await _httpClient.PutAsJsonAsync("Autos/updateAuto", auto);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsAsync<ML.Result>();

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al actualizar el registro";
                    }
                }

                return PartialView("Modal");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Delete 

        
        public async Task<ActionResult> Delete(int idAuto)
        {
            try
            {
                _httpClient.BaseAddress = new Uri("http://localhost:5104/api/"); //http://localhost:5104/api/Autos/deleteAuto/9
                var response = await _httpClient.DeleteAsync("Autos/deleteAuto/" + idAuto);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsAsync<ML.Result>();

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Eliminado Correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al eliminar, intente de nuevo";
                }
                return PartialView("Modal");
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message, e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}
