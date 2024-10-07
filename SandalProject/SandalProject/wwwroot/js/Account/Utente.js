var userPoints = 400;

function showSection(section) {
    document.querySelectorAll('.user-section').forEach(function (el) {
        el.style.display = 'none';
    });
    document.getElementById(section).style.display = 'block';
    if (section === 'password-change') {
        document.getElementById('password-change').style.display = 'block';
    }
}

function showOrderDetails(orderId) {
    var details = document.getElementById('order-' + orderId + '-details');
    details.style.display = details.style.display === 'none' ? 'block' : 'none';
}

function enableEditing() {
    document.querySelectorAll('#user-form input').forEach(function (input) {
        input.disabled = false;
    });
    document.getElementById('edit-button').style.display = 'none';
    document.getElementById('save-button').style.display = 'block';
}

function saveChanges() {
    var form = document.getElementById('user-form');
    if (form.checkValidity()) {
        document.querySelectorAll('#user-form input').forEach(function (input) {
            input.disabled = true;
        });
        document.getElementById('edit-button').style.display = 'block';
        document.getElementById('save-button').style.display = 'none';
    } else {
        form.reportValidity();
    }
}

function updatePointsDisplay() {
    document.getElementById('user-points').textContent = userPoints;
}

updatePointsDisplay();

function changePassword() {
    var currentPassword = document.getElementById('current-password').value;
    var newPassword = document.getElementById('new-password').value;
    var confirmPassword = document.getElementById('confirm-password').value;

    if (newPassword !== confirmPassword) {
        alert("Le nuove password non coincidono.");
        return;
    }

    if (currentPassword && newPassword && confirmPassword) {
        // Qui puoi aggiungere il codice per inviare la richiesta al server per cambiare la password
        alert("Password cambiata con successo!");
        document.getElementById('password-form').reset();
    } else {
        alert("Per favore, compila tutti i campi.");
    }
}

function ShowCodice() {
    let codice = document.getElementById("codiceSconto");
    let punti = document.getElementById("user-points");
    codice.removeAttribute("hidden");
    punti.innerHTML = "300";
}