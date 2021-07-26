using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1201
{
    public class BaseDatos
    {
        readonly string cadena = "Data Source=192.168.1.28; Initial Catalog=FACTURACION1201; User ID=sa; Password=Estado2012";

        public bool ValidarUsuario(string codigo, string clave)
        {
            bool EsUsuarioValido = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT 1 FROM USUARIOS WHERE CODIGO = @Codigo AND CLAVE = @Clave; ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using(SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 30).Value = clave;

                        EsUsuarioValido = Convert.ToBoolean(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception)
            {
            }
            return EsUsuarioValido;
        }

        public DataTable CargarCategorias()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM CATEGORIAS ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using(SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        SqlDataReader dr = comando.ExecuteReader();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public bool InsertarProducto(string codigo, string descripcion, int idcategoria, decimal precio, int existencia)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO PRODUCTOS ");
                sql.Append(" VALUES (@Codigo, @Descripcion, @IdCategoria, @Precio, @Existencia); ");

                using(SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 80).Value = descripcion;
                        comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = idcategoria;
                        comando.Parameters.Add("@Precio", SqlDbType.Decimal).Value = precio;
                        comando.Parameters.Add("@Existencia", SqlDbType.Int).Value = existencia;
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable ListarProductos()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT P.CODIGO, P.DESCRIPCION, C.DESCRIPCION CATEGORIA, P.PRECIO, P.EXISTENCIA ");
                sql.Append(" FROM PRODUCTOS P ");
                sql.Append(" INNER JOIN CATEGORIAS C ON C.ID = P.IDCATEGORIA ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        SqlDataReader dr = comando.ExecuteReader();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public bool EditarProducto(string codigo, string descripcion, int idcategoria, decimal precio, int existencia)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE PRODUCTOS ");
                sql.Append(" SET DESCRIPCION = @Descripcion, IDCATEGORIA = @IdCategoria, PRECIO = @Precio, EXISTENCIA = @Existencia ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 80).Value = descripcion;
                        comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = idcategoria;
                        comando.Parameters.Add("@Precio", SqlDbType.Decimal).Value = precio;
                        comando.Parameters.Add("@Existencia", SqlDbType.Int).Value = existencia;
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
           
        }


    }
}
