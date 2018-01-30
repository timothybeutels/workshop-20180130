using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.DomainTests
{
    public class DomainSpecification
    {
        private readonly Dictionary<string, object> _commands = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _events = new Dictionary<string, object>();

        public DomainSpecification Command(string name, object specification)
        {
            _commands.Add(name, specification);
            return this;
        }

        public DomainSpecification Event(string name, object specification)
        {
            _events.Add(name, specification);
            return this;
        }

        public Dictionary<string, string> Build()
        {
            List<KeyValuePair<string, object>> typesToGenerate = new List<KeyValuePair<string, object>>();
            List<KeyValuePair<string, string>> generatedCode = new List<KeyValuePair<string, string>>();
            foreach (var cmd in _commands)
            {
                generatedCode.Add(BuildClass(cmd, "Command", "", out var toGenerate));

                typesToGenerate.AddRange(toGenerate);
            }
            foreach (var cmd in _events)
            {
                generatedCode.Add(BuildClass(cmd, "Event", "", out var toGenerate));

                typesToGenerate.AddRange(toGenerate);
            }
            foreach (var toGenerate in typesToGenerate)
            {
                generatedCode.Add(BuildClass(toGenerate, "", "", out _));
            }

            return generatedCode.ToDictionary(v => v.Key, v => v.Value);
        }
        

        private KeyValuePair<string, string> BuildClass(
            KeyValuePair<string, object> cmd, 
            string postFix,
            string implements,
            out Dictionary<string, object> typesToGenerate)
        {
            typesToGenerate = new Dictionary<string, object>();

            var className = $"{cmd.Key}{postFix}";
            var code = new StringBuilder();
            
            

            code.AppendLine($"public class {className}{implements}");
            code.AppendLine($"{{");

            foreach (var f in cmd.Value.GetType().GetProperties())
            {
                var propType = f.PropertyType.ToString();
                if (f.PropertyType.IsArray)
                {
                    var typeName = f.Name.Substring(0, f.Name.Length - 1) + "Model";
                    propType = typeName + "[]";
                    typesToGenerate.Add(typeName, ((object[])f.GetValue(cmd.Value))[0]);
                }

                code.AppendLine($"public {propType} {f.Name} {{ get; private set; }}");
            }


            var constructorArgs = cmd.Value
                .GetType()
                .GetProperties()
                .Select(p =>
                {
                    var propType = p.PropertyType.ToString();
                    if (p.PropertyType.IsArray)
                    {
                        var typeName = p.Name.Substring(0, p.Name.Length - 1) + "Model";
                        propType = typeName + "[]";
                        
                    }
                    return $"{propType} {p.Name.ToLowerInvariant()}";
                });

            var constructor = new StringBuilder($"public {className}({string.Join(",", constructorArgs)}) {{");
            foreach (var f in cmd.Value.GetType().GetProperties())
            {
                constructor.AppendLine($"{f.Name} = {f.Name.ToLowerInvariant()};");
            }
            constructor.AppendLine("}");

            code.Append(constructor.ToString());

            code.AppendLine($"}}");

            return new KeyValuePair<string, string>($"{className}.cs", code.ToString());
        }

        

    }







}
