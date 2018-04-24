using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestRef.Templates
{
    class OutputTemplate
    {
        public string name;
        public string scope;
        public List<ServiceTemplate> services;

        public OutputTemplate(string name, string scope, List<ServiceTemplate> services)
        {
            this.name = name;
            this.scope = scope;
            this.services = services;
        }
        public override string ToString()
        {
            return $"{this.name}, {this.scope}, {this.services.Select(s => s.ToString())}";
        }
    }
}
