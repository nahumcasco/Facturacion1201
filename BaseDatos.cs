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
                sql.Append(" SELECT 1 FROM USUARIOS WHERE CODIGO = @Codigo AND CLAVE = @Clave AND ESTAACTIVO = 1; ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
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

        public bool InsertarProducto(string codigo, string descripcion, int idcategoria, decimal precio, int existencia, byte[] imagen)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO PRODUCTOS ");
                sql.Append(" VALUES (@Codigo, @Descripcion, @IdCategoria, @Precio, @Existencia, @Imagen); ");

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
                        comando.Parameters.Add("@Imagen", SqlDbType.Image).Value = imagen;
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

        public byte[] SeleccionarImagenproducto(string codigo)
        {
            byte[] _imagen = new byte[0];
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT IMAGEN FROM PRODUCTOS ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;

                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            _imagen = (byte[])dr["IMAGEN"];
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return _imagen;
        }

        public bool EliminarProducto(string codigo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM PRODUCTOS ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
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


        public bool InsertarUsuario(string codigo, string nombre, string clave)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO USUARIOS ");
                sql.Append(" VALUES (@Codigo, @Clave, @Nombre, @EstaActivo); ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = nombre;
                        comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 30).Value = clave;
                        comando.Parameters.Add("@EstaActivo", SqlDbType.Bit).Value = true;

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

        public bool EditarUsuario(string codigo, string nombre, string clave, bool estado)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE USUARIOS ");
                sql.Append(" SET NOMBRE = @Nombre, CLAVE = @Clave, ESTAACTIVO = @EstaActivo ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = nombre;
                        comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 30).Value = clave;
                        comando.Parameters.Add("@EstaActivo", SqlDbType.Bit).Value = estado;
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

        public DataTable SeleccionarUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT CODIGO, NOMBRE, CLAVE, ESTAACTIVO FROM USUARIOS ");

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

        public bool EliminarUsuario(string codigo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM USUARIOS ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
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

        public bool InsertarCliente(string identidad, string nombre, int telefono, string direccion, byte[] imagen)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO CLIENTES ");
                sql.Append(" VALUES (@Identidad, @Nombre, @Telefono, @Direccion, @Imagen);");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 20).Value = identidad;
                        comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = nombre;
                        comando.Parameters.Add("@Telefono", SqlDbType.Int).Value = telefono;
                        comando.Parameters.Add("@Direccion", SqlDbType.NVarChar, 80).Value = direccion;
                        comando.Parameters.Add("@Imagen", SqlDbType.Image).Value = imagen;
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

        public bool EditarCliente(int id, string identidad, string nombre, int telefono, string direccion, byte[] imagen)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE CLIENTES ");
                sql.Append(" SET IDENTIDAD = @Identidad, NOMBRE = @Nombre, TELEFONO = @Telefono, DIRECCION = @Direccion, IMAGEN = @Imagen ");
                sql.Append(" WHERE ID = @Id ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 20).Value = identidad;
                        comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = nombre;
                        comando.Parameters.Add("@Telefono", SqlDbType.Int).Value = telefono;
                        comando.Parameters.Add("@Direccion", SqlDbType.NVarChar, 80).Value = direccion;
                        comando.Parameters.Add("@Imagen", SqlDbType.Image).Value = imagen;
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

        public DataTable SeleccionarClientes()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM CLIENTES ");

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

        public bool EliminarCliente(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM CLIENTES ");
                sql.Append(" WHERE ID = @Id ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
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

        public byte[] SeleccionarImagenCliente(int id)
        {
            byte[] _imagen = new byte[0];
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT IMAGEN FROM CLIENTES ");
                sql.Append(" WHERE ID = @Id ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            _imagen = (byte[])dr["IMAGEN"];
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return _imagen;
        }

        public string GetNombreUsuario(string codigoUsuario)
        {
            string nombre = string.Empty;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT NOMBRE FROM USUARIOS ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigoUsuario;
                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            nombre = dr["NOMBRE"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return nombre;
        }

        public List<KeyValuePair<int, string>> GetClienteParaFactura(string identidad)
        {
            List<KeyValuePair<int, string>> miLista = new List<KeyValuePair<int, string>>();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID, NOMBRE FROM CLIENTES ");
                sql.Append(" WHERE IDENTIDAD = @Identidad ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 20).Value = identidad;

                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            miLista.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr["ID"].ToString()), dr["NOMBRE"].ToString()));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return miLista;
        }

        public DataTable SeleccionarClientesPorNombre(string nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM CLIENTES  WHERE NOMBRE LIKE ('%" + nombre + "%') ");

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

        public Producto GetProdoctoPorCodigo(string codigo)
        {
            Producto _producto = new Producto();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT CODIGO, DESCRIPCION, PRECIO, EXISTENCIA FROM PRODUCTOS WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigo;
                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            _producto.Codigo = dr["CODIGO"].ToString();
                            _producto.Descripcion = dr["DESCRIPCION"].ToString();
                            _producto.Precio = Convert.ToDecimal(dr["PRECIO"]);
                            _producto.Existencia = Convert.ToInt32(dr["EXISTENCIA"]);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return _producto;
        }


        public int GetIdUsuario(string codigoUsuario)
        {
            int id = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID FROM USUARIOS ");
                sql.Append(" WHERE CODIGO = @Codigo ");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    _conexion.Open();
                    using (SqlCommand comando = new SqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigoUsuario;
                        SqlDataReader dr = comando.ExecuteReader();
                        if (dr.Read())
                        {
                            id = (int)dr["ID"];
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return id;
        }


        public async Task<bool> InsertarFacturaAsync(Factura _factura, List<DetalleFactura> _detalleFacturaList)
        {
            try
            {
                StringBuilder sqlF = new StringBuilder();
                sqlF.Append(" INSERT INTO FACTURA ");
                sqlF.Append(" VALUES (@IdCliente, @IdUsuario, @Fecha, @SubTotal, @Descuento, @Impuesto, @Total);");
                sqlF.Append(" SELECT SCOPE_IDENTITY() ");

                StringBuilder sqlDF = new StringBuilder();
                sqlDF.Append(" INSERT INTO DETALLEFACTURA ");
                sqlDF.Append(" VALUES (@IdFactura, @CodigoProducto, @Cantidad, @Precio);");

                using (SqlConnection _conexion = new SqlConnection(cadena))
                {
                    await _conexion.OpenAsync();

                    SqlTransaction transaccion = await Task.Run<SqlTransaction>(
                        () => _conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                        );

                    try
                    {
                        SqlCommand comando1 = new SqlCommand(sqlF.ToString(), _conexion, transaccion);
                        comando1.CommandType = CommandType.Text;

                        

                        comando1.Parameters.Add("@IdCliente", SqlDbType.Int).Value = _factura.IdCliente;
                        comando1.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _factura.Fecha;
                        comando1.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = _factura.IdUsuario;
                        comando1.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = _factura.SubTotal;
                        comando1.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = _factura.Descuento;
                        comando1.Parameters.Add("@Impuesto", SqlDbType.Decimal).Value = _factura.Impuesto;
                        comando1.Parameters.Add("@Total", SqlDbType.Decimal).Value = _factura.Total;
                       
                        int idFactura = 0;
                        idFactura =  Convert.ToInt32( await comando1.ExecuteScalarAsync());

                        foreach (var item in _detalleFacturaList)
                        {
                            SqlCommand comando2 = new SqlCommand(sqlDF.ToString(), _conexion, transaccion);
                            comando2.CommandType = CommandType.Text;

                            comando2.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura;
                            comando2.Parameters.Add("@CodigoProducto", SqlDbType.NVarChar, 30).Value = item.Codigo;
                            comando2.Parameters.Add("@Cantidad", SqlDbType.Int).Value = item.Cantidad;
                            comando2.Parameters.Add("@Precio", SqlDbType.Decimal).Value = item.Precio;
                            await comando2.ExecuteNonQueryAsync();
                        }
                        await Task.Run(() => transaccion.Commit());
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
