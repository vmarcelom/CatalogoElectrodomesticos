using Dominio;
using Negocio;
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
    public partial class frmArticulo : Form      
    {

        private Articulo articulo = null;

        public frmArticulo()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
        }

        public frmArticulo (Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";



        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            lblCodigo.Visible = false;
            lblNombre.Visible = false;
            lblDescripcion.Visible = false;           
            lblPrecio.Visible = false;
            lblCampoRequerido.Visible = false;

            try
            {
                cbxCategoria.DataSource = categoriaNegocio.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                cbxMarca.DataSource = marcaNegocio.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";

                if ( articulo != null)
                {
                    txbCodigo.Text = articulo.Codigo;
                    txbNombre.Text = articulo.Nombre;
                    txbDescripcion.Text = articulo.Descripcion;
                    txbImagenUrl.Text = articulo.ImagenUrl;
                    txbPrecio.Text = articulo.Precio.ToString();
                    cargarImagen(articulo.ImagenUrl);
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            //Articulo articulo = new Articulo();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                if (validarCampo())
                    return;
                

                articulo.Codigo = txbCodigo.Text;
                articulo.Nombre = txbNombre.Text;
                articulo.Descripcion = txbDescripcion.Text;
                articulo.ImagenUrl = txbImagenUrl.Text;
                articulo.Precio = decimal.Parse(txbPrecio.Text);
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;

                if (articulo.Id != 0)
                {
                    
                    negocio.modificar(articulo);
                    MessageBox.Show("Articulo modificado correctamente");
                    Close();
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Articulo agregado correctamente");
                    Close();
                }
                
                
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticuloNuevo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticuloNuevo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

        private void txbImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txbImagenUrl.Text);
        }

        private bool validarCampo ()
        {
            if (string.IsNullOrEmpty(txbCodigo.Text) || string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbDescripcion.Text) || string.IsNullOrEmpty(txbPrecio.Text))
            {
                lblCodigo.Visible = true;
                lblNombre.Visible = true;
                lblDescripcion.Visible = true;
                lblPrecio.Visible = true;
                lblCampoRequerido.Visible = true;

                return true;
            }

            if (decimal.TryParse(txbPrecio.Text, out decimal result)) // para validar que el campo precio sea un numero deciimal valido
            {
                return false;
            }
            else
            {
                MessageBox.Show("El campo Precio debe ser un número decimal válido.");
                return true;
            }

           
        }

    }
       
}
