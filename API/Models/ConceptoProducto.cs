using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class ConceptoProducto : Concepto
    {
        public int IDConceptoProducto { get; set; }

        public int IDProducto { get; set; }

    }
}
