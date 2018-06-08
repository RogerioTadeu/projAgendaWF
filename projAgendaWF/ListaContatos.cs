using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace projAgendaWF
{
    [DataContract]
    class ListaContatos // CONTROLLER
    {
        #region atributos
        private List<Contato> meusContatos;
        #endregion

        #region propriedades
        [DataMember(Name = "meuscontatos")]
        public List<Contato> MeusContatos
        {
            get { return meusContatos; }
            set { meusContatos = value; }
        }
        #endregion

        #region construtores
        public ListaContatos()
        {
            this.meusContatos = new List<Contato>();
        }
        #endregion

        #region métodos
        public void incluir(Contato contato)
        {
            this.meusContatos.Add(contato);
        }

        public void alterar(Contato contato)
        {
            int posicao = 0;
            while(!this.meusContatos[posicao].Equals(contato) && 
                posicao<this.meusContatos.Count)
                posicao++;

            if (posicao < this.meusContatos.Count)
            {
                this.meusContatos.Remove(contato);
                this.meusContatos.Insert(posicao, contato);
            }
        }

        public void remover(Contato contato)
        {
            this.meusContatos.Remove(contato);
        }

        public Contato pesquisar(Contato contato)
        {
            Contato contatoPesquisado = new Contato();
            foreach(Contato c in this.meusContatos)
                if(c.Equals(contato))
                    contatoPesquisado = c;
            return contatoPesquisado;
        }

        public void gravar()
        {
            StreamWriter arq = new StreamWriter("contatos.txt");
            foreach (Contato contato in this.meusContatos)
                arq.WriteLine(contato.dados());
            arq.Close();
        }

        public void recuperar()
        {
            if (File.Exists("contatos.txt"))
            {
                StreamReader arq = new StreamReader("contatos.txt");
                while (!arq.EndOfStream)
                    this.incluir(new Contato(arq.ReadLine()));
                arq.Close();
            }
        }


        #endregion
    }
}
