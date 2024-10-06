function validateEmail(email) {
    var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

document.getElementById('submitButton').addEventListener('click', function () {
    var email = document.getElementById('emailInput').value;
    var confirmEmail = document.getElementById('confirmEmailInput').value;
    var alertMessage = document.getElementById('alertMessage');

    if (email && confirmEmail) {
        if (validateEmail(email) && validateEmail(confirmEmail)) {
            if (email === confirmEmail) {
                alertMessage.textContent = "L'email è stata inviata!";
                alertMessage.classList.remove('hidden', 'error');
                alertMessage.classList.add('success');
            } else {
                alertMessage.textContent = "Le email non coincidono. Per favore, riprova.";
                alertMessage.classList.remove('hidden', 'success');
                alertMessage.classList.add('error');
            }
        } else {
            alertMessage.textContent = "Per favore, inserisci un'email valida.";
            alertMessage.classList.remove('hidden', 'success');
            alertMessage.classList.add('error');
        }
    } else {
        alertMessage.textContent = "Per favore, inserisci e conferma l'email.";
        alertMessage.classList.remove('hidden', 'success');
        alertMessage.classList.add('error');
    }
});

document.getElementById('returnButton').addEventListener('click', function () {
    window.location.href = "/Utente/Login/";
});