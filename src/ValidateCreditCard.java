import java.util.regex.Pattern;

public class ValidateCreditCard {

    public static void main(String[] args) {
        // Example usage for Visa
        String visaCardNumber = "4111111111111111";
        Result visaResult = validateCreditCard(visaCardNumber);
        System.out.println("Card Number: " + visaCardNumber + ", Valid: " + visaResult.valid + ", Bandeira: "
                + visaResult.bandeira);

        // Example usage for MasterCard
        String masterCardNumber = "5277959558870483";
        Result masterCardResult = validateCreditCard(masterCardNumber);
        System.out.println("Card Number: " + masterCardNumber + ", Valid: " + masterCardResult.valid + ", Bandeira: "
                + masterCardResult.bandeira);

        // Example usage for American Express
        String amexCardNumber = "378282246310005";
        Result amexResult = validateCreditCard(amexCardNumber);
        System.out.println("Card Number: " + amexCardNumber + ", Valid: " + amexResult.valid + ", Bandeira: "
                + amexResult.bandeira);

        // Example usage for Discover
        String discoverCardNumber = "6011111111111117";
        Result discoverResult = validateCreditCard(discoverCardNumber);
        System.out.println("Card Number: " + discoverCardNumber + ", Valid: " + discoverResult.valid + ", Bandeira: "
                + discoverResult.bandeira);

        // Example usage for Diners Club
        String dinersCardNumber = "30569309025904";
        Result dinersResult = validateCreditCard(dinersCardNumber);
        System.out.println("Card Number: " + dinersCardNumber + ", Valid: " + dinersResult.valid + ", Bandeira: "
                + dinersResult.bandeira);

        // Example usage for JCB
        String jcbCardNumber = "3530111333300000";
        Result jcbResult = validateCreditCard(jcbCardNumber);
        System.out.println(
                "Card Number: " + jcbCardNumber + ", Valid: " + jcbResult.valid + ", Bandeira: " + jcbResult.bandeira);

        // Example usage for Elo
        String eloCardNumber = "6363680000000000";
        Result eloResult = validateCreditCard(eloCardNumber);
        System.out.println(
                "Card Number: " + eloCardNumber + ", Valid: " + eloResult.valid + ", Bandeira: " + eloResult.bandeira);

        // Example usage for Hipercard
        String hipercardCardNumber = "6062825624254001";
        Result hipercardResult = validateCreditCard(hipercardCardNumber);
        System.out.println("Card Number: " + hipercardCardNumber + ", Valid: " + hipercardResult.valid + ", Bandeira: "
                + hipercardResult.bandeira);

        // Example usage for Aura
        String auraCardNumber = "5078601870000127980";
        Result auraResult = validateCreditCard(auraCardNumber);
        System.out.println("Card Number: " + auraCardNumber + ", Valid: " + auraResult.valid + ", Bandeira: "
                + auraResult.bandeira);
    }

    public static Result validateCreditCard(String cardNumber) {
        // Remove all non-digit characters
        String sanitized = cardNumber.replaceAll("\\D", "");

        // Check if the card number is valid using Luhn Algorithm
        if (!luhnCheck(sanitized)) {
            return new Result(false, null);
        }

        // Determine the card issuer (bandeira)
        String bandeira = getCardIssuer(sanitized);

        return new Result(true, bandeira);
    }

    public static boolean luhnCheck(String cardNumber) {
        int sum = 0;
        boolean shouldDouble = false;

        // Loop through the card number digits in reverse order
        for (int i = cardNumber.length() - 1; i >= 0; i--) {
            int digit = Character.getNumericValue(cardNumber.charAt(i));

            if (shouldDouble) {
                digit *= 2;
                if (digit > 9) {
                    digit -= 9;
                }
            }

            sum += digit;
            shouldDouble = !shouldDouble;
        }

        return sum % 10 == 0;
    }

    public static String getCardIssuer(String cardNumber) {
        String[] patterns = {
                "^4[0-9]{12}(?:[0-9]{3})?$", // Visa
                "^5[1-5][0-9]{14}$", // MasterCard
                "^3[47][0-9]{13}$", // American Express
                "^6(?:011|5[0-9]{2})[0-9]{12}$", // Discover
                "^3(?:0[0-5]|[68][0-9])[0-9]{11}$", // Diners Club
                "^(?:2131|1800|35\\d{3})\\d{11}$", // JCB
                "^((636368)|(438935)|(504175)|(451416)|(636297)|(5067)|(4576)|(4011))\\d+$", // Elo
                "^(606282\\d{10}(\\d{3})?)|(3841\\d{15})$", // Hipercard
                "^50[0-9]{14,17}$" // Aura
        };

        String[] issuers = {
                "visa", "mastercard", "amex", "discover", "diners", "jcb", "elo", "hipercard", "aura"
        };

        for (int i = 0; i < patterns.length; i++) {
            if (Pattern.matches(patterns[i], cardNumber)) {
                return issuers[i];
            }
        }

        return "unknown";
    }

    static class Result {
        boolean valid;
        String bandeira;

        Result(boolean valid, String bandeira) {
            this.valid = valid;
            this.bandeira = bandeira;
        }
    }
}