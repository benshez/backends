using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace Shezzy.Firebase.Services.Form
{
    public class DatabaseResultModel
    {
        public Home home { get; set; }
    }

    [FirestoreData]
    public class Home
    {
        [FirestoreProperty]
        public string path { get; set; }
        [FirestoreProperty]
        public string heading { get; set; }
        [FirestoreProperty]
        public int currentStepIndex { get; set; }
        [FirestoreProperty]
        public bool isAuthenticatonRequired { get; set; }
        [FirestoreProperty]
        public Steps steps { get; set; }
    }
    [FirestoreData]
    public class Steps
    {
        [FirestoreProperty]
        public Personal personal { get; set; }
    }
    [FirestoreData]
    public class Personal
    {
        [FirestoreProperty]
        public Firstname firstname { get; set; }
        [FirestoreProperty]
        public Country_Code country_code { get; set; }
    }
    [FirestoreData]
    public class Firstname
    {
        [FirestoreProperty]
        public bool isRequired { get; set; }
        [FirestoreProperty]
        public string component { get; set; }
        [FirestoreProperty]
        public bool isReadonly { get; set; }
        [FirestoreProperty]
        public string helpText { get; set; }
        [FirestoreProperty]
        public string cssClass { get; set; }
        [FirestoreProperty]
        public Validators validators { get; set; }
        [FirestoreProperty]
        public bool isValid { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public bool isVisible { get; set; }
        [FirestoreProperty]
        public string value { get; set; }
        [FirestoreProperty]
        public string mask { get; set; }
    }
    [FirestoreData]
    public class Validators
    {
        [FirestoreProperty]
        public bool expression { get; set; }
    }
    public class FormValidatorTypes
    {
        string Type { get; set;}
        string Espression { get; set; }
    }
    [FirestoreData]
    public class Country_Code
    {
        [FirestoreProperty]
        public string component { get; set; }
        [FirestoreProperty]
        public string cssClass { get; set; }
        [FirestoreProperty]
        public string helpText { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public bool isRequired { get; set; }
        [FirestoreProperty]
        public bool isValid { get; set; }
        [FirestoreProperty]
        public Options options { get; set; }
        [FirestoreProperty]
        public string label { get; set; }
        [FirestoreProperty]
        public bool isReadonly { get; set; }
        [FirestoreProperty]
        public string value { get; set; }
        [FirestoreProperty]
        public bool isVisible { get; set; }
        [FirestoreProperty]
        public Visibleif visibleIf { get; set; }
    }
    [FirestoreData]
    public class Options
    {
        [FirestoreProperty]
        public string LDN { get; set; }
        [FirestoreProperty]
        public string NY { get; set; }
        [FirestoreProperty]
        public string RM { get; set; }
    }
    [FirestoreData]
    public class Visibleif
    {
        [FirestoreProperty]
        public string firstname { get; set; }
    }

}