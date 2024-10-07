const form = document.querySelector('form');
form.addEventListener('submit', (e) => {
    const fileInput = document.getElementById('file');
    if (!fileInput.files.length) {
        e.preventDefault();
        alert('Seleziona un file Excel prima di inviare il form.');
    }
});

//Javascript dowloadExcel(non funziona)
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('downloadExcelBtn').addEventListener('click', function () {
        window.location.href = '/Utente/DownloadExcel';
    });
});
