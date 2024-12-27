#include <stdio.h>

typedef int (*Operacao)(int, int);

int soma(int a, int b)
{
    return a + b;
}

int subtracao(int a, int b)
{
    return a - b;
}

int executaOperacao(Operacao operacao, int a, int b)
{
    return operacao(a, b);
}

int main()
{
    Operacao operacao = soma;
    printf("%d\n", executaOperacao(operacao, 1, 2)); // 3

    operacao = subtracao;
    printf("%d\n", executaOperacao(operacao, 1, 2)); // -1
}