using System.IO;
using fooddelivery.Models.Constants;

namespace fooddelivery.Libraries.Template
{
    public class EmailTemplate
    {
        public static string EmailPage(string nome, string key, string type, string contactLink)
        {
            var typeText = type == KeyType.Verification ? $"<b>{nome}</b> seu código de confirmação é:" : $"<b>{nome}</b> seu código de recuperação é:";
            var template = File.ReadAllText("wwwroot/EmailTemplate.html")
                                .Replace("[[fieldType]]", typeText)
                                .Replace("[[fieldKey]]", key);

            return template;
        }
    }
}