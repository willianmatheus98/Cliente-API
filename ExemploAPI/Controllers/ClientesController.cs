using ExemploAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ExemploAPI.Controllers
{
    public class ClientesController : ApiController
    {
        private static List<Cliente> clientes = new List<Cliente>();

        public List<Cliente> Get(int id)
        {
            List<Cliente> clis = new List<Cliente>();
            if (id != null)
            {
                clis = returnCliente(clis, id);
            }
            return clis;
        }

        public List<Cliente> Get()
        {
            List<Cliente> clis = new List<Cliente>();

            clis = returnClientes(clis);

            return clis;
        }

        public List<Cliente> returnClientes(List<Cliente> clis)
        {
            DataSet1TableAdapters.ClienteTableAdapter d = new DataSet1TableAdapters.ClienteTableAdapter();
            DataSet1.ClienteDataTable dataTable = d.GetData();
            if (dataTable != null)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    clis.Add(new Cliente((int)item["Id"], item["Nome"].ToString(), (int)item["Idade"]));
                }
            }
            return clis;
        }

        public List<Cliente> returnCliente(List<Cliente> clis, int id)
        {
            DataSet1TableAdapters.ClienteTableAdapter d = new DataSet1TableAdapters.ClienteTableAdapter();
            DataSet1.ClienteDataTable dataTable = d.GetDataById(id);
            if (dataTable != null)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    clis.Add(new Cliente((int)item["Id"], item["Nome"].ToString(), (int)item["Idade"]));
                }
            }
            return clis;
        }

        public Cliente Post(String nome, int idade)
        {
            Cliente c = new Cliente(nome, idade);
            if (!String.IsNullOrEmpty(nome))
            {
                DataSet1TableAdapters.ClienteTableAdapter d = new DataSet1TableAdapters.ClienteTableAdapter();
                int idAfterInsert = d.InsertQuery(c.Nome, c.Idade);
                c.Id = idAfterInsert;
                clientes.Add(c);
            }
            return c;
        }

        public Cliente Put(int id, String nome, int idade)
        {
            Cliente c = new Cliente(id, nome, idade);
            if (!String.IsNullOrEmpty(nome))
            {
                DataSet1TableAdapters.ClienteTableAdapter d = new DataSet1TableAdapters.ClienteTableAdapter();
                int idAfterInsert = d.UpdateQuery(c.Nome, c.Idade, c.Id);
            }
            return c;
        }

        /*
        [HttpPost]
        public List<Cliente> Post(Cliente c)
        {

            if (c != null)
            {
                //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                //List<Cliente> clis = jsonSerializer.Deserialize<List<Cliente>>(jsonBody);
                //foreach (Cliente c in clis)
                //{
                    DataSet1TableAdapters.ClienteTableAdapter d = new DataSet1TableAdapters.ClienteTableAdapter();
                    int idAfterInsert = d.InsertQuery(c.Nome, c.Idade);
                    c.Id = idAfterInsert;
               // }
               // clientes.AddRange(clis);

            }
            return clientes;
        }
         */
        public String Delete(int id)
        {
            String nome = "";
            DataSet1TableAdapters.ClienteTableAdapter d = new DataSet1TableAdapters.ClienteTableAdapter();
            DataSet1.ClienteDataTable dataTable = d.GetDataById(id);
            if (dataTable != null)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    nome = item["Nome"].ToString();
                }
            }

            d.DeleteQuery(id);
            if (nome != "")
            {
                return "Cliente " + nome + " removido com sucesso";
            }
            else
            {
                return "Não existe cliente com o id= " + id;
            }

        }

    }
}