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
