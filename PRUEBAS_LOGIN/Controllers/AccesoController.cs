﻿using PRUEBAS_LOGIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;

namespace PRUEBAS_LOGIN.Controllers
{
    public class AccesoController : Controller
    {

        static string cadena = "Data Source=masbien;Initial Catalog=DB_Acceso;Integrated Security=true";
        // GET: Acceso 
        public ActionResult Login()
        {
            return View();
        }



        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool Registrado;
            string Mensaje;
            if(oUsuario.Clave == oUsuario.ConfirmarClave) {
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
            }
            else
            {

                ViewData["Mensaje"] = "las contraseñas no coinciden";
                return View();  
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario",cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.Parameters.Add("Registrado",SqlDbType.Bit).Direction=ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                Registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
;            }


            ViewData["Mensaje"] = Mensaje;
            if (Registrado)
            {
                return RedirectToAction("Login","Acceso");
            }
            else
            {

                return View();
            }

            
        }


        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
         {
            oUsuario.Clave = ConvertirSha256(oUsuario.Clave);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
              
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                 oUsuario.idUsuario = Convert.ToInt32 (cmd.ExecuteScalar().ToString());
                
            }
             if(oUsuario.idUsuario != 0)
            {
                Session["Usuario"] = oUsuario;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewData["Mensaje"] ="Usuario no encontrado";
                return View();
            }


            
        }







        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}