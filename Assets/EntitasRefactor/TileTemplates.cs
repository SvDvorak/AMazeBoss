using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.CodeGenerator;

namespace Assets.EntitasRefactor
{
    [SingleEntity]
    public class TileTemplates : IComponent
    {
        public TemplateNames Value;
    }

    public class TemplateNames : Dictionary<string, SubtemplateNames>
    {
        public Tuple<string, List<string>> Retrieve(string type)
        {
            var firstSubtypeNames = this[type.ToUpper()].First();
            return new Tuple<string, List<string>>(firstSubtypeNames.Key, firstSubtypeNames.Value);
        }

        public List<string> Retrieve(string type, string subtype)
        {
            return this[type.ToUpper()][subtype.ToUpper()];
        }
    }

    public class SubtemplateNames : Dictionary<string, List<string>>
    {
    }
}