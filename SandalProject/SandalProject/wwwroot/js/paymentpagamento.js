function showPaymentFields(id) {
    const fields = document.querySelectorAll('.payment-fields');
    fields.forEach(field => field.style.display = 'none');
    document.getElementById(id).style.display = 'block';
}

function formatCardNumber() {
    const cardNumber = document.getElementById('card-number');
    const value = cardNumber.value.replace(/\s+/g, '').replace(/[^0-9]/gi, '');
    const matches = value.match(/\d{4,16}/g);
    const match = matches && matches[0] || '';
    const parts = [];

    for (let i = 0, len = match.length; i < len; i += 4) {
        parts.push(match.substring(i, i + 4));
    }

    if (parts.length) {
        cardNumber.value = parts.join(' ');
    } else {
        cardNumber.value = value;
    }
}

function validateCardNumber() {
    const cardNumber = document.getElementById('card-number').value.replace(/\s+/g, '');
    const cardNumberError = document.getElementById('card-number-error');
    const cardIcon = document.getElementById('card-icon');
    const visaRegex = /^4[0-9]{12}(?:[0-9]{3})?$/;
    const mastercardRegex = /^5[1-5][0-9]{14}$/;

    if (!visaRegex.test(cardNumber) && !mastercardRegex.test(cardNumber)) {
        cardNumberError.textContent = 'Numero di carta non valido';
        document.getElementById('card-number').classList.add('error');
        cardIcon.style.display = 'none'; // nascondi icon
    } else {
        cardNumberError.textContent = '';
        document.getElementById('card-number').classList.remove('error');
        cardIcon.style.display = 'block'; // nascondi icon
        if (visaRegex.test(cardNumber)) {
            cardIcon.src = 'https://upload.wikimedia.org/wikipedia/commons/5/5e/Visa_Inc._logo.svg';
        } else if (mastercardRegex.test(cardNumber)) {
            cardIcon.src = 'https://upload.wikimedia.org/wikipedia/commons/2/2a/Mastercard-logo.svg';
        }
    }
}

function validateCardName() {
    const cardName = document.getElementById('card-name').value;
    const cardNameError = document.getElementById('card-name-error');

    if (cardName.trim() === '') {
        cardNameError.textContent = 'Nome sulla carta è obbligatorio';
        document.getElementById('card-name').classList.add('error');
    } else {
        cardNameError.textContent = '';
        document.getElementById('card-name').classList.remove('error');
    }
}

function validateCardExpiry() {
    const cardExpiry = document.getElementById('card-expiry').value;
    const cardExpiryError = document.getElementById('card-expiry-error');
    const expiryRegex = /^(0[1-9]|1[0-2])\/?([0-9]{2})$/;

    if (!expiryRegex.test(cardExpiry)) {
        cardExpiryError.textContent = 'Data di scadenza non valida';
        document.getElementById('card-expiry').classList.add('error');
    } else {
        cardExpiryError.textContent = '';
        document.getElementById('card-expiry').classList.remove('error');
    }

    if (cardExpiry.length === 2 && !cardExpiry.includes('/')) {
        document.getElementById('card-expiry').value = cardExpiry + '/';
    }
}

function validateCardCVV() {
    const cardCVV = document.getElementById('card-cvv').value;
    const cardCVVError = document.getElementById('card-cvv-error');
    const cvvRegex = /^[0-9]{3,4}$/;

    if (!cvvRegex.test(cardCVV)) {
        cardCVVError.textContent = 'CVV non valido';
        document.getElementById('card-cvv').classList.add('error');
    } else {
        cardCVVError.textContent = '';
        document.getElementById('card-cvv').classList.remove('error');
    }
}

function validateForm() {
    validateCardNumber();
    validateCardName();
    validateCardExpiry();
    validateCardCVV();

    const cardNumberError = document.getElementById('card-number-error').textContent;
    const cardNameError = document.getElementById('card-name-error').textContent;
    const cardExpiryError = document.getElementById('card-expiry-error').textContent;
    const cardCVVError = document.getElementById('card-cvv-error').textContent;

    if (cardNumberError || cardNameError || cardExpiryError || cardCVVError) {
        // Non fare nulla, gli errori sono già visualizzati accanto ai campi
    } else {
        // Puoi aggiungere un messaggio di successo accanto al pulsante di invio
        const successMessage = document.getElementById('success-message');
        successMessage.textContent = 'Dati della carta validi. Procedi con il pagamento.';
        successMessage.style.color = 'green';
    }
}
