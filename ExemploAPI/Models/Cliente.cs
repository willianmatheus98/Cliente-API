using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExemploAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public int Idade { get; set; }
        

        public Cliente(String nome, int idade)
        {
            this.Nome = nome;
            this.Idade = idade;
        }

        public Cliente(int id, String nome, int idade)
        {
            this.Id = id;
            this.Nome = nome;
            this.Idade = idade;
        }
    }
}