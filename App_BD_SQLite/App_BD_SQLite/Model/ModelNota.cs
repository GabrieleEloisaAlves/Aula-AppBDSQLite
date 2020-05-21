using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace App_BD_SQLite.Model
{
    [Table("Anotacoes")]
   public class ModelNota
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Titulo { get; set; }
        [NotNull]
        public string Dados { get; set; }
        [NotNull]
        public Boolean Favorito { get; set; }
        
        public ModelNota()
        {
            this.Id = 0;
            this.Dados = "";
            this.Favorito = false;
            this.Titulo = "";
        }
    }
}
