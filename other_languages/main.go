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