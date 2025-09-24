// Validação de CPF
$.validator.addMethod("cpfValido", function (value, element) {
    value = value.replace(/[^\d]+/g, '');
    if (value.length !== 11 || /^(\d)\1+$/.test(value)) return false;

    let soma = 0;
    for (let i = 0; i < 9; i++) soma += parseInt(value.charAt(i)) * (10 - i);
    let resto = soma % 11;
    let digito1 = resto < 2 ? 0 : 11 - resto;

    soma = 0;
    for (let i = 0; i < 10; i++) soma += parseInt(value.charAt(i)) * (11 - i);
    resto = soma % 11;
    let digito2 = resto < 2 ? 0 : 11 - resto;

    return digito1 === parseInt(value.charAt(9)) && digito2 === parseInt(value.charAt(10));
}, "CPF inválido.");
