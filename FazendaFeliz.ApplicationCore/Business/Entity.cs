using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FazendaFeliz.ApplicationCore.Business
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
