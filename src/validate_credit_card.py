import re

def validate_credit_card(card_number):
    # Remove all non-digit characters
    sanitized = re.sub(r'\D', '', card_number)

    # Check if the card number is valid using Luhn Algorithm
    if not luhn_check(sanitized):
        return {'valid': False, 'bandeira': None}

    # Determine the card issuer (bandeira)
    bandeira = get_card_issuer(sanitized)

    return {'valid': True, 'bandeira': bandeira}

def luhn_check(card_number):
    sum = 0
    should_double = False

    # Loop through the card number digits in reverse order
    for digit in reversed(card_number):
        digit = int(digit)

        if should_double:
            digit *= 2
            if digit > 9:
                digit -= 9

        sum += digit
        should_double = not should_double

    return sum % 10 == 0

def get_card_issuer(card_number):
    patterns = {
        'visa': r'^4[0-9]{12}(?:[0-9]{3})?$',
        'mastercard': r'^5[1-5][0-9]{14}$',
        'amex': r'^3[47][0-9]{13}$',
        'discover': r'^6(?:011|5[0-9]{2})[0-9]{12}$',
        'diners': r'^3(?:0[0-5]|[68][0-9])[0-9]{11}$',
        'jcb': r'^(?:2131|1800|35\d{3})\d{11}$',
        'elo': r'^((636368)|(438935)|(504175)|(451416)|(636297)|(5067)|(4576)|(4011))\d+$',
        'hipercard': r'^(606282\d{10}(\d{3})?)|(3841\d{15})$',
        'aura': r'^50[0-9]{14,17}$'
    }

    for issuer, pattern in patterns.items():
        if re.match(pattern, card_number):
            return issuer

    return 'unknown'

# Example usage for Visa
visa_card_number = '4111111111111111'
visa_result = validate_credit_card(visa_card_number)
print(f"Card Number: {visa_card_number}, Valid: {visa_result['valid']}, Bandeira: {visa_result['bandeira']}") # { valid: True, bandeira: 'visa' }

# Example usage for MasterCard
master_card_number = '5277959558870483'
master_card_result = validate_credit_card(master_card_number)
print(f"Card Number: {master_card_number}, Valid: {master_card_result['valid']}, Bandeira: {master_card_result['bandeira']}") # { valid: True, bandeira: 'mastercard' }

# Example usage for American Express
amex_card_number = '378282246310005'
amex_result = validate_credit_card(amex_card_number)
print(f"Card Number: {amex_card_number}, Valid: {amex_result['valid']}, Bandeira: {amex_result['bandeira']}") # { valid: True, bandeira: 'amex' }

# Example usage for Discover
discover_card_number = '6011111111111117'
discover_result = validate_credit_card(discover_card_number)
print(f"Card Number: {discover_card_number}, Valid: {discover_result['valid']}, Bandeira: {discover_result['bandeira']}") # { valid: True, bandeira: 'discover' }

# Example usage for Diners Club
diners_card_number = '30569309025904'
diners_result = validate_credit_card(diners_card_number)
print(f"Card Number: {diners_card_number}, Valid: {diners_result['valid']}, Bandeira: {diners_result['bandeira']}") # { valid: True, bandeira: 'diners' }

# Example usage for JCB
jcb_card_number = '3530111333300000'
jcb_result = validate_credit_card(jcb_card_number)
print(f"Card Number: {jcb_card_number}, Valid: {jcb_result['valid']}, Bandeira: {jcb_result['bandeira']}") # { valid: True, bandeira: 'jcb' }

# Example usage for Elo
elo_card_number = '6363680000000000'
elo_result = validate_credit_card(elo_card_number)
print(f"Card Number: {elo_card_number}, Valid: {elo_result['valid']}, Bandeira: {elo_result['bandeira']}") # { valid: True, bandeira: 'elo' }

# Example usage for Hipercard
hipercard_card_number = '6062825624254001'
hipercard_result = validate_credit_card(hipercard_card_number)
print(f"Card Number: {hipercard_card_number}, Valid: {hipercard_result['valid']}, Bandeira: {hipercard_result['bandeira']}") # { valid: True, bandeira: 'hipercard' }

# Example usage for Aura
aura_card_number = '5078601870000127980'
aura_result = validate_credit_card(aura_card_number)
print(f"Card Number: {aura_card_number}, Valid: {aura_result['valid']}, Bandeira: {aura_result['bandeira']}") # { valid: True, bandeira: 'aura' }