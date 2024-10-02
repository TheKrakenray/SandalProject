function controllo_login() {
    if (document.getElementById('mail').value.length < 4) {
        alert("Mail " + document.getElementById('mail').value + " troppo breve");
        return false;
    }
    if (document.getElementById('passw').value.length < 8) {
        alert("PASSWORD troppo breve");
        return false;
    }
}

document.getElementById('togglePassword').addEventListener('click', function (e) {
    const passwordField = document.getElementById('password');
    const type = passwordField.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordField.setAttribute('type', type);
    this.classList.toggle('fa-eye-slash');
});
