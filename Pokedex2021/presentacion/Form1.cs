using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace presentacion
{
    public partial class Form1 : Form
    {

        private List<Pokemon> listaPokemons;
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            //listaPokemons = new List<Pokemon>();
            PokemonNegocio pokemonNegocio = new PokemonNegocio();

            try
            {
                listaPokemons = pokemonNegocio.listar3();
                dgvPokemons.DataSource = listaPokemons;
                ocultarColumnas();
                

                RecargarImg(listaPokemons[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPokemons_MouseClick(object sender, MouseEventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            RecargarImg(seleccionado.UrlImagen);
            RecargarNombre(seleccionado.Nombre, seleccionado.Numero);
            RecargarTipo(seleccionado.Tipo.Nombre);
            RecargarDesc(seleccionado.Descripcion);

        }
        private void ocultarColumnas()
        {
            //Oculto Columnas de la grilla.
            //Puedo poner el indice de la columna o el nombre de la propiedad.
            dgvPokemons.Columns["Id"].Visible = false;
            dgvPokemons.Columns["Ficha"].Visible = false;
            dgvPokemons.Columns["Descripcion"].Visible = false;
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            dgvPokemons.Columns["Evolucion"].Visible = false;
            dgvPokemons.Columns["Tipo"].Visible = false;
            dgvPokemons.Columns["Debilidad"].Visible = false;
        }

        private void RecargarImg(string img)
        {
            pbxPokemon.Load(img);
        }
        private void RecargarDesc(string desc)
        {
            tbDesc.Text = desc;
        }
        private void RecargarNombre(string nombre, int id)
        {
            tbNombre.Text = id.ToString() + " " + nombre;
        }
        private void RecargarTipo(string tipo)
        {
            tbTipo.Text = tipo;
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            frmPokemon modificar = new frmPokemon(seleccionado);
            modificar.ShowDialog();
            cargarGrilla();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmPokemon agregar = new frmPokemon();
            agregar.ShowDialog();
            cargarGrilla();
        }

        private void btnAgregarElemento_Click(object sender, EventArgs e)
        {
            frmElemento elemento = new frmElemento();
            elemento.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                if (MessageBox.Show("Estás seguro de eliminarlo?", "Eliminandooo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionado.Id);
                    cargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            busqueda();
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            busqueda();
        }

        private void busqueda()
        {
            //txtFiltro
            List<Pokemon> listaFiltrada;
            if (txtFiltro.Text != "")
            {
                listaFiltrada = listaPokemons.FindAll(PEPE => PEPE.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || PEPE.Tipo.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                dgvPokemons.DataSource = null;
                dgvPokemons.DataSource = listaFiltrada;
            }
            else
            {
                dgvPokemons.DataSource = null;
                dgvPokemons.DataSource = listaPokemons;
            }

            ocultarColumnas();
        }

    }
}
