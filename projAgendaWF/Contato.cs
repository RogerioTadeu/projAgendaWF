using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace projAgendaWF
{
    [DataContract]
    class Contato // MODEL
    {
        #region atributos
        private string email;
        private string nome;
        private string fone;
        #endregion

        #region propriedades
        [DataMember(Name = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember(Name = "nome")] 
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        [DataMember(Name = "fone")] 
        public string Fone
        {
            get { return fone; }
            set { fone = value; }
        }
        #endregion

        #region construtores
        public Contato(string email, string nome, string fone)
        {
            this.email = email;
            this.nome = nome;
            this.fone = fone;
        }

        public Contato()
            : this("", "", "")
        { }

        public Contato(string dados)
        {
            this.email = dados.Substring(0, 50).Trim();
            this.nome = dados.Substring(50, 50).Trim();
            this.fone = dados.Substring(100, 15).Trim();
        }
        #endregion

        public string dados()
        {
            return String.Format("{0,-50}{1,-50}{2,-15}", this.email, this.nome, this.fone);
        }

        #region sobrecargas
        public override bool Equals(object obj)
        {
            return this.email.Equals(((Contato)obj).Email);
        }

        public override string ToString()
        {
            return "E-mail: " + this.email + " | " +
                   "Nome: " + this.nome + " | " +
                   "Fone: " + this.fone;
        }
        #endregion
    }
}
