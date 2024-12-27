// Exemplo de delegate definido

IEnumerable<int> numeros = Enumerable.Range(1, 100);

IEnumerable<Condicao<int>> condicoes = [
  //Numeros pares
  x => x % 2 == 0,
  //Numeros impares
  x => x % 2 != 0,
  //Numeros maiores que 50
  x => x > 50,
  //Numeros primos
  x => {
    if (x == 1) return false;
    if (x == 2) return true;
    if (x % 2 == 0) return false;
    for (int i = 3; i <= Math.Sqrt(x); i += 2)
    {
        if (x % i == 0) return false;
    }
    return true;
  }
];

foreach (var condicao in condicoes)
{
    foreach (var item in numeros.ElementosOnde(condicao))
    {
        Console.WriteLine(item);
    }
    
    Console.WriteLine();
}


public static class Extensions
{
    public static IEnumerable<T> ElementosOnde<T>(this IEnumerable<T> colecao, Condicao<T> condicao)
    {
        foreach (var item in colecao)
        {
            if (condicao(item))
            {
                yield return item;
            }
        }
    }
}

public delegate bool Condicao<T>(T valor);
