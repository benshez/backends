using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using Shezzy.Web.ContentFields.MultiValueTextField.Fields;

namespace Shezzy.Web.ContentFields.MultiValueTextField.ViewModels
{
    public class DisplayMultiValueTextFieldViewModel
    {
        public string[] Values => Field.Values;
        public Fields.MultiValueTextField Field { get; set; }
        public ContentPart Part { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
    }
}
