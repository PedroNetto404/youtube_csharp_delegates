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