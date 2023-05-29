using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using PRUEBAS_LOGIN.Models;
using System.Web.UI.WebControls;

namespace PRUEBAS_LOGIN
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string Patron = "DB_Acceso";
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            string conectar = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection sqlconectar = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand("SP_ValidarUsuarios", sqlconectar)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Connection.Open();
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = tbUsuario.Text;
            cmd.Parameters.Add("@Contrasenia",SqlDbType.VarChar,50).Value = tbPassword.Text;
            cmd.Parameters.Add("@Patron", SqlDbType.VarChar, 50).Value = Patron;
            SqlDataReader dr=cmd.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine("entro");
                //agregar una sesion de usuario
                Response.Redirect("Index2.aspx");   

            }
            else {
                Console.WriteLine("no entro");
            }
            cmd.Connection.Close(); 
        }


    }
}