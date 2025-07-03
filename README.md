# F-- Programming Language
## F-- is basically Tylerlaceby's Guide to interpreters tutorial remade with C# and with additional features like a builtin graphics library, OS integration, reading and writing to files.

## F-- Syntax
The syntax is very simple

### Builtin keywords
There are 17 keywords in F--:

Variable Declaration
```var const```

Booleans
```false true```

Classes, Functions and Structs
```new class self function struct```

Loops
```for while```

Comparison
```if else elseif and or```

Builtin Functions
```print```

### Using builtin functions
You can print text to the console using ```print("");```

### Creating Variables
You can create a constant variable using ```const VariableName = 50;```

You can create a mutable variable using ```var VariableName = "Hello, world!";```

### Adding Comments
Comments can be created using '//' ```// This is a comment in F--```

### Class and Function declaration
You can create a class using ```class SomeClassName {}```

You can initialize the class by using ```SomeClassName myClass = new SomeClassName();```

You can create functions using  ```function SomeFunctionName(param1, param2) {}```

You can add methods to classes using:
```
Class SomeClassName {
  function Initializer() { // This is the function that gets executed when the class object gets created it is like '__init__()' in Python or 'constructor() {}' in Javascript.
    print("This will get printed when the class gets initialized!");
  }

  function SomeClassName.Add(num1, num2) {
    return num1 + num2;
  }
}
```
