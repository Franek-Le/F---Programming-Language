using FMM.Helper.Error;
using FMM.Imports;

namespace FMM.Runtime
{
    public class Enviorment
    {
        public Enviorment? parent;
        private Dictionary<string, RuntimeVal> variables;
        private HashSet<string> constants;

        public Enviorment(Enviorment? parentENV) {
            parent = parentENV;
            variables = new Dictionary<string, RuntimeVal>();
            constants = new HashSet<string>();
        }

        public RuntimeVal DeclareVar(String varname, RuntimeVal value, bool constant) {
            if (variables.ContainsKey(varname)) {
                Error error = new Error(ErrorType.RuntimeError, $"Cannot declare variable: '{varname}' as it is already defined.");
            }

            variables[varname] = value;
            if (constant) {
                constants.Add(varname);
            }
            return value;
        }

        public RuntimeVal AssignVar(String varname, RuntimeVal value) {
            Enviorment env = Resolve(varname);
            env.variables[varname] = value;

            if (env.constants.Contains(varname))
            {
                Error error = new Error(ErrorType.RuntimeError, $"Cannot assign to constant variable: '{varname}'.");
            }

            return value;
        }

        public RuntimeVal LookupVar(String varname) {
            Enviorment env = Resolve(varname);
            return variables[varname];
        }

        public Enviorment Resolve(String varname)
        {
            if (variables.ContainsKey(varname)) {
                return this;
            }

            if (this.parent == null) {
                Error error = new Error(ErrorType.RuntimeError, $"Cannot resolve variable: '{varname}' as it does not exist.");
            }

            return this.parent.Resolve(varname);
        }
    }
}
