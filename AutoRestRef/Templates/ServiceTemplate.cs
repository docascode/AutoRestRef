using System;

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
    }
}
