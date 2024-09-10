using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Examen
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarCategorias();
            CargarProductos();
        }

        public class Categoria
        {
            public int IdCategoria { get; set; }
            public string NombreCategoria { get; set; }
            public string Descripcion { get; set; }
            public bool? Activo { get; set; }
            public string CodCategoria { get; set; }
        }

        public class Producto
        {
            public int IdProducto { get; set; }
            public string NombreProducto { get; set; }
            public int IdCategoria { get; set; }
            public decimal PrecioUnidad { get; set; }
            public string CantidadPorUnidad { get; set; }
            public short? UnidadesEnExistencia { get; set; }
            public short? UnidadesEnPedido { get; set; }
            public short? NivelNuevoPedido { get; set; }
            public bool? Suspendido { get; set; }
            public string CategoriaProducto { get; set; }
        }

        private void CargarCategorias()
        {
            string cadena = "Server=LAB1507-19\\SQLEXPRESS; Database=Neptuno2024; Integrated Security=True";
            SqlConnection connection = new SqlConnection(cadena);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("USP_ListarCategorias", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                List<Categoria> listaCategorias = new List<Categoria>();

                while (reader.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        IdCategoria = Convert.ToInt32(reader["idcategoria"]),
                        NombreCategoria = reader["nombrecategoria"].ToString(),
                        Descripcion = reader["descripcion"].ToString(),
                        Activo = reader["Activo"] as bool?,
                        CodCategoria = reader["CodCategoria"].ToString()
                    };
                    listaCategorias.Add(categoria);
                }

                connection.Close();

                dgCategorias.ItemsSource = listaCategorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message);
            }
        }

        private void CargarProductos()
        {
            string cadena = "Server=LAB1507-19\\SQLEXPRESS; Database=Neptuno2024; Integrated Security=True";
            SqlConnection connection = new SqlConnection(cadena);

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("USP_ListarProductos", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                List<Producto> listaProductos = new List<Producto>();

                while (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        IdProducto = Convert.ToInt32(reader["idproducto"]),
                        NombreProducto = reader["nombreProducto"].ToString(),
                        IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                        PrecioUnidad = Convert.ToDecimal(reader["precioUnidad"]),
                        CantidadPorUnidad = reader["cantidadPorUnidad"].ToString(),
                        UnidadesEnExistencia = reader["unidadesEnExistencia"] as short?,
                        UnidadesEnPedido = reader["unidadesEnPedido"] as short?,
                        NivelNuevoPedido = reader["nivelNuevoPedido"] as short?,
                        Suspendido = reader["suspendido"] as bool?,
                        CategoriaProducto = reader["categoriaProducto"].ToString()
                    };
                    listaProductos.Add(producto);
                }

                connection.Close();

                dgProductos.ItemsSource = listaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }
    }
}