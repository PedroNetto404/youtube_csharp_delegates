# C# Delegates: tratando funções como tipos de dados

## O que são delegates?

Delegates em C# são tipos de referência que podem ser usados para encapsular um método com uma assinatura específica e parâmetros. Eles são semelhantes a ponteiros de função em C e C++, mas são seguros e protegidos.

Com a keyword `delegate` é possível declarar um tipo de delegate que pode ser usado para referenciar métodos que correspondem à assinatura do delegate.

Observe a sintaxe a seguir:

```c#
//sintaxe:
//modificador_acesso delegate tipo_retorno NomeDelegate(parametros);
delegate void MeuDelegate(string mensagem);
```

Note que ao realizar essa declaração, definimos um novo tipo de dado, semelhante a quando declaramos uma classe, estrutura, interface ou enumeração. Isto é, `MeuDelegate` é um novo tipo de dado que pode ser utilizado para realizar a tipagem de variáveis. Isso implica que podemos `manipular funções como se fossem variáveis`.

Observe:

```c#
public delegate void TimeOutCallback();

public static class Timeout
{
    public static void SetTimeout(TimeOutCallback callback, int milliseconds)
    {
        Thread.Sleep(milliseconds);
        callback();
    }
}

public class Program
{
    public static void Main()
    {
        TimeOutCallback callback = () => Console.WriteLine("Time out!");
        Timeout.SetTimeout(callback, 1000);
    }
}
```

Repare que acabamos de utilizar uma função como argumento de um método. Isso é possível porque `TimeOutCallback` é um delegate que aceita funções que não recebem parâmetros e não retornam nada.

## Tipos definidos no .NET

Na biblioteca de classes do .NET, existem alguns tipos de delegates que são comumente utilizados. A vantagem é que podemos utilizá-los sem a necessidade de declarar um novo tipo de delegate, diminuindo a verbosidade do código.
É importante que tenhamos em mente a existência desses delegates, pois eles são amplamente utilizados em diversos cenários. (LINQ, eventos, etc);

Alguns deles são:

### `Func`

`Func`é uma delegate genérico que possui várias variações para suportar diferentes números de parâmetros e tipos de retorno. A última entrada de tipo é o tipo de retorno.

```c#
public delegate TResult Func<out TResult>();
public delegate TResult Func<in T, out TResult>(T arg);
public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
public delegate TResult Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
```

Exemplo:

```c#
Func<int, int, int> soma = (a, b) => a + b;
Console.WriteLine(soma(1, 2)); // 3
```

 ps: não se assuste, as keywords `in` e `out` são modificadores de variância que não serão abordados neste momento. Mas em resumo, `in` significa que o tipo é um parâmetro de entrada e `out` significa que o tipo é um parâmetro de saída.

### `Action`

`Action` é um delegate que não retorna valor e possui N parâmetros.

```c#
public delegate void Action();
public delegate void Action<in T>(T obj);
public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);
```

Exemplo:

```c#
Action<string> imprimir = (mensagem) => Console.WriteLine(mensagem);
imprimir("Olá, mundo!");
```

### `Predicate`

`Predicate` é um delegate que recebe um parâmetro e retorna um valor booleano.

```c#
public delegate bool Predicate<in T>(T obj);
```

Exemplo:

```c#
Predicate<int> ehPar = (numero) => numero % 2 == 0;
Console.WriteLine(ehPar(2)); // True
```

## Aplicações

### LINQ e delegates

LINQ (Language Integrated Query) é uma extensão do C# que permite realizar consultas em coleções de objetos. Uma das características mais interessantes do LINQ é a possibilidade de utilizar delegates para realizar consultas.

Por exemplo, o método de extensão Where do LINQ tem a seguinte assinatura:

```C#
public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
```

Repare, Where é um método de extensão, isto é, uma função,  que espera como argumento outra função que recebe um parâmetro e retorna um valor booleano. Isso significa que podemos passar qualquer função que atenda a essa assinatura como argumento para o método Where.

Exemplo:

```c#
List<int> numeros = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

Func<int, bool> ehPar = (numero) => numero % 2 == 0;
Func<int, bool> ehImpar = (numero) => numero % 2 != 0;

var pares = numeros.Where(ehPar);
var impares = numeros.Where(ehImpar);
```

### Callbacks comuns

Callbacks são funções que são passadas como argumento para outras funções. Elas são comuns em diversas situações, como eventos, timers, etc.

Context: CancellationToken é um tipo que nos permite cancelar uma operação assíncrona. Ele possui um método chamado Register que aceita um delegate que será chamado quando o token for cancelado.

Observe:

```c#
CancelationTokenSource cts = new CancelationTokenSource();
CancelationToken token = cts.Token;

Action callback = () => Console.WriteLine("Cancelado!");
token.Register(callback);

cts.Cancel();
```

Isto é útil, principalmente considerando aplicações ASP.NET Core, visto que o Lifetime de uma aplicação é gerenciado pelo framework. Dessa forma, podemos utilizar delegates para realizar ações quando a aplicação é iniciada, finalizada, etc.

```c#
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.Lifetime.ApplicationStarted.Register(() => Console.WriteLine("Aplicação iniciada!"));

app.Lifetime.ApplicationStopping.Register(() => Console.WriteLine("Aplicação parando!"));

app.Run();
```

## Funções como tipos de dados em outras linguagens

### C

Em C, é possível utilizar ponteiros de função para realizar a mesma funcionalidade que os delegates em C#. A diferença é que ponteiros de função em C são mais verbosos e menos seguros.

Exemplo:

```c
#include <stdio.h>

typedef int (*Operacao)(int, int);

int soma(int a, int b) {
    return a + b;
}

int subtracao(int a, int b) {
    return a - b;
}

int executaOperacao(Operacao operacao, int a, int b) {
    return operacao(a, b);
}

int main() {
    Operacao operacao = soma;
    printf("%d\n", executaOperacao(operacao, 1, 2)); // 3

    operacao = subtracao;
    printf("%d\n", executaOperacao(operacao, 1, 2)); // -1
}
```

### Go

Em Go, funções são tipos de dados de primeira classe, o que significa que podemos passá-las como argumento para outras funções. No entanto, Go não possui delegates como C#.

Exemplo:

```go
package main

import "fmt"

type Operacao func(int, int) int

func Soma(a, b int) int {
    return a + b
}

func Subtracao(a, b int) int {
    return a - b
}

func ExecutaOperacao(operacao Operacao, a, b int) int {
    return operacao(a, b)
}

func main() {
    var operacao Operacao = Soma
    fmt.Println(ExecutaOperacao(operacao, 1, 2)) // 3

    operacao = Subtracao
    fmt.Println(ExecutaOperacao(operacao, 1, 2)) // -1
}
```

### Python

```python
def soma(a, b):
    return a + b

def subtracao(a, b):
    return a - b

def executa_operacao(operacao, a, b):
    return operacao(a, b)

operacao = soma
print(executa_operacao(operacao, 1, 2)) # 3

operacao = subtracao
print(executa_operacao(operacao, 1, 2)) # -1
```

### JavaScript

```javascript
function soma(a, b) {
    return a + b;
}

function subtracao(a, b) {
    return a - b;
}

function executaOperacao(operacao, a, b) {
    return operacao(a, b);
}

let operacao = soma;
console.log(executaOperacao(operacao, 1, 2)); // 3

operacao = subtracao;
console.log(executaOperacao(operacao, 1, 2)); // -1
```
