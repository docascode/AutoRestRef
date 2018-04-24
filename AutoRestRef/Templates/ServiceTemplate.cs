using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestRef.Templates
{
    class ServiceTemplate
    {
        public string name;
        public string url;
        public string description;
        public ServiceTemplate(string name, string url, string description)
        {
            this.name = name;
            this.url = url;
            this.description = description;
        }
        public override string ToString()
        {
            return $"({this.name}, {this.url}, {this.description})";
        }
    }
}
