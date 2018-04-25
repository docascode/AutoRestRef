using Newtonsoft.Json;

namespace AutoRestRef.Templates
{
    class InputTemplate
    {
        public string name;
        public string scope;
        [JsonProperty("toc_url")]
        public string tocUrl;
        public InputTemplate(string name, string scope, string tocUrl) {
            this.name = name;
            this.scope = scope;
            this.tocUrl = tocUrl;
        }
    }
}
