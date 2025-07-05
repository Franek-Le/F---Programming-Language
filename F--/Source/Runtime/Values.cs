using FMM.Imports;

namespace FMM.Runtime
{
    public enum ValueType {
        Null,
        Number,
        Boolean,
    }

    public interface RuntimeVal {
        ValueType type { get; }
    }

    public class NullVal : RuntimeVal {
        public ValueType type => ValueType.Null;
        public String value = "null";
    }

    public class BoolVal : RuntimeVal
    {
        public ValueType type => ValueType.Boolean;
        public bool value;

        public BoolVal(bool v)
        {
            value = v;
        }
    }

    public class NumberVal : RuntimeVal {
        public ValueType type => ValueType.Number;
        public float value;

        public NumberVal(float v) {
            value = v;
        }
    }
}
