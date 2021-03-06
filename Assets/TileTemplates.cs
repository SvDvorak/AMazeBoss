using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.CodeGenerator;

namespace Assets
{
    [SingleEntity, Game]
    public class TileTemplates : IComponent
    {
        public TemplateNames Value;
    }

    public class TemplateNames : Dictionary<string, SubtemplateNames>
    {
        public Tuple<string, List<string>> Retrieve(string type)
        {
            try
            {
                var firstSubtypeNames = this[type.ToUpper()].First();
                return new Tuple<string, List<string>>(firstSubtypeNames.Key, firstSubtypeNames.Value);
            }
            catch (Exception)
            {
                throw new MissingTemplateException(type);
            }
        }

        public List<string> Retrieve(string type, string subtype)
        {
            try
            {
                return this[type.ToUpper()][subtype.ToUpper()];
            }
            catch (Exception)
            {
                throw new MissingTemplateException(type, subtype);
            }
        }
    }

    public class MissingTemplateException : Exception
    {
        public MissingTemplateException(string type, string subtype = null) : base(string.Format("Could not retrieve {0} - {1}", type, subtype ?? "none")) { }
    }

    public class SubtemplateNames : Dictionary<string, List<string>>
    {
    }
}