namespace Shezzy.Firebase.Services.Form
{
    public class DatabaseResultModel
    {
        public Shezzy shezzy { get; set; }
        public string Id { get; set; }
    }

    public class Shezzy
    {
        public int currentStepIndex { get; set; }
        public string heading { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public bool requiresAuthenticaton { get; set; }
        public Step[] steps { get; set; }
    }

    public class Step
    {
        public Element[] elements { get; set; }
        public int inValidItemsCount { get; set; }
        public string label { get; set; }
        public int pageIndex { get; set; }
    }

    public class Element
    {
        public string component { get; set; }
        public string cssClass { get; set; }
        public string helpText { get; set; }
        public string id { get; set; }
        public bool isRequired { get; set; }
        public bool isValid { get; set; }
        public string label { get; set; }
        public bool _readonly { get; set; }
        public Validator[] validators { get; set; }
        public object value { get; set; }
        public bool visible { get; set; }
        public Option[] options { get; set; }
        public Visibleif[] visibleIf { get; set; }
    }

    public class Validator
    {
        public string type { get; set; }
    }

    public class Option
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Visibleif
    {
        public string key { get; set; }
        public string value { get; set; }
    }

}
