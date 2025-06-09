using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace Presentacion
{
    public partial class frmCatalogo : Form
    {
        private List<Articulo> listaArticulos;
        public frmCatalogo()
        {
            InitializeComponent();
            cargar();

            
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {

            
            cbxSeleccion.Items.Add("Categoria");
            cbxSeleccion.Items.Add("Marca");
            lblCRSeleccion.Visible = false;
            lblCRCriterio.Visible = false;

            
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {           
            cargar();
            cbxCriterio.SelectedIndex = -1;
            cbxSeleccion.SelectedIndex = -1;
            txbModelo.Text = string.Empty;
            lblCRSeleccion.Visible = false;
            lblCRCriterio.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            frmArticulo nuevo = new frmArticulo();
            nuevo.ShowDialog();
            cargar();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;


            frmArticulo modificado = new frmArticulo(seleccionado);
            modificado.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;           

            try
            {
                // Confirmar la eliminación
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el articulo seleccionado?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumna();
                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

        private void ocultarColumna()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Categoria"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (validarBusqueda()) return;

                string seleccion = cbxSeleccion.SelectedItem.ToString();               
                string criterio = cbxCriterio.SelectedItem.ToString();
                string modelo = txbModelo.Text;

                dgvArticulos.DataSource = negocio.buscar(seleccion, criterio, modelo);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxSeleccion.SelectedIndex == -1) return;

            string opcionSeleccion = cbxSeleccion.SelectedItem.ToString();
            

            if (!string.IsNullOrEmpty(opcionSeleccion))
            {
                lblCRSeleccion.Visible = false;
            }

            

            if (opcionSeleccion == "Marca") 
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Samsung");
                cbxCriterio.Items.Add("Apple");
                cbxCriterio.Items.Add("Sony");
                cbxCriterio.Items.Add("Huawei");
                cbxCriterio.Items.Add("Motorola");

            }
            else 
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Celulares");
                cbxCriterio.Items.Add("Televisores");
                cbxCriterio.Items.Add("Media");
                cbxCriterio.Items.Add("Audio");
                
            }                   
            
        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCriterio.SelectedIndex == -1) return;

            string opcionCriterio = cbxCriterio.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(opcionCriterio))
            {
                lblCRCriterio.Visible = false;
            }
        }


        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalle detalle = new frmDetalle(seleccionado);
            detalle.ShowDialog();
            

        }

        private bool validarBusqueda()
        {
            if (cbxSeleccion.SelectedIndex < 0 )
            {
                lblCRSeleccion.Visible = true;
                
                return true;
            }
            if (cbxCriterio.SelectedIndex < 0)
            {
                lblCRCriterio.Visible = true;
                return true;
            }

            return false;
        }

        
    }
    
}
