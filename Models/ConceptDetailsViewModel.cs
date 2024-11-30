using BIProject.Models;

namespace BIProject.Models
{
    public class ConceptDetailsViewModel
    {
        public required ConceptDetails ConceptDetails { get; set; }
        public required List<ConceptTitle> ConceptTitles { get; set; } = new List<ConceptTitle>();
    }

}