using Google.Cloud.Firestore;
using System.Linq;
using System.Text.Json;
using Shezzy.Shared.Infrastructure.Json;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace Shezzy.Firebase.Services.Forms
{
    public class DatabaseQueryResponse
    {
        public DatabaseResultModel Frms { get; set; }


        public static Form Deserialize(QuerySnapshot querySnapshot)
        {
            if (querySnapshot == null) return null;

            //Frms = querySnapshot.Documents.Select(
            //    document =>
            //    {
            //        return ConversionDispatcher.Convert(document, document.Id);
            //    }).ToList();

           var documents = from DocumentSnapshot documentSnapshot in querySnapshot.Documents where documentSnapshot.Exists select documentSnapshot;

           // AppDomain.CurrentDomain.GetAssemblies()
           // .Reverse()
           //.Select(assembly => assembly.GetType(document.Id.ToString()))
           //.FirstOrDefault(t => t != null);

           // var x = from document in documents
           //         select document.ConvertTo < AppDomain.CurrentDomain.GetAssemblies()
           // .Reverse()
           //.Select(assembly => assembly.GetType(document.Id.ToString()))
           //.FirstOrDefault(t => t != null))> ();
            return null;
            //return new Form()
            //{
            //    from document in documents select ConversionDispatcher.ConvertToForm(document),
            //    //Id = documents.First().Id,
            //};
        }

        public static string Serialize(DocumentSnapshot documentSnapshot)
        {
            return JsonSerializer.Serialize(documentSnapshot.ToDictionary(), JsonOptions.Default);
        }
    }

    public static class ConversionDispatcher
    {
        public static Form ConvertToForm(DocumentSnapshot document)
        {
            return document.ConvertTo<Form>();
        }
        //public Form Convert<Form>(DocumentSnapshot document)
        //{
        //    return document.ConvertTo<Form>();
        //}
    }
}
