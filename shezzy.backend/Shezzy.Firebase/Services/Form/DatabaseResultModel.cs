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

public class Rootobject
{
    public Tenants tenants { get; set; }
}

public class Tenants
{
    public DreamWeddings dreamweddings { get; set; }
}

public class DreamWeddings
{
    public Forms forms { get; set; }
    public Users users { get; set; }
}

public class Forms
{
    public Home home { get; set; }
    public About about { get; set; }
    public Profile profile { get; set; }
}

public class Home
{
    public int currentStepIndex { get; set; }
    public string heading { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public bool isAuthenticatonRequired { get; set; }
    public Steps steps { get; set; }
}

public class Steps
{
    public Personal personal { get; set; }
    public Some_Other some_other { get; set; }
    public The_End the_end { get; set; }
}

public class Personal
{
    public Firstname firstname { get; set; }
    public Country_Code country_code { get; set; }
    public City city { get; set; }
    public int inValidItemsCount { get; set; }
    public int stepIndex { get; set; }
}

public class Firstname
{
    public string component { get; set; }
    public string cssClass { get; set; }
    public string helpText { get; set; }
    public string id { get; set; }
    public string label { get; set; }
    public string value { get; set; }
    public Validators validators { get; set; }
    public bool isValid { get; set; }
    public bool isRequired { get; set; }
    public bool isReadonly { get; set; }
    public bool isVisible { get; set; }
}

public class Validators
{
    public _0 _0 { get; set; }
}

public class _0
{
    public string type { get; set; }
}

public class Country_Code
{
    public string component { get; set; }
    public string cssClass { get; set; }
    public string helpText { get; set; }
    public string id { get; set; }
    public bool isRequired { get; set; }
    public bool isValid { get; set; }
    public string label { get; set; }
    public Options options { get; set; }
    public bool isReadonly { get; set; }
    public string value { get; set; }
    public bool isVisible { get; set; }
    public Visibleif visibleIf { get; set; }
}

public class Options
{
    public _01 _0 { get; set; }
    public _1 _1 { get; set; }
    public _2 _2 { get; set; }
    public _3 _3 { get; set; }
    public _4 _4 { get; set; }
}

public class _01
{
    public string key { get; set; }
    public string value { get; set; }
}

public class _1
{
    public string key { get; set; }
    public string value { get; set; }
}

public class _2
{
    public string key { get; set; }
    public string value { get; set; }
}

public class _3
{
    public string key { get; set; }
    public string value { get; set; }
}

public class _4
{
    public string key { get; set; }
    public string value { get; set; }
}

public class Visibleif
{
    public _02 _0 { get; set; }
}

public class _02
{
    public string key { get; set; }
    public string value { get; set; }
}

public class City
{
    public string component { get; set; }
    public string cssClass { get; set; }
    public string helpText { get; set; }
    public string id { get; set; }
    public bool isRequired { get; set; }
    public bool isValid { get; set; }
    public string label { get; set; }
    public Options1 options { get; set; }
    public bool isReadonly { get; set; }
    public Validators1 validators { get; set; }
    public bool isVisible { get; set; }
}

public class Options1
{
    public _03 _0 { get; set; }
    public _11 _1 { get; set; }
}

public class _03
{
    public string key { get; set; }
    public string value { get; set; }
}

public class _11
{
    public string key { get; set; }
    public string value { get; set; }
}

public class Validators1
{
    public _04 _0 { get; set; }
}

public class _04
{
    public string type { get; set; }
}

public class Some_Other
{
    public Surname surname { get; set; }
    public Aswitch aswitch { get; set; }
    public int inValidItemsCount { get; set; }
    public int stepIndex { get; set; }
}

public class Surname
{
    public string component { get; set; }
    public string cssClass { get; set; }
    public string helpText { get; set; }
    public string id { get; set; }
    public bool isRequired { get; set; }
    public bool isValid { get; set; }
    public string label { get; set; }
    public bool isReadonly { get; set; }
    public bool isVisible { get; set; }
}

public class Aswitch
{
    public string component { get; set; }
    public string cssClass { get; set; }
    public string helpText { get; set; }
    public string id { get; set; }
    public bool isRequired { get; set; }
    public bool isValid { get; set; }
    public string label { get; set; }
    public bool isReadonly { get; set; }
    public bool value { get; set; }
    public bool isVisible { get; set; }
}

public class The_End
{
    public Theend theend { get; set; }
}

public class Theend
{
    public string component { get; set; }
    public int inValidItemsCount { get; set; }
    public int stepIndex { get; set; }
}

public class About
{
    public int currentStepIndex { get; set; }
    public string heading { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public int inValidItemsCount { get; set; }
    public bool isAuthenticatonRequired { get; set; }
}

public class Profile
{
    public int currentStepIndex { get; set; }
    public string heading { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public int inValidItemsCount { get; set; }
    public bool isAuthenticatonRequired { get; set; }
}

public class Users
{
    public _05 _0 { get; set; }
}

public class _05
{
    public string surname { get; set; }
    public string name { get; set; }
    public string email { get; set; }
}
