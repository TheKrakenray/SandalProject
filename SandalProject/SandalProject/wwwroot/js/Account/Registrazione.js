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