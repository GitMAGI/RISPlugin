using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.VO
{
    public class RichiestaRISVO
    {
        public string data { get; set; }
        public string data_creazione { get; set; }
        public string data_modifica { get; set; }
        public bool dimprotetta { get; set; }
        public string esami { get; set; }
        public string idepisodio { get; set; }
        public string locker { get; set; }
        public string motivo { get; set; }
        public string nomeesami { get; set; }
        public string nomeutente_creazione { get; set; }
        public string nomeutente_modifica { get; set; }
        public string objectid { get; set; }
        public string ora { get; set; }
        public string pdfcreato { get; set; }
        public string quesitoclinico { get; set; }
        public long seriale { get; set; }
        public string statopaziente { get; set; }
        public bool urgente { get; set; }
        public string versione { get; set; }
    }
}
