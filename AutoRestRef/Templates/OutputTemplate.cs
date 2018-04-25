using System.Collections.Generic;

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
    }
}
