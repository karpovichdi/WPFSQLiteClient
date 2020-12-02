using System;
using System.Collections.Generic;
using System.Text;

namespace Turtur
{
    class Resource
    {
        private string name;

        private int id, supplier;

        public string Name { get; set; }

        public int Supplier { get; set; }

        public int Id { get; set; }

        public Resource() { }

        public Resource(string name, int supplier)
        {
            this.supplier = supplier;
            this.name = name;
        }
    }
}
