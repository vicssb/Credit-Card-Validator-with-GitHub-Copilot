function validateCreditCard(cardNumber) {
    // Remove all non-digit characters
    const sanitized = cardNumber.replace(/[^0-9]/g, '');

    // Check if the card number is valid using Luhn Algorithm
    if (!luhnCheck(sanitized)) {
        return { valid: false, bandeira: null };
    }

    // Determine the card issuer (bandeira)
    const bandeira = getCardIssuer(sanitized);

    return { valid: true, bandeira: bandeira };
}

function luhnCheck(cardNumber) {
    let sum = 0;
    let shouldDouble = false;

    // Loop through the card number digits in reverse order
    for (let i = cardNumber.length - 1; i >= 0; i--) {
        let digit = parseInt(cardNumber.charAt(i));

        if (shouldDouble) {
            digit *= 2;
            if (digit > 9) {
                digit -= 9;
            }
        }

        sum += digit;
        shouldDouble = !shouldDouble;
    }

    return sum % 10 === 0;
}

function getCardIssuer(cardNumber) {
    const patterns = {
        visa: /^4[0-9]{12}(?:[0-9]{3})?$/,
        mastercard: /^5[1-5][0-9]{14}$/,
        amex: /^3[47][0-9]{13}$/,
        discover: /^6(?:011|5[0-9]{2})[0-9]{12}$/,
        diners: /^3(?:0[0-5]|[68][0-9])[0-9]{11}$/,
        jcb: /^(?:2131|1800|35\d{3})\d{11}$/,
        elo: /^((636368)|(438935)|(504175)|(451416)|(636297)|(5067)|(4576)|(4011))\d+$/,
        hipercard: /^(606282\d{10}(\d{3})?)|(3841\d{15})$/,
        aura: /^50[0-9]{14,17}$/
    };

    for (const [issuer, pattern] of Object.entries(patterns)) {
        if (pattern.test(cardNumber)) {
            return issuer;
        }
    }

    return 'unknown';
}

// Example usage 1
// const cardNumber = '4111111111111111';
// const result = validateCreditCard(cardNumber);
// console.log(result); // { valid: true, bandeira: 'visa' }

// Example usage 2 
// const cardNumber = '5277959558870483';
// const result = validateCreditCard(cardNumber);
// console.log(result); // { valid: true, bandeira: 'visa' }

// Example usage for Visa
const visaCardNumber = '4111111111111111';
const visaResult = validateCreditCard(visaCardNumber);
console.log(visaResult); // { valid: true, bandeira: 'visa' }

// Example usage for MasterCard
const masterCardNumber = '5277959558870483';
const masterResult = validateCreditCard(masterCardNumber);
console.log(masterResult); // { valid: true, bandeira: 'mastercard' }

// Example usage for American Express
const amexCardNumber = '378282246310005';
const amexResult = validateCreditCard(amexCardNumber);
console.log(amexResult); // { valid: true, bandeira: 'amex' }

// Example usage for Discover
const discoverCardNumber = '6011111111111117';
const discoverResult = validateCreditCard(discoverCardNumber);
console.log(discoverResult); // { valid: true, bandeira: 'discover' }

// Example usage for Diners Club
const dinersCardNumber = '30569309025904';
const dinersResult = validateCreditCard(dinersCardNumber);
console.log(dinersResult); // { valid: true, bandeira: 'diners' }

// Example usage for JCB
const jcbCardNumber = '3530111333300000';
const jcbResult = validateCreditCard(jcbCardNumber);
console.log(jcbResult); // { valid: true, bandeira: 'jcb' }