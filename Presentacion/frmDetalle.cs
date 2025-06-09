using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo = null;
        public frmDetalle(Articulo seleccionado)
        {
            InitializeComponent();
            this.articulo = seleccionado;
            
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            lblNombre.Text = articulo.Nombre;
            lblCategoriaDetalle.Text = articulo.Categoria.Descripcion;
            lblMarcaDetalle.Text = articulo.Marca.Descripcion;
            lblPrecioDetalle.Text = articulo.Precio.ToString();
            txtDescripcionDetalle.Text = articulo.Descripcion;
            cargarImagen(articulo.ImagenUrl);
            //pbxImagenDetalle.Load(articulo.ImagenUrl);                  
        }

        private void btnAceptarDetalle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagenDetalle.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagenDetalle.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }
    }
}
