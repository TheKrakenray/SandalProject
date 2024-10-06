function controllo_registrazione() {
    if (document.getElementById('user').value.length < 4) {
        alert("USERNAME " + document.getElementById('user').value + " troppo breve");
        return false;
    }

    const emailRegex = /^ [\w -\.] + @([\w -] +\.)+[\w -]{ 2, 4 } $/;
    const email = document.getElementById('email');
    if (emailRegex.test(email)) {
        alert("l'email non è corretta");
        return false;
    }

    const passwregex = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$/;
    const passw = document.getElementById('passw').value;
    const validapassw = document.getElementById('validapassw').value;
    if (passwregex.test(passw)) {
        alert("la password deve essere di minimo 4 caratteri, deve contenere almeno una maiuscola una minuscola un numero e un carattere speciale");
        return false;
    }
    if (passw != validapassw) {
        alert("la password deve combaciare");
        return false;
    }
}

document.getElementById('registration-form').addEventListener('submit', function (event) {
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;
    const errorMessage = document.getElementById('error-message');

    if (password !== confirmPassword) {
        event.preventDefault();
        errorMessage.style.display = 'block';
    } else {
        errorMessage.style.display = 'none';
    }
});

function togglePassword(id) {
    const passwordField = document.getElementById(id);
    const icon = passwordField.nextElementSibling.querySelector('i');
    if (passwordField.type === "password") {
        passwordField.type = "text";
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    } else {
        passwordField.type = "password";
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    }
}