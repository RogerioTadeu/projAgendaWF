using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace projAgendaWF
{
    public partial class Form1 : Form
    {
        private ListaContatos agenda;

        public void salvarJSON()
        {
            Stream stream = File.OpenWrite("contatos.json");
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListaContatos));
            serializer.WriteObject(stream, this.agenda);
            stream.Close();
        }

        public void recuperarJSON()
        {
            if (File.Exists("contatos.json"))
            {
                Stream stream = File.OpenRead("contatos.json");
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListaContatos));
                this.agenda = (ListaContatos)serializer.ReadObject(stream);
                stream.Close();
            }
        }

        public Form1()
        {
            InitializeComponent();
            agenda = new ListaContatos();
            this.recuperarJSON();
            //agenda.recuperar();

            //agenda.incluir(new Contato("joao@nada", "Joao Silva", "1111"));
            //agenda.incluir(new Contato("maria@nada", "Maria Santos", "2222"));
            //agenda.incluir(new Contato("carlos@nada", "Carlos Souza", "3333"));
            //agenda.incluir(new Contato("antonia@nada", "Antonia Rocha", "4444"));
        }

        private void limparInterface(string email, string nome, string fone)
        {
            txtEmail.Text = email;
            txtNome.Text = nome;
            txtFone.Text = fone;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparInterface("", "", "");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Contato contatoPesquisado = new Contato(txtEmail.Text, "", "");
            contatoPesquisado = agenda.pesquisar(contatoPesquisado);
            if (contatoPesquisado.Email == "")
            {
                agenda.incluir(new Contato(txtEmail.Text,
                    txtNome.Text,
                    txtFone.Text));
            }
            else
            {
                agenda.alterar(new Contato(txtEmail.Text,
                    txtNome.Text,
                    txtFone.Text));
            }
            MessageBox.Show("Contato gravado");
            limparInterface("", "", "");
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Contato contatoPesquisado = new Contato(txtEmail.Text, "", "");
            contatoPesquisado = agenda.pesquisar(contatoPesquisado);
            if (contatoPesquisado.Email == "")
            {
                MessageBox.Show("Contato não encontrado");
            }
            else
            {
                limparInterface(contatoPesquisado.Email, 
                    contatoPesquisado.Nome, 
                    contatoPesquisado.Fone);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Contato contatoPesquisado = new Contato(txtEmail.Text, "", "");
            contatoPesquisado = agenda.pesquisar(contatoPesquisado);
            if (contatoPesquisado.Email == "")
            {
                MessageBox.Show("Contato não encontrado");
            }
            else
            {
                agenda.remover(contatoPesquisado);
                MessageBox.Show("Contato excluído");
                limparInterface("", "", "");
            }
            BindingList<Contato> lista = new BindingList<Contato>(agenda.MeusContatos);
            dgContatos.DataSource = lista;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            BindingList<Contato> lista = new BindingList<Contato>(agenda.MeusContatos);
            dgContatos.DataSource = lista;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //agenda.gravar();
            salvarJSON();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
