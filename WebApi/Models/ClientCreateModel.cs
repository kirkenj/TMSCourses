using System.ComponentModel.DataAnnotations;
using WebApi.Models.Attributes;

namespace WebApi.Models
{
    public class ClientCreateModel
    {
        [MaxLength(10)]
        [BannedSymbolsAttribute(new[] {'*', '?', '!'})]
        [NameValidation(1, ' ')]
        [BannedWords(new[] { "admin" })]
        public string Name { get; set; } = null!;
        [MaxLength(10)]
        [BannedSymbolsAttribute(new[] { '*', '?', '!' })]
        [BannedWords(new[] { "admin" })]
        [NameValidation(1, ' ')]

        public string Surname { get; set; } = null!;
    }
}